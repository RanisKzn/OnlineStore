<script>
import { deleteUser, deleteCustomer } from "../services/api";
import Toastify from "toastify-js";
import "toastify-js/src/toastify.css";

export default {
  name: "DelereModal",
  props: {
    customer: Object,
  },
  methods: {
    close() {
      this.$emit("close");
    },
    async confirmDelete() {
      try {
        console.log("Customer deleted:", await deleteCustomer(this.customer.Id));
        console.log("User deleted:", await deleteUser(this.customer.UserId));
        this.showNotification(
          "Заказчик успешно удален",
          "linear-gradient(to right, #00b09b, #96c93d)"
        );
        this.close();
      } catch (error) {
        console.error("Error adding customer:", error);
        this.showNotification(
          "Ошибка при удалении заказчика",
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
  },
};
</script>

<template>
  <div class="fixed inset-0 bg-black/50 backdrop-blur-sm z-10 flex items-center justify-center">
    <div class="fixed inset-0 bg-black opacity-50" @click="close"></div>
    <div class="bg-white p-8 rounded-lg shadow-lg w-full max-w-md relative z-10">
      <h2 class="text-xl font-bold mb-4">Подтвердите удаление</h2>
      <p class="mb-6">
        Вы уверены, что хотите удалить заказчика <b>{{ customer.Name }}</b
        >?
      </p>
      <div class="flex justify-end space-x-4">
        <button
          type="button"
          class="px-4 py-2 bg-gray-300 text-gray-700 rounded hover:bg-gray-400 transition"
          @click="close"
        >
          Отмена
        </button>
        <button
          type="button"
          class="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600 transition"
          @click="confirmDelete"
        >
          Удалить
        </button>
      </div>
    </div>
  </div>
</template>
