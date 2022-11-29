<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div>
    <div class="flex flex-row justify-space-between align-items-center mt-2">
      <div class="col-1 pl-0">
        <prime-button
          class="text-sm font-normal p-bbutton-sm p-2"
          icon="pi pi-bars"
          label="Каталог"
        />
      </div>
      <div class="col-7">
        <span
          class="p-input-icon-right"
          :style="{
            width: '100%',
          }"
        >
          <i class="pi pi-search" />
          <input-text
            type="text"
            placeholder="Найти"
            class="p-2"
            :style="{
              width: '100%',
            }"
          />
        </span>
      </div>
      <div class="col-1">
        <prime-button class="text-sm font-normal p-bbutton-sm p-2 p-button-text p-button-secondary">
          <div class="flex flex-column">
            <i class="pi pi-heart"></i>
            <span>Избранное</span>
          </div>
        </prime-button>
      </div>
      <div class="col-1">
        <prime-button class="text-sm font-normal p-bbutton-sm p-2 p-button-text p-button-secondary">
          <div class="flex flex-column">
            <i class="pi pi-chart-bar"></i>
            <span>Сравнение</span>
          </div>
        </prime-button>
      </div>
      <div class="col-1">
        <prime-button class="text-sm font-normal p-bbutton-sm p-2 p-button-text p-button-secondary">
          <div class="flex flex-column">
            <i
              class="pi pi-sort-alt"
              :style="{
                transform: 'rotate(90deg)',
              }"
            ></i>
            <span>Аналоги</span>
          </div>
        </prime-button>
      </div>
      <div class="col-1">
        <prime-button class="text-sm font-normal p-bbutton-sm p-2 p-button-text p-button-secondary">
          <div class="flex flex-column">
            <i class="pi pi-shopping-cart"></i>
            <span>Корзина</span>
          </div>
        </prime-button>
      </div>
    </div>
    <div class="flex flex-row justify-space-between align-items-center">
      <div class="col-4 pl-0">
        <dropdown placeholder="Область применения" :style="{ width: '100%' }" />
      </div>
      <div class="col-4">
        <dropdown placeholder="Регион" :style="{ width: '100%' }" />
      </div>
      <div class="col-4">
        <dropdown placeholder="Производитель" :style="{ width: '100%' }" />
      </div>
    </div>
    <div class="flex flex-row justify-space-between align-items-center">
      <div class="col-6 pl-0">
        <checkbox v-model="hasPhoto" inputId="chkbox1" :binary="true" />
        <label
          for="chkbox1"
          :style="{
            color: '#64748B',
            fontFamily:
              '-apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Helvetica, Arial, sans-serif, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol',
          }"
        >
          Наличие фото
        </label>
      </div>
      <div class="col-6">
        <div class="flex flex-row justify-content-end">
          <prime-button
            class="text-sm font-normal p-bbutton-sm p-button-text p-button-secondary p-2"
            icon="pi pi-sliders-h"
            label="Расширенный поиск"
          />
        </div>
      </div>
    </div>
    <div class="grid">
      <h1 class="p-component text-xl sm:text-2xl mb-1">Популярные категории</h1>
      <prime-divider class="mt-0"></prime-divider>
      <popular-categories-carousel></popular-categories-carousel>
    </div>
    <div class="grid">
      <h1 class="p-component text-xl sm:text-2xl mb-1">Популярные товары</h1>
      <prime-divider class="mt-0"></prime-divider>
      <popular-goods-list></popular-goods-list>
    </div>
    <div class="grid">
      <h1 class="p-component text-xl sm:text-2xl mb-1">Производители</h1>
      <prime-divider class="mt-0"></prime-divider>
      <manufacturers-list></manufacturers-list>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import PopularCategoriesCarousel from '@/components/PopularCategories/popular-categories-carousel.vue';
import PopularGoodsList from '@/components/PopularGoods/popular-goods-list.vue';
import ManufacturersList from '@/components/Manufacturers/manufacturers-list.vue';

export default defineComponent({
  components: {
    PopularCategoriesCarousel,
    PopularGoodsList,
    ManufacturersList,
  },
  setup() {
    const hasPhoto = ref(false);
    return {
      hasPhoto,
    };
  },
});
</script>

<style scoped></style>
