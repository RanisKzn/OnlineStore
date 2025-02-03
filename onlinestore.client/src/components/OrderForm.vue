<script>
import { getCustomer, getStatuses, updateOrder } from "../services/api";

import Toastify from "toastify-js";
import "toastify-js/src/toastify.css";

export default {
  name: "ProductForm",
  props: {
    orderEdit: Object,
  },
  data() {
    return {
      order: this.orderEdit,
      customer: {},
      isDisabled: true,
      status: [],
    };
  },
  async created() {
    this.customer = await getCustomer(this.order.CustomerId);
    this.statuses = await getStatuses();
    this.status = this.statuses.find((status) => status.Id === this.order.StatusId);
  },
  methods: {
    async editOrder() {
      this.status = this.statuses.find((status) => status.Name === "Выполняется");
      this.order.StatusId = this.status.Id;
      console.log(this.order.Id);
      try {
        console.log("Order updated:", await updateOrder(this.order));
        this.showNotification(
          "Заказ успешно обновлен",
          "linear-gradient(to right, #00b09b, #96c93d)"
        );
        this.closeForm();
      } catch (error) {
        console.error("Error updating Order:", error);
        this.showNotification(
          "Ошибка при обновлении заказа",
          "linear-gradient(to right, #ff0000, #ffc0cb)"
        );
      }
    },
    closeForm() {
      this.$emit("close");
    },
    showNotification(message, background) {
      Toastify({
        text: message,
        duration: 3000,
        close: true,
        gravity: "top",
        position: "center",
        style: {
          background: background,
        },
      }).showToast();
    },
  },
};
</script>

<template>
  <div class="fixed inset-0 bg-black/50 backdrop-blur-sm z-10 flex items-center justify-center">
    <div class="bg-white w-1/3 p-8 rounded-xl shadow-lg">
      <h2 class="text-2xl font-bold mb-6">Редактировать заказ</h2>
      <form @submit.prevent="editOrder">
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Номер</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="this.order.OrderNumber"
            :disabled="isDisabled"
            required
          />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="code">Заказчик</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="code"
            type="text"
            v-model="customer.Name"
            :disabled="isDisabled"
            required
          />
        </div>

        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Дата заказа</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="order.OrderDate"
            :disabled="isDisabled"
            required
          />
        </div>

        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Дата досаввки</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="date"
            v-model="order.ShipmentDate"
            required
          />
        </div>

        <div class="flex items-center justify-between">
          <button
            class="bg-blue-400 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
            type="submit"
          >
            Сохранить
          </button>
          <button
            class="bg-red-400 hover:bg-red-600 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
            type="button"
            @click="closeForm"
          >
            Отменить
          </button>
        </div>
      </form>
    </div>
  </div>
</template>
