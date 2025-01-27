
<script setup>
  import CustomerList from './CustomerList.vue';

</script>

<template>
  <div class="main-content">
    <div class="toolbar">
      <button>Кнопка</button>
      <button>Кнопка</button>
    </div>
    <div class="content">
      <h2>Контент табличных данных с фильтром</h2>
      <div class="table-container">
        <p>Пейджинг таблицы</p>
      </div>
    </div>
    <div class="footer">
      <p>Текущие состояние объекта (если поддерживается)</p>
      <div class="current-state">
        <p>Контент полей формы (не более 3-х колонок) с группировкой по смысловой нагрузке</p>
      </div>
      <CustomerList>

      </CustomerList>
      <div class="form-actions">
        <button>Дополнительные кнопки (при необходимости)</button>
      </div>
    </div>
  </div>
</template>

<style scoped>
  .main-content {
    flex-grow: 1;
    padding: 20px;
  }

  .toolbar {
    display: flex;
    justify-content: flex-end;
    margin-bottom: 20px;
  }

    .toolbar button {
      margin-left: 10px;
      padding: 10px 20px;
      background-color: #007bff;
      color: #fff;
      border: none;
      cursor: pointer;
    }

      .toolbar button:hover {
        background-color: #0056b3;
      }

  .content {
    background-color: #fff;
    padding: 20px;
    border: 1px solid #ddd;
    margin-bottom: 20px;
  }

    .content h2 {
      margin-bottom: 20px;
    }

  .table-container {
    background-color: #f5f5f5;
    padding: 10px;
    border: 1px solid #ddd;
  }

  .footer {
    background-color: #f5f5f5;
    padding: 20px;
    border-top: 1px solid #ddd;
  }

  .current-state {
    margin-bottom: 20px;
  }

  .form-actions {
    display: flex;
    justify-content: flex-end;
  }

    .form-actions button {
      padding: 10px 20px;
      background-color: #007bff;
      color: #fff;
      border: none;
      cursor: pointer;
    }

      .form-actions button:hover {
        background-color: #0056b3;
      }
</style>
