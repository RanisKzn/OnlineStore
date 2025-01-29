<script>
import { addCustomer, addUser } from "../services/api";
import { v4 as uuidv4 } from "uuid";
import Toastify from "toastify-js";
import "toastify-js/src/toastify.css";

export default {
  name: "CustomerForm",
  props: {
    mode: String,
  },
  data() {
    return {
      customer: {
        id: "",
        name: "",
        code: "",
        address: "",
        discount: "",
        userid: "",
      },
      user: {
        id: "",
        username: "",
        password: "",
        roleId: "f2058115-0e96-4deb-9fec-79dbab43899d",
      },
    };
  },
  methods: {
    async addCustomer() {
      try {
        this.user.id = uuidv4();
        this.customer.id = uuidv4();
        this.customer.userid = this.user.id;
        if (await addUser(this.user)) {
          console.log("Customer added:", await addCustomer(this.customer));
        }
        this.showNotification(
          "Заказчик успешно добавлен",
          "linear-gradient(to right, #00b09b, #96c93d)"
        );
        this.closeForm();
      } catch (error) {
        console.error("Error adding customer:", error);
        this.showNotification(
          "Ошибка при добавлении заказчика",
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
  <div class="fixed inset-0 bg-black bg-opacity-50 z-10 flex items-center justify-center">
    <div class="bg-white w-1/2 p-8 rounded shadow-lg">
      <h2 class="text-2xl font-bold mb-6">
        {{ mode === "add" ? "Добавить заказчика" : "Редактировать заказчика" }}
      </h2>
      <form @submit.prevent="addCustomer">
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Имя</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="customer.name"
            required
          />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="code">Код</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="code"
            type="text"
            v-model="customer.code"
            required
          />
        </div>
        <div class="mb-6">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="address">Адрес</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="address"
            type="text"
            v-model="customer.address"
            required
          />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Скидка</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="customer.discount"
            required
          />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Логин</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="user.username"
            required
          />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Пароль</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="user.password"
            required
          />
        </div>

        <div class="flex items-center justify-between">
          <button
            class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
            type="submit"
          >
            Добавить
          </button>
          <button
            class="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
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
