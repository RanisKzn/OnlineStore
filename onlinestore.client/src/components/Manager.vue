<template>
  <div>
    <h1>Manager Dashboard</h1>
    <Navigation @filter="filterItems" />
    <div>
      <h2>Products</h2>
      <ul>
        <li v-for="product in filteredProducts" :key="product.id">
          {{ product.name }} - {{ product.price }}
        </li>
      </ul>
      <button @click="addProduct">Add Product</button>
    </div>
    <div>
      <h2>Users</h2>
      <ul>
        <li v-for="user in users" :key="user.id">
          {{ user.username }}
        </li>
      </ul>
      <button @click="addUser">Add User</button>
    </div>
    <div>
      <h2>Orders</h2>
      <ul>
        <li v-for="order in orders" :key="order.id">
          {{ order.orderNumber }} - {{ order.status }}
          <button @click="confirmOrder(order.id)">Confirm</button>
          <button @click="closeOrder(order.id)">Close</button>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
  import { getProducts, getUsers, getOrders, addProduct, addUser, confirmOrder, closeOrder } from '../services/api';

  export default {
    components: {
     
    },
    data() {
      return {
        products: [],
        users: [],
        orders: [],
        searchQuery: ''
      };
    },
    async created() {
      this.products = await getProducts();
      this.users = await getUsers();
      this.orders = await getOrders();
    },
    methods: {
      async addProduct() {
        await addProduct({ name: 'New Product', price: 100 });
        this.products = await getProducts();
      },
      async addUser() {
        await addUser({ username: 'New User', password: 'password' });
        this.users = await getUsers();
      },
      async confirmOrder(orderId) {
        await confirmOrder(orderId);
        this.orders = await getOrders();
      },
      async closeOrder(orderId) {
        await closeOrder(orderId);
        this.orders = await getOrders();
      },
      filterItems(query) {
        this.searchQuery = query;
      }
    },
    computed: {
      filteredProducts() {
        if (!this.searchQuery) return this.products;
        return this.products.filter(product => product.name.includes(this.searchQuery));
      }
    }
  };
</script>

<style scoped>
  h2 {
    margin-top: 20px;
  }

  button {
    margin-top: 10px;
  }
</style>
