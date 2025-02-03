<script>
import { addOrder, addOrderItem, getStatuses } from "../services/api";
import { v4 as uuidv4 } from "uuid";
import Toastify from "toastify-js";
import "toastify-js/src/toastify.css";
import CartItemComp from "./CartItemComp.vue";

export default {
  components: { CartItemComp },
  props: {},
  data() {
    return {
      cartItems: {},
      customer: {},
      totalPrice: [],
      discount: [],
      order: {
        Id: "",
        CustomerId: "",
        OrderDate: "",
        ShipmentDate: "",
        OrderNumber: 0,
        StatusId: "",
      },
      orderItem: {
        Id: "",
        OrderId: "",
        ItemId: "",
        ItemsCount: "",
        ItemPrice: "",
      },
    };
  },
  watch: {
    $route: "created",
  },
  async created() {
    try {
      this.cartItems = JSON.parse(localStorage.getItem("cart"));
      this.customer = JSON.parse(localStorage.getItem("customer"));
      this.cartItems.forEach((cartItem) => {
        this.totalPrice = Number(this.totalPrice) + cartItem.count * cartItem.product.Price;
      });
      this.discount = (Number(this.totalPrice) / 100) * Number(this.customer.Discount);
    } catch (error) {
      console.log(error);
    }
  },
  methods: {
    async fetchData() {},
    async addOrder() {
      try {
        this.order.Id = uuidv4().toString();
        this.order.CustomerId = this.customer.Id.toString();
        let statuses = await getStatuses();
        let now = new Date();
        this.order.OrderDate = new Date(now.getFullYear(), now.getMonth(), now.getDate())
          .toISOString()
          .split("T")[0];
        this.order.StatusId = statuses.find((status) => status.Name === "Новый").Id.toString();
        console.log(this.order.StatusId);
        console.log("Order added:", await addOrder(this.order));
        for (const cartItem of this.cartItems) {
          this.orderItem.Id = uuidv4();
          this.orderItem.OrderId = this.order.Id;
          this.orderItem.ItemId = cartItem.product.Id;
          this.orderItem.ItemsCount = cartItem.count;
          this.orderItem.ItemPrice = cartItem.product.Price;
          await addOrderItem(this.orderItem);
        }
        this.showNotification(
          "Заказ успешно добавлен",
          "linear-gradient(to right, #00b09b, #96c93d)"
        );
        localStorage.removeItem("cart");
        this.closeForm();
      } catch (error) {
        console.error("Error adding Order:", error);
        this.showNotification(
          "Ошибка при добавлении заказа",
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
  <div class="fixed inset-0 bg-black/50 backdrop-blur-sm z-10" />
  <div
    class="flex flex-col justify-between fixed h-full z-10 top-0 right-0 w-96 bg-white px-10 py-7"
  >
    <h2 @click="closeForm" class="text-2xl font-bold mb-10 flex items-center gap-5">
      <svg
        class="rotate-180 hover:-translate-x-1 opacity-30 hover:opacity-100 transition cursor-pointer"
        width="16"
        height="14"
        viewBox="0 0 16 14"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          d="M1 7H14.7143"
          stroke="black"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
        <path
          d="M8.71436 1L14.7144 7L8.71436 13"
          stroke="black"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
      </svg>
      Корзина
    </h2>
    <div class="flex flex-col flex-1 justify-between">
      <div class="flex flex-col gap-5">
        <CartItemComp
          v-for="item in cartItems"
          :key="item.product.Id"
          :product="item.product"
          :count="item.count"
        />
      </div>

      <div>
        <div class="flex flex-col gap-5">
          <div
            v-if="this.customer.Discount > 0 && this.totalPrice > 0"
            class="flex items-end gap-2"
          >
            <span>Скидка {{ this.customer.Discount }} %:</span>
            <div class="flex-1 border-b border-dashed" />
            <span class="font-bold">{{ this.discount }}руб.</span>
          </div>

          <div v-if="this.totalPrice > 0" class="flex items-end gap-2">
            <span>Итого:</span>
            <div class="flex-1 border-b border-dashed" />
            <span class="font-bold"
              >{{
                this.customer.Discount > 0 ? this.totalPrice - this.discount : this.totalPrice
              }}
              руб.</span
            >
          </div>
        </div>

        <button
          v-if="this.totalPrice > 0"
          class="flex justify-center items-center gap-3 w-full py-3 mt-10 bg-blue-400 text-white rounded-xl transition active:bg-blue-600 hover:bg-blue-600"
          @click="addOrder"
        >
          Оформить заказ
        </button>
      </div>
    </div>
  </div>
</template>
