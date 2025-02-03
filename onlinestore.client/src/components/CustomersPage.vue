<script>
import CardComp from "@/components/CustomerCardComp.vue";
import { getCustomers } from "../services/api";
import { defineComponent } from "vue";
import CustomerForm from "@/components/CustomerForm.vue";

export default defineComponent({
  components: {
    CardComp,
    CustomerForm,
  },
  data() {
    return {
      loading: false,
      customers: [],
      currentPage: 1,
      itemsPerPage: 4,
      showForm: false,
      mode: "",
      filterCriteria: "name",
      searchQuery: "",
    };
  },
  async created() {
    await this.fetchData();
  },
  watch: {
    $route: "fetchData",
    filterCriteria: "fetchData",
    searchQuery: "fetchData",
  },
  computed: {
    paginatedCustomers() {
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return this.filteredOrders.slice(start, end);
    },
    filteredOrders() {
      let filtered = this.customers;

      if (this.searchQuery) {
        filtered = filtered.filter((customer) =>
          customer.Name.toLowerCase().includes(this.searchQuery.toLowerCase())
        );
      }

      if (this.filterCriteria === "name_asc") {
        filtered.sort((a, b) => a.Name.localeCompare(b.Name));
      } else if (this.filterCriteria === "name_desc") {
        filtered.sort((a, b) => b.Name.localeCompare(a.Name));
      }

      return filtered;
    },
    totalPages() {
      return Math.ceil(this.customers.length / this.itemsPerPage);
    },
  },
  methods: {
    async fetchData() {
      this.loading = true;
      try {
        this.customers = await getCustomers();
        console.log(this.customers);
      } catch (error) {
        console.error("Error fetching customers:", error);
      }
      this.loading = false;
    },
    addCustomer() {
      this.showForm = true;
      this.mode = "add";
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
  <CustomerForm v-if="showForm" @close="handleClose" :mode="this.mode"></CustomerForm>

  <div class="flex justify-between items-center mb-10">
    <h1 class="text-3xl font-bold">Пользователи</h1>
    <div class="flex items-center gap-4">
      <select
        v-model="filterCriteria"
        class="py-2 px-3 border border-gray-200 focus:border-gray-400 rounded-md focus:outline-none"
      >
        <option value="name">По названию</option>
        <option value="name_asc">По названию (A - Я)</option>
        <option value="name_desc">По названию (Я - А)</option>
      </select>
      <div class="relative">
        <input
          v-model="searchQuery"
          type="text"
          class="border border-gray-200 rounded-md py-2 pl-10 pr-4 focus:outline-none focus:border-gray-400"
          placeholder="Поиск..."
        />
        <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
          <img src="" />
        </div>
      </div>
    </div>
  </div>
  <div class="grid gap-5" v-if="!loading">
    <CardComp
      v-for="customer in paginatedCustomers"
      :key="customer.Id"
      :customer="customer"
      @close="handleClose"
    />
  </div>
  <div v-else>Loading...</div>
  <div class="flex justify-center mt-8">
    <button
      class="px-4 py-2 bg-green-400 text-white rounded hover:bg-green-600 transition"
      @click="addCustomer"
    >
      Добавить
    </button>
  </div>
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
