<template>
  <nav class="navbar">
    <div class="navbar-brand">Название приложения</div>
    <div class="navbar-actions">
      <button @click="logout">Выход</button>
      <span>{{ currentUser }}</span>
    </div>
  </nav>
</template>

<script>
  export default {
    data() {
      return {
        currentUser: 'Иван Иванов', // Пример данных пользователя
      };
    },
    methods: {
      logout() {
        // Логика выхода из системы
        console.log('Logout');
      },
    },
  };
</script>

<style scoped>
  .navbar {
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: #f8f9fa;
    padding: 10px 20px;
  }

  .navbar-brand {
    font-size: 1.5em;
  }

  .navbar-actions {
    display: flex;
    align-items: center;
  }

    .navbar-actions button {
      margin-left: 20px;
    }
</style>
