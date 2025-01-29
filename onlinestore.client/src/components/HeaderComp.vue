<template>
  <header class="flex justify-between border-b border-slate-200 px-10 py-8">
    <ul class="flex items-center gap-4">
      <li class="flex items-center gap-3 cursor-pointer">
        <span>Товары</span>
      </li>
      <li class="flex items-center gap-3 cursor-pointer">
        <span>Заказы</span>
      </li>
      <li class="flex items-center gap-3 cursor-pointer">
        <span>Пользователи</span>
      </li>
    </ul>
    <div class="flex items-center gap-4">
      <span>Роль</span>
      <span>Текущий пользователь</span>
      <span class="cursor-pointer">Выход</span>
    </div>
  </header>
</template>

<script>
export default {
  name: "HeaderComp",
};
</script>

<style scoped></style>
