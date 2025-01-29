<script>
export default {
  name: "CardComp",
  props: {
    name: String,
    code: String,
    address: String,
  },
};
</script>

<template>
  <div
    class="relative flex flex-col w-full border border-slate-100 rounded-xl p-8 cursor-pointer transition hover:shadow-xl hover:transform hover:-translate-y-2"
  >
    <div class="flex justify-between mt-5">
      <div class="flex flex-col gap-2">
        <span class="font-bold">Имя: {{ name }}</span>
        <p>Код: {{ code }}</p>
        <p>Адрес: {{ address }}</p>
      </div>
      <div class="flex gap-2 my-auto">
        <button class="px-4 py-2 bg-blue-400 text-white rounded hover:bg-blue-600 transition">
          Редактировать
        </button>
        <button class="px-4 py-2 bg-red-400 text-white rounded hover:bg-red-600 transition">
          Удалить
        </button>
      </div>
    </div>
  </div>
</template>
