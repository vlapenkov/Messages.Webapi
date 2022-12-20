<template>
  <div
    class="w-full"
    :style="{
      backgroundColor: '#f4f4fb',
    }"
  >
    <app-container>
      <div class="w-full flex flex-column mt-3 mb-4">
        <div class="w-full flex flex-row justify-content-end align-items-top gap-6">
          <div class="flex flex-column gap-2">
            <span class="p-component text-500 cursor-pointer"> Информация для покупателей </span>
            <span class="p-component text-500 cursor-pointer"> Информация для производителей </span>
          </div>
          <div class="flex flex-column gap-2">
            <span class="p-component text-500 cursor-pointer">Доставка</span>
            <span class="p-component text-500 cursor-pointer">Оплата</span>
            <span class="p-component text-500 cursor-pointer">Контакты</span>
          </div>
        </div>
        <prime-divider class="mt-4 mb-3"></prime-divider>
        <div>
          <span class="p-component text-500 text-sm">
            © {{ new Date().getFullYear() }} Маркетплейс продукции гражданского назначения
          </span>
        </div>
      </div>
    </app-container>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return {};
  },
});
</script>

<style scoped></style>
