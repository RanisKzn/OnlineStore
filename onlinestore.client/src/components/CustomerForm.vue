<script>
import { addCustomer, updateCustomer, addUser, getUser, updateUser } from "../services/api";
import { v4 as uuidv4 } from "uuid";
import Toastify from "toastify-js";
import "toastify-js/src/toastify.css";

export default {
  name: "CustomerForm",
  props: {
    mode: String,
    customerEdit: Object,
    userEdit: Object,
  },
  data() {
    return {
      customer:
        this.mode === "add"
          ? {
              Id: "",
              Name: "",
              Code: "",
              Address: "",
              Discount: "",
              Userid: "",
            }
          : this.customerEdit,
      user:
        this.mode === "add"
          ? {
              Id: "",
              Username: "",
              Password: "",
              RoleId: "f2058115-0e96-4deb-9fec-79dbab43899d",
            }
          : this.userEdit,
    };
  },
  methods: {
    async addCustomer() {
      try {
        this.user.Id = uuidv4();
        this.customer.Id = uuidv4();
        this.customer.Userid = this.user.Id;
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
    async editCustomer() {
      this.user = await getUser(this.customerEdit.UserId);
      try {
        if (await updateUser(this.user)) {
          console.log("Customer updated:", await updateCustomer(this.customerEdit));
        }
        this.showNotification(
          "Заказчик успешно обновлен",
          "linear-gradient(to right, #00b09b, #96c93d)"
        );
        this.closeForm();
      } catch (error) {
        console.error("Error updating customer:", error);
        this.showNotification(
          "Ошибка при обновлении заказчика",
          "linear-gradient(to right, #ff0000, #ffc0cb)"
        );
      }
    },
    handleSubmit() {
      return this.mode === "add" ? this.addCustomer() : this.editCustomer();
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
      <h2 class="text-2xl font-bold mb-6">
        {{ mode === "add" ? "Добавить заказчика" : "Редактировать заказчика" }}
      </h2>
      <form @submit.prevent="handleSubmit">
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Имя</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="customer.Name"
            required
          />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="code">Код</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="code"
            type="text"
            v-model="customer.Code"
            required
          />
        </div>
        <div class="mb-6">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="address">Адрес</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="address"
            type="text"
            v-model="customer.Address"
            required
          />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Скидка</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="customer.Discount"
            required
          />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Логин</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="user.Username"
            required
          />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Пароль</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="user.Password"
            required
          />
        </div>

        <div class="flex items-center justify-between">
          <button
            class="bg-blue-400 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
            type="submit"
          >
            {{ mode === "add" ? "Добавить" : "Редактировать" }}
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
