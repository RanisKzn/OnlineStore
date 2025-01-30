<script>
import CustomerForm from "./CustomerForm.vue";
import { getUser } from "../services/api";
import DeleteModal from "./DeleteModal.vue";
export default {
  components: { CustomerForm, DeleteModal },
  name: "CardComp",
  props: {
    customer: Object,
  },
  data() {
    return {
      showEditForm: false,
      showDeleteForm: false,
      mode: "",
      user: {},
    };
  },
  methods: {
    async editCustomer() {
      this.user = await getUser(this.customer.UserId);
      this.mode = "edit";
      this.showEditForm = true;
    },
    openDeleteModal() {
      this.showDeleteForm = true;
    },
    handleClose() {
      this.showDeleteForm = false;
      this.showEditForm = false;
      this.$emit("close");
    },
  },
};
</script>

<template>
  <CustomerForm
    v-if="showEditForm"
    @close="handleClose"
    :mode="this.mode"
    :customerEdit="this.customer"
    :userEdit="this.user"
  ></CustomerForm>
  <DeleteModal v-if="showDeleteForm" :customer="this.customer" @close="handleClose" />
  <div
    class="relative flex flex-col w-full border border-slate-100 rounded-xl p-8 cursor-pointer transition hover:shadow-xl hover:transform hover:-translate-y-2"
  >
    <div class="flex justify-between mt-5">
      <div class="flex flex-col gap-2">
        <span class="font-bold">Имя: {{ this.customer.Name }}</span>
        <p>Код: {{ customer.Code }}</p>
        <p>
          Скидка:
          {{ customer.Discount === null ? "Не распространяется" : customer.Discount }}
        </p>
        <p>Адрес: {{ customer.Address }}</p>
      </div>
      <div class="flex gap-2 my-auto">
        <button
          class="px-4 py-2 bg-blue-400 text-white rounded hover:bg-blue-600 transition"
          @click="editCustomer"
        >
          Редактировать
        </button>
        <button
          class="px-4 py-2 bg-red-400 text-white rounded hover:bg-red-600 transition"
          @click="openDeleteModal"
        >
          Удалить
        </button>
      </div>
    </div>
  </div>
</template>
