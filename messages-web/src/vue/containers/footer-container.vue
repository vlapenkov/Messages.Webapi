<template>
  <div class="w-full bg-gray-100">
    <app-container>
      <div class="w-full flex flex-column mt-3 mb-4">
        <div class="w-full grid">
          <div class="col-offset-6 col-3">
            <div class="flex flex-column gap-2">
              <span class="p-component text-500 cursor-pointer"> Информация для покупателей </span>
              <span class="p-component text-500 cursor-pointer">
                Информация для производителей
              </span>
            </div>
          </div>
          <div class="col-3">
            <div class="flex flex-column gap-2">
              <span class="w-full p-component text-500 cursor-pointer">Доставка</span>
              <span class="p-component text-500 cursor-pointer">Оплата</span>
              <span class="p-component text-500 cursor-pointer">Контакты</span>
            </div>
          </div>
        </div>
        <div>
          <span class="p-component text-500 text-sm ml-1">
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
