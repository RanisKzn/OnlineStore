<template>
  <div class="sidebar">
    <div class="section">
      <h3>Разделы форм для изменения данных (могут отсутствовать)</h3>
      <button class="accordion">Раздел</button>
      <button class="accordion">Раздел</button>
    </div>
    <div class="section">
      <h3>Представленные фильтры (могут отсутствовать)</h3>
      <button class="accordion">Фильтр</button>
      <button class="accordion">Фильтр</button>
    </div>
    <div class="section">
      <h3>Дополнительная информация (может отсутствовать)</h3>
      <input type="text" placeholder="Дополнительные кнопки (при необходимости)">
      <button>OK</button>
      <button>Отмена</button>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'Sidebar'
  }
</script>

<style scoped>
  .sidebar {
    width: 250px;
    background-color: #f5f5f5;
    padding: 20px;
    box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1);
  }

  .section {
    margin-bottom: 20px;
  }

    .section h3 {
      margin-bottom: 10px;
    }

  .accordion {
    width: 100%;
    padding: 10px;
    margin-bottom: 5px;
    background-color: #fff;
    border: 1px solid #ddd;
    cursor: pointer;
  }

  input[type="text"] {
    width: calc(100% - 22px);
    padding: 10px;
    margin-bottom: 10px;
    border: 1px solid #ddd;
  }

  button {
    padding: 10px 20px;
    margin-bottom: 10px;
    background-color: #007bff;
    color: #fff;
    border: none;
    cursor: pointer;
  }

    button:hover {
      background-color: #0056b3;
    }
</style>
