<script>
import CartComp from "./CartComp.vue";

export default {
  components: { CartComp },
  props: {
    tabs: Array,
  },
  data() {
    return {
      activeTabIndex: 0,
      customer: {},
      name: [],
      role: [],
      showBusket: false,
    };
  },
  async created() {
    this.role = localStorage.getItem("role");
    if (this.role === "Customer") {
      this.customer = JSON.parse(localStorage.getItem("customer"));
      this.name = this.customer.Name;
    } else {
      this.name = localStorage.getItem("name");
    }
    this.getCartItemCount();
  },
  methods: {
    selectTab(index) {
      this.activeTabIndex = index;
      this.$emit("tabChanged", this.tabs[index].component);
    },
    logout() {
      localStorage.removeItem("token");
      localStorage.removeItem("role");
      localStorage.removeItem("cart");
      localStorage.removeItem("customer");
      this.$router.push("/login");
    },
    openBusket() {
      this.showBusket = !this.showBusket;
    },
    handleClose() {
      this.showBusket = false;
    },
  },
};
</script>

<template>
  <CartComp v-if="showBusket" @close="handleClose"></CartComp>
  <header class="flex justify-between px-10 py-8">
    <ul class="flex items-center gap-4">
      <li class="flex items-center gap-3 cursor-pointer">
        <span
          v-for="(tab, index) in tabs"
          :key="index"
          @click="selectTab(index)"
          :class="[
            'px-4 py-2 rounded-t-lg cursor-pointer ',
            activeTabIndex === index
              ? 'bg-blue-400 text-white'
              : 'bg-gray-300 text-gray-700 hover:bg-gray-400 transition',
          ]"
        >
          {{ tab.name }}</span
        >
      </li>
    </ul>
    <div class="flex items-center gap-4">
      <span
        v-if="role === 'Customer'"
        class="px-2 cursor-pointer hover:bg-gray-400 rounded transition"
        @click="openBusket"
        >Корзина
      </span>
      <span>{{ this.role }}: {{ this.name }} </span>
      <span class="px-2 cursor-pointer hover:bg-gray-400 rounded transition" @click="logout"
        >Выход</span
      >
    </div>
  </header>
</template>
