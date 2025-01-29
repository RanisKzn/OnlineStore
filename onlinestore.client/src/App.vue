<template>
  <div id="app">
    <HomePage> </HomePage>
  </div>
</template>

<script>
import HomePage from "./page/HomePage.vue";

export default {
  name: "App",
  components: {
    HomePage,
  },
};
</script>

<style>
.container {
  display: flex;
  height: 100vh;
}
</style>
