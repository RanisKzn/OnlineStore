<template>
  <div>
    <div v-if="loading" class="loading">
      Loading... Please refresh once the ASP.NET backend has started.
    </div>

    <div>
      <h1>Customers</h1>
      <ul>
        <li v-for="customer in customers" :key="customer.id">
          {{ customer.Name }} - {{ customer.Code }}
        </li>
      </ul>
    </div>
  </div>
  
</template>

<script>
import { getCustomers } from '../services/api';
import { defineComponent } from 'vue';

export default defineComponent({
  data() {
    return {
      loading: false,
      customers: []
    };
  },
  async created() {
    await this.fetchData();
  },
  watch: {
    '$route': 'fetchData'
  },
  methods: {
    async fetchData() {
      this.loading = true;
      this.customers = await getCustomers();
      console.log(this.customers);
      this.loading = false;
    }
  }
});

</script>
