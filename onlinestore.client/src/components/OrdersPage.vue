<script>
import CardComp from "@/components/OrderCardComp.vue";
import { getOrders, getStatuses } from "../services/api";
import { defineComponent } from "vue";

export default defineComponent({
  components: {
    CardComp,
  },
  data() {
    return {
      loading: false,
      orders: [],
      currentPage: 1,
      itemsPerPage: 4,
      showForm: false,
      mode: "",
      customer: {},
      filterCriteria: "name",
      searchQuery: "",
      statuses: {},
    };
  },
  async created() {
    this.customer = JSON.parse(localStorage.getItem("customer"));
    this.statuses = await getStatuses();
    await this.fetchData();
  },
  watch: {
    $route: "fetchData",
    filterCriteria: "fetchData",
    searchQuery: "fetchData",
  },
  computed: {
    paginatedOrders() {
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return this.filteredOrders.slice(start, end);
    },
    filteredOrders() {
      let filtered = this.orders;

      if (this.searchQuery) {
        filtered = filtered.filter((order) =>
          String(order.OrderNumber).toLowerCase().includes(this.searchQuery.toLowerCase())
        );
      }

      if (this.filterCriteria === "status_new") {
        filtered = filtered.filter(
          (order) => order.StatusId === this.statuses.find((status) => status.Name === "Новый").Id
        );
      } else if (this.filterCriteria === "status_in_progress") {
        filtered = filtered.filter(
          (order) =>
            order.StatusId === this.statuses.find((status) => status.Name === "Выполняется").Id
        );
      } else if (this.filterCriteria === "status_completed") {
        filtered = filtered.filter(
          (order) =>
            order.StatusId === this.statuses.find((status) => status.Name === "Выполнен").Id
        );
      }

      return filtered;
    },
    totalPages() {
      return Math.ceil(this.orders.length / this.itemsPerPage);
    },
  },
  methods: {
    async fetchData() {
      this.loading = true;
      try {
        if (this.customer != null) {
          const allOrders = await getOrders();
          this.orders = allOrders.filter((order) => order.CustomerId === this.customer.Id);
        } else {
          this.orders = await getOrders();
        }
      } catch (error) {
        console.error("Error fetching orders:", error);
      }
      this.loading = false;
    },
    handleClose() {
      this.showForm = false;
      this.fetchData();
    },
    goToPage(page) {
      if (page >= 1 && page <= this.totalPages) {
        this.currentPage = page;
      }
    },
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
    firstPage() {
      this.currentPage = 1;
    },
    lastPage() {
      this.currentPage = this.totalPages;
    },
  },
});
</script>
<template>
  <div class="flex justify-between items-center mb-10">
    <h1 class="text-3xl font-bold">Заказы</h1>
    <div class="flex items-center gap-4">
      <select
        v-model="filterCriteria"
        class="py-2 px-3 border border-gray-200 focus:border-gray-400 rounded-md focus:outline-none"
      >
        <option value="name">По названию</option>
        <option value="status_new">По статусу (Новый)</option>
        <option value="status_in_progress">По статусу (Выполняется)</option>
        <option value="status_completed">По статусу (Выполнен)</option>
      </select>
      <div class="relative">
        <input
          v-model="searchQuery"
          type="text"
          class="border border-gray-200 rounded-md py-2 pl-10 pr-4 focus:outline-none focus:border-gray-400"
          placeholder="Поиск..."
        />
      </div>
    </div>
  </div>
  <div class="grid gap-5" v-if="!loading">
    <CardComp
      v-for="order in paginatedOrders"
      :key="order.Id"
      :order="order"
      @close="handleClose"
    />
  </div>
  <div v-else>Loading...</div>
  <div class="flex justify-center mt-4">
    <button
      class="px-4 py-2 bg-gray-300 text-gray-700 rounded hover:bg-gray-400 transition"
      @click="firstPage"
      :disabled="currentPage === 1"
    >
      &lt;&lt;
    </button>
    <button
      class="px-4 py-2 bg-gray-300 text-gray-700 rounded hover:bg-gray-400 transition"
      @click="prevPage"
      :disabled="currentPage === 1"
    >
      &lt;
    </button>
    <span v-for="page in totalPages" :key="page">
      <button
        class="px-4 py-2 rounded transition"
        :class="
          currentPage === page
            ? 'bg-blue-500 text-white hover:bg-blue-600'
            : 'bg-gray-300 text-gray-700 hover:bg-gray-400'
        "
        @click="goToPage(page)"
      >
        {{ page }}
      </button>
    </span>
    <button
      class="px-4 py-2 bg-gray-300 text-gray-700 rounded hover:bg-gray-400 transition"
      @click="nextPage"
      :disabled="currentPage === totalPages"
    >
      &gt;
    </button>
    <button
      class="px-4 py-2 bg-gray-300 text-gray-700 rounded hover:bg-gray-400 transition"
      @click="lastPage"
      :disabled="currentPage === totalPages"
    >
      &gt;&gt;
    </button>
  </div>
</template>
