<script>
import { getCategory } from "../services/api";
export default {
  props: {
    product: Object,
    count: Number,
  },
  data() {
    return {
      category: [],
    };
  },
  async created() {
    this.category = await getCategory(this.product.CategoryId);
  },
  methods: {
    handleClose() {
      this.$emit("close");
    },
  },
};
</script>

<template>
  <div class="relative flex w-full border border-slate-100 rounded-xl p-4 gap-4">
    <div class="flex flex-col w-full">
      <span class="font-bold">Имя: {{ this.product.Name }}</span>
      <p>Код: {{ product.Code }}</p>
      <p>
        Категория:
        {{ category.Name }}
      </p>
      <p>
        Кол-во:
        {{ this.count }}
      </p>
      <p>
        <b>Цена: {{ Number(product.Price) * this.count }} руб.</b>
      </p>
    </div>
  </div>
</template>
