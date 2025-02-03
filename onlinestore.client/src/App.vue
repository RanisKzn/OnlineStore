<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: "App",
  components: {},
  computed: {
    isAuthenticated() {
      return localStorage.getItem("token"); // Проверяем наличие токена
    },
  },
};
</script>

<style>
.container {
  display: flex;
  height: 100vh;
}
</style>
