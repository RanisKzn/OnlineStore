<script>
import { defineComponent } from "vue";
import CustomersPage from "@/components/CustomersPage.vue";
import OrdersPage from "@/components/OrdersPage.vue";
import ProductsPage from "@/components/ProductsPage.vue";
import TabMenu from "@/components/TabMenu.vue";

export default defineComponent({
  components: {
    TabMenu,
    CustomersPage,
    OrdersPage,
    ProductsPage,
  },
  data() {
    return {
      currentComponent: "",
      AdminTabs: [
        { name: "Заказчики", component: "CustomersPage" },
        { name: "Заказы", component: "OrdersPage" },
        { name: "Товары", component: "ProductsPage" },
      ],
      CustomerTabs: [
        { name: "Заказы", component: "OrdersPage" },
        { name: "Товары", component: "ProductsPage" },
      ],
      role: "",
    };
  },
  async created() {
    this.role = localStorage.getItem("role");
    this.currentComponent =
      this.role === "Customer" ? this.CustomerTabs[0].component : this.AdminTabs[0].component;
  },
  computed: {
    currentTabs() {
      return this.role === "Customer" ? this.CustomerTabs : this.AdminTabs;
    },
  },
  methods: {
    setCurrentComponent(component) {
      this.currentComponent = component;
    },
  },
});
</script>

<template>
  <div class="bg-white w-3/5 m-auto rounded-xl shadow-xl shadow-grey-200 mt-20">
    <TabMenu :tabs="currentTabs" @tabChanged="setCurrentComponent" />

    <div class="px-10 py-5 bg-white shadow-md">
      <component :is="currentComponent" />
    </div>
  </div>
</template>
