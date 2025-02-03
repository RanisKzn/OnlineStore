<script>
import { getCategory } from "../services/api";
import DeleteModal from "./ProductDeleteModal.vue";
import ProductForm from "./ProductForm.vue";
export default {
  components: { ProductForm, DeleteModal },
  name: "CardComp",
  props: {
    product: Object,
  },
  data() {
    return {
      showEditForm: false,
      showDeleteForm: false,
      mode: "",
      category: {},
      role: [],
      showCustomerButton: false,
      cartItemCount: 0,
      isDisabled: true,
      isInCart: false,
    };
  },
  async mounted() {
    this.category = await getCategory(this.product.CategoryId);
    this.role = localStorage.getItem("role");
    this.showCustomerButton = this.role === "Customer" ? true : false;
    this.InCart();
    this.cartItem();
  },
  methods: {
    InCart() {
      const cart = JSON.parse(localStorage.getItem("cart")) || [];
      this.isInCart = cart.some((item) => item.product.Id === this.product.Id);
    },
    cartItem() {
      const cart = JSON.parse(localStorage.getItem("cart")) || [];
      this.cartItemCount = cart.find((item) => item.product.Id === this.product.Id).count || {
        count: 0,
      };
    },
    async editOrder() {
      this.mode = "edit";
      this.showEditForm = true;
    },
    openDeleteModal() {
      this.showDeleteForm = true;
    },
    handleClose() {
      this.showDeleteForm = false;
      this.showEditForm = false;
      this.$emit("close");
    },
    intoCart() {
      let cart = localStorage.getItem("cart");
      let newProduct = [
        {
          product: this.product,
          count: 1,
        },
      ];
      if (!cart) {
        localStorage.setItem("cart", JSON.stringify(newProduct));
        this.InCart();
        this.cartItem();
      } else {
        cart = JSON.parse(cart);
        cart.forEach((productInCart) => {
          if (productInCart.product.Id === this.product.Id) {
            productInCart.count = Number(productInCart.count) + 1;
            newProduct = null;
          }
        });
        Array.prototype.push.apply(cart, newProduct);
        localStorage.setItem("cart", JSON.stringify(cart));
        this.InCart();
        this.cartItem();
      }
    },
    increaseItemCount() {
      let cart = JSON.parse(localStorage.getItem("cart")) || [];

      const productIndex = cart.findIndex((item) => item.product.Id === this.product.Id);

      if (productIndex !== -1) {
        cart[productIndex].count += 1;
        localStorage.setItem("cart", JSON.stringify(cart));
      }
      this.InCart();
      this.cartItem();
    },

    decreaseItemCount() {
      let cart = JSON.parse(localStorage.getItem("cart")) || [];

      const productIndex = cart.findIndex((item) => item.product.Id === this.product.Id);

      if (productIndex !== -1) {
        if (cart[productIndex].count > 1) {
          cart[productIndex].count -= 1;
        } else {
          cart.splice(productIndex, 1);
        }
        localStorage.setItem("cart", JSON.stringify(cart));
      }

      this.InCart();
      this.cartItem();
    },
  },
};
</script>

<template>
  <ProductForm
    v-if="showEditForm"
    @close="handleClose"
    :mode="this.mode"
    :productEdit="this.product"
    :categoryEdit="this.category"
  ></ProductForm>
  <DeleteModal v-if="showDeleteForm" :product="this.product" @close="handleClose" />
  <div
    class="relative flex flex-col w-full border border-slate-100 rounded-xl p-8 cursor-pointer transition hover:shadow-xl hover:transform hover:-translate-y-2"
  >
    <div class="flex justify-between mt-5">
      <div class="flex flex-col gap-2">
        <span class="font-bold">Имя: {{ this.product.Name }}</span>
        <p>Код: {{ product.Code }}</p>
        <p>
          Категория:
          {{ category.Name }}
        </p>
        <p>
          <b>Цена: {{ product.Price }} руб.</b>
        </p>
      </div>
      <div class="flex gap-2 my-auto">
        <div v-if="role === 'Customer'" class="flex items-center gap-2">
          <button
            v-if="!isInCart"
            class="px-4 py-2 bg-blue-400 text-white rounded hover:bg-blue-600 transition"
            @click="intoCart"
          >
            В корзину
          </button>
          <div v-else class="flex items-center gap-2">
            <button
              class="px-2 py-1 bg-blue-400 text-white rounded hover:bg-blue-600 transition"
              @click="increaseItemCount"
            >
              +
            </button>
            <spam>{{ this.cartItemCount }}</spam>
            <button
              class="px-2 py-1 bg-red-400 text-white rounded hover:bg-red-600 transition"
              @click="decreaseItemCount"
              :disabled="cartItem.count <= 1"
            >
              -
            </button>
          </div>
        </div>
        <button
          class="px-4 py-2 bg-blue-400 text-white rounded hover:bg-blue-600 transition"
          v-if="role === 'Manager'"
          @click="editOrder"
        >
          Редактировать
        </button>
        <button
          class="px-4 py-2 bg-red-400 text-white rounded hover:bg-red-600 transition"
          v-if="role === 'Manager'"
          @click="openDeleteModal"
        >
          Удалить
        </button>
      </div>
    </div>
  </div>
</template>
