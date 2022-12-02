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
          <i class="pi pi-search"></i>
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
    <div class="grid mt-2">
      <h1 class="p-component text-xl sm:text-2xl mb-1">Популярные категории</h1>
      <prime-divider class="mt-0"></prime-divider>
      <popular-sections-carousel></popular-sections-carousel>
    </div>
    <div class="grid mt-5">
      <h1 class="p-component text-xl sm:text-2xl mb-1">Популярные товары</h1>
      <prime-divider class="mt-0"></prime-divider>
      <popular-products-list></popular-products-list>
    </div>
    <div class="grid mt-5">
      <h1 class="p-component text-xl sm:text-2xl mb-1">Производители</h1>
      <prime-divider class="mt-0"></prime-divider>
      <popular-organizations-list></popular-organizations-list>
    </div>
    <div class="grid mt-5">
      <h1 class="p-component text-xl sm:text-2xl mb-1">Дайджесты</h1>
      <prime-divider class="mt-0"></prime-divider>
      <div class="grid w-full">
        <div class="col-3">
          <card class="news-card p-3">
            <template #title>
              <span class="font-semibold">Высокоскоростные гибридные шаговые электродвигатели</span>
            </template>
            <template #content>
              В дайджесте представлена информация о наиболее актуальных и перспективных разработках
              в ракетно-космической отрасли на основе патентной информации.
            </template>
          </card>
        </div>

        <div class="col-3">
          <card class="news-card p-3">
            <template #title>
              <span class="font-semibold">Миниатюрные электромеханические устройства</span>
            </template>
            <template #content>
              В дайджесте представлена информация о наиболее актуальных и перспективных разработках
              в ракетно-космической отрасли на основе патентной информации.
            </template>
          </card>
        </div>

        <div class="col-3">
          <card class="news-card p-3">
            <template #title>
              <span class="font-semibold">Системы и средства запуска космических аппаратов</span>
            </template>
            <template #content>
              В дайджесте представлена информация о наиболее актуальных и перспективных разработках
              в космической отрасли на основе патентной информации.
            </template>
          </card>
        </div>

        <div class="col-3">
          <card class="news-card p-3">
            <template #title>
              <span class="font-semibold">Системы квантовой связи для космических аппаратов</span>
            </template>
            <template #content>
              В дайджесте представлена информация о наиболее актуальных и перспективных разработках
              в ракетно-космической отрасли на основе патентной информации.
            </template>
          </card>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue';
import PopularSectionsCarousel from '@/vue/containers/sections/popular-sections-carousel.vue';
import PopularProductsList from '@/vue/containers/products/popular-products-list.vue';
import PopularOrganizationsList from '@/vue/containers/organizations/popular-organizations-list.vue';
import { productShortsService } from '@/app/product-shorts/services/product-shorts.service';
import { organizationsService } from '@/app/organizations/services/organization.service';

export default defineComponent({
  components: {
    PopularSectionsCarousel,
    PopularProductsList,
    PopularOrganizationsList,
  },
  setup() {
    onMounted(async () => {
      await productShortsService.loadPage({
        name: null,
        catalogSectionId: undefined,
        pageNumber: 1,
        pageSize: 12,
      });
      await organizationsService.loadPage({
        pageNumber: 1,
        pageSize: 8,
      });
    });
    const hasPhoto = ref(false);
    return {
      hasPhoto,
    };
  },
});
</script>

<style lang="scss">
.news-card {
  :deep(.p-card-header) {
    line-height: 0;
    text-align: center;
  }

  .p-card-header {
    line-height: 0 !important;
    text-align: center !important;
  }

  :deep(.p-card-body) {
    padding: 0;
  }

  .p-card-body {
    padding: 0 !important;
  }

  :deep(.p-card-content) {
    padding-bottom: 0;
    padding-top: 0;
  }

  .p-card-content {
    padding-bottom: 0 !important;
    padding-top: 0 !important;
  }

  .custom-button-text {
    padding: 0;
  }
}
</style>
