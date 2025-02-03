<script>
import { getCustomer, getStatuses, updateOrder } from "@/services/api";
import OrderForm from "./OrderForm.vue";
import DeleteModal from "./OrderDeleteModal.vue";
import Toastify from "toastify-js";
import "toastify-js/src/toastify.css";

export default {
  components: { OrderForm, DeleteModal },
  name: "CardComp",
  props: {
    order: Object,
  },
  data() {
    return {
      showEditForm: false,
      showDeleteForm: false,
      mode: "",
      customer: [],
      statuses: {},
      status: [],
      role: [],
      currentOrder: {},
    };
  },
  async mounted() {
    this.customer = await getCustomer(this.order.CustomerId);
    this.statuses = await getStatuses();
    this.status = this.statuses.find((status) => status.Id === this.order.StatusId);
    this.role = localStorage.getItem("role");
    this.currentOrder = this.order;
  },
  methods: {
    async collectOrder() {
      this.mode = "edit";
      this.showEditForm = true;
    },
    async performOrder() {
      this.status = this.statuses.find((status) => status.Name === "Выполнен");
      this.currentOrder.StatusId = this.status.Id;
      try {
        console.log("Order updated:", await updateOrder(this.currentOrder));
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
    openDeleteModal() {
      this.showDeleteForm = true;
    },
    handleClose() {
      this.showDeleteForm = false;
      this.showEditForm = false;
      this.$emit("close");
    },
    getStatusClass(statusName) {
      switch (statusName) {
        case "Выполнен":
          return "rounded px-2 bg-green-500 text-white";
        case "Новый":
          return " rounded px-2 bg-red-500 text-white";
        case "Выполняется":
          return "rounded px-2 bg-blue-500 text-white";
        default:
          return "bg-gray-500 text-white";
      }
    },

    handleSubmit() {
      switch (this.status.Name) {
        case "Новый":
          return this.collectOrder();
        case "Выполняется":
          return this.performOrder();
        default:
          return "";
      }
    },
  },
};
</script>

<template>
  <OrderForm
    v-if="showEditForm"
    @close="handleClose"
    :mode="this.mode"
    :orderEdit="this.order"
  ></OrderForm>
  <DeleteModal v-if="showDeleteForm" :order="this.order" @close="handleClose" />
  <div
    class="relative flex flex-col w-full border border-slate-100 rounded-xl p-8 cursor-pointer transition hover:shadow-xl hover:transform hover:-translate-y-2"
  >
    <div class="flex justify-between mt-5">
      <div class="flex flex-col gap-2">
        <span
          ><b>№ {{ order.OrderNumber }} - </b>
          <span :class="getStatusClass(this.status.Name)"> {{ this.status.Name }}</span></span
        >
        <p>Заказчик: {{ customer.Name }}</p>
        <p>Дата заказа: {{ order.OrderDate }}</p>
        <p>
          <b>Дата досаввки: {{ order.ShipmentDate == null ? "-" : order.ShipmentDate }} </b>
        </p>
      </div>
      <div class="flex gap-2 my-auto">
        <button
          v-if="role === 'Customer' && this.status.Name === 'Новый'"
          class="px-4 py-2 bg-red-400 text-white rounded hover:bg-red-600 transition"
          @click="openDeleteModal"
        >
          Удалить
        </button>
        <button
          v-if="role === 'Manager' && this.status.Name !== 'Выполнен'"
          class="px-4 py-2 bg-blue-400 text-white rounded hover:bg-blue-600 transition"
          @click="handleSubmit"
        >
          Подтвердить
        </button>
      </div>
    </div>
  </div>
</template>
