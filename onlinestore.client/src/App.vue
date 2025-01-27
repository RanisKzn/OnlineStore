<template>
  <div id="app">
    <div class="container">
      <Sidebar />
      <MainContent />
    </div>
  </div>
</template>

<script>
  import Sidebar from './components/Sidebar.vue';
  import MainContent from './components/MainContent.vue';

  export default {
    name: 'App',
    components: {
      Sidebar,
      MainContent
    }
  }
</script>

<style>
  .container {
    display: flex;
    height: 100vh;
  }
</style>
