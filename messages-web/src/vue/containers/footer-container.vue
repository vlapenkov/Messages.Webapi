<template>
  <div class="w-full bg-gray-100">
    <app-container>
      <div class="w-full flex flex-column mt-3 mb-4">
        <div class="w-full flex flex-row justify-content-between align-items-top">
          <div class="flex flex-column gap-2">
            <span class="p-component text-md text-color font-medium mb-1"> Общая информация </span>
            <span class="p-component text-sm text-500 cursor-pointer"> О маркетплейсе </span>
            <span class="p-component text-sm text-500 cursor-pointer"> Контакты </span>
            <span class="p-component text-sm text-500 cursor-pointer"> Реквизиты </span>
          </div>
          <div class="flex flex-column gap-2">
            <span class="p-component text-md text-color font-medium mb-1">
              Информация для покупателей
            </span>
            <span class="p-component text-sm text-500 cursor-pointer"> Как выбрать товар </span>
            <span class="p-component text-sm text-500 cursor-pointer"> Доставка </span>
            <span class="p-component text-sm text-500 cursor-pointer"> Оплата</span>
            <span class="p-component text-sm text-500 cursor-pointer"> Обратная связь </span>
          </div>
          <div class="flex flex-column gap-2">
            <span class="p-component text-md text-color font-medium mb-1">
              Информация для производителей
            </span>
            <span class="p-component text-sm text-500 cursor-pointer"> Размещение организации</span>
            <span class="p-component text-sm text-500 cursor-pointer"> Обмен </span>
            <span class="p-component text-sm text-500 cursor-pointer"> Интеграции </span>
            <span class="p-component text-sm text-500 cursor-pointer"> Правила продажи </span>
          </div>
          <div class="flex flex-column gap-2">
            <span class="p-component text-md text-color font-medium mb-1"> Помощь </span>
            <span class="p-component text-sm text-500 cursor-pointer">Как сделать заказ</span>
            <span class="p-component text-sm text-500 cursor-pointer">Безопасность</span>
            <span class="p-component text-sm text-500 cursor-pointer">
              Условия обработки данных
            </span>
            <span class="p-component text-sm text-500 cursor-pointer">Горячая линия</span>
          </div>
        </div>
        <prime-divider class="mt-5 mb-3"></prime-divider>
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
