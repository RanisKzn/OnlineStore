<script>
import { addItem, updateItem, getCategories } from "../services/api";
import { v4 as uuidv4 } from "uuid";
import Toastify from "toastify-js";
import "toastify-js/src/toastify.css";

export default {
  name: "ProductForm",
  props: {
    mode: String,
    productEdit: Object,
    categoryEdit: Object,
  },
  data() {
    return {
      isOpen: false,
      category: this.mode === "add" ? "" : this.categoryEdit,
      selectedOption: this.mode === "add" ? "" : this.categoryEdit,
      options: {},
      categories: {},
      product:
        this.mode === "add"
          ? {
              Id: "",
              Name: "",
              Code: "",
              Price: "",
              Categotyid: "",
            }
          : this.productEdit,
    };
  },
  async created() {
    this.categories = await getCategories();
  },
  methods: {
    toggleDropdown() {
      this.isOpen = !this.isOpen;
    },
    selectOption(category) {
      this.selectedOption = category;
      this.isOpen = false;
    },
    async addProduct() {
      try {
        this.product.Id = uuidv4();
        this.product.CategoryId = this.selectedOption.Id;
        console.log("Product added:", await addItem(this.product));
        this.showNotification(
          "Продукт успешно добавлен",
          "linear-gradient(to right, #00b09b, #96c93d)"
        );
        this.closeForm();
      } catch (error) {
        console.error("Error adding Product:", error);
        this.showNotification(
          "Ошибка при добавлении продукта",
          "linear-gradient(to right, #ff0000, #ffc0cb)"
        );
      }
    },
    async editProduct() {
      try {
        this.product.CategoryId = this.selectedOption.Id;
        console.log("Product updated:", await updateItem(this.product));
        this.showNotification(
          "Продукт успешно обновлен",
          "linear-gradient(to right, #00b09b, #96c93d)"
        );
        this.closeForm();
      } catch (error) {
        console.error("Error updating Product:", error);
        this.showNotification(
          "Ошибка при обновлении продукта",
          "linear-gradient(to right, #ff0000, #ffc0cb)"
        );
      }
    },
    handleSubmit() {
      return this.mode === "add" ? this.addProduct() : this.editProduct();
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
        {{ mode === "add" ? "Добавить продукт" : "Редактировать продукт" }}
      </h2>
      <form @submit.prevent="handleSubmit">
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Имя</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="product.Name"
            required
          />
        </div>
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="code">Код</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="code"
            type="text"
            v-model="product.Code"
            required
          />
        </div>

        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="code">Категория</label>
          <div>
            <button
              @click="toggleDropdown"
              type="button"
              class="inline-flex justify-between w-full px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-50 focus:outline-none"
            >
              {{ selectedOption.Name }}
              <svg
                class="-mr-1 ml-2 h-5 w-5"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 20 20"
                fill="currentColor"
                aria-hidden="true"
              >
                <path
                  fill-rule="evenodd"
                  d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                  clip-rule="evenodd"
                />
              </svg>
            </button>
            <div
              v-if="isOpen"
              class="origin-top-left absolute mt-2 w-auto shadow-lg bg-white ring-black ring-opacity-5"
            >
              <div
                class="py-1"
                role="menu"
                aria-orientation="vertical"
                aria-labelledby="options-menu"
              >
                <a
                  href="#"
                  v-for="(category, index) in categories"
                  :key="index"
                  @click="selectOption(category)"
                  class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 hover:text-gray-900"
                  role="menuitem"
                >
                  {{ category.Name }}
                </a>
              </div>
            </div>
          </div>
        </div>
        <div class="mb-4">
          <label class="block text-gray-700 text-sm font-bold mb-2" for="name">Цена</label>
          <input
            class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            v-model="product.Price"
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
