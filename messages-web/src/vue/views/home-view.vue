<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div class="home-content-card">
    <div class="grid mt-4">
      <div class="col-12">
        <app-text mode="header" class="p-component">Популярные категории</app-text>
        <prime-divider class="mt-2"></prime-divider>
        <popular-sections-carousel class="w-full"></popular-sections-carousel>
      </div>
      <div class="col-12 mt-5">
        <app-text mode="header" class="p-component">Популярные товары</app-text>
        <prime-divider class="mt-2"></prime-divider>
        <popular-products-list></popular-products-list>
      </div>
      <div class="col-12 mt-5">
        <app-text mode="header" class="p-component">Производители</app-text>
        <prime-divider class="mt-2"></prime-divider>
        <popular-organizations-list></popular-organizations-list>
      </div>
      <div class="col-12 mt-5">
        <app-text mode="header" class="p-component">Дайджесты</app-text>
        <prime-divider class="mt-2"></prime-divider>
        <div class="grid w-full">
          <div class="col-3">
            <card class="h-full news-card shadow-none">
              <template #content>
                <div class="w-full flex flex-row">
                  <img
                    src="/digests/d1.png"
                    alt=""
                    :style="{
                      borderRadius: '10px',
                    }"
                  />
                </div>
                <div class="w-full flex flex-row p-2">
                  <span
                    class="w-full font-semibold text-xl"
                    :style="{ overflowWrap: 'break-word' }"
                  >
                    Высокоскоростные гибридные шаговые электродвигатели
                  </span>
                </div>
                <!-- <div class="flex flex-row mt-2">
                  <span>
                    В дайджесте представлена информация о наиболее актуальных и перспективных
                    разработках в ракетно-космической отрасли на основе патентной информации.
                  </span>
                </div> -->
              </template>
            </card>
          </div>

          <div class="col-3">
            <card class="h-full news-card shadow-none">
              <template #content>
                <div class="w-full flex flex-row">
                  <img
                    src="/digests/d2.png"
                    alt=""
                    :style="{
                      borderRadius: '10px',
                    }"
                  />
                </div>
                <div class="w-full flex flex-row p-2">
                  <span
                    class="w-full font-semibold text-xl"
                    :style="{ overflowWrap: 'break-word' }"
                  >
                    Миниатюрные электромеханические устройства
                  </span>
                </div>
                <!-- <div class="flex flex-row mt-2">
                  <span>
                    В дайджесте представлена информация о наиболее актуальных и перспективных
                    разработках в ракетно-космической отрасли на основе патентной информации.
                  </span>
                </div> -->
              </template>
            </card>
          </div>

          <div class="col-3">
            <card class="h-full news-card shadow-none">
              <template #content>
                <div class="w-full flex flex-row">
                  <img
                    src="/digests/d3.png"
                    alt=""
                    :style="{
                      borderRadius: '10px',
                    }"
                  />
                </div>
                <div class="w-full flex flex-row p-2">
                  <span
                    class="w-full font-semibold text-xl"
                    :style="{ overflowWrap: 'break-word' }"
                  >
                    Системы и средства запуска космических аппаратов
                  </span>
                </div>
                <!-- <div class="flex flex-row mt-2">
                  <span>
                    В дайджесте представлена информация о наиболее актуальных и перспективных
                    разработках в ракетно-космической отрасли на основе патентной информации.
                  </span>
                </div> -->
              </template>
            </card>
          </div>

          <div class="col-3">
            <card class="h-full news-card shadow-none">
              <template #content>
                <div class="w-full flex flex-row">
                  <img
                    src="/digests/d4.png"
                    alt=""
                    :style="{
                      borderRadius: '10px',
                    }"
                  />
                </div>
                <div class="w-full flex flex-row p-2">
                  <span
                    class="w-full font-semibold text-xl"
                    :style="{ overflowWrap: 'break-word' }"
                  >
                    Системы квантовой связи для космических аппаратов
                  </span>
                </div>
                <!-- <div class="flex flex-row mt-2">
                  <span>
                    В дайджесте представлена информация о наиболее актуальных и перспективных
                    разработках в ракетно-космической отрасли на основе патентной информации.
                  </span>
                </div> -->
              </template>
            </card>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue';
import { productionsService } from '@/app/productions/services/productions.service';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { catalogFiltersStore } from '@/store/catalog-filters.store';

export default defineComponent({
  setup() {
    const { showFilters } = catalogFiltersStore;
    onMounted(() => {
      productionsService.loadPage({
        name: null,
        catalogSectionId: undefined,
        pageNumber: 1,
        pageSize: 12,
        producerName: null,
        region: null,
        orderBy: null,
      });
      shoppingCartStore.getDataAsync();
    });
    const hasPhoto = ref(false);

    const cartCapacity = shoppingCartStore.totalQuantity;

    return {
      hasPhoto,
      cartCapacity,
      showFilters,
    };
  },
});
</script>

<style lang="scss">
.home-content-card {
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
      padding: 0 !important;
    }

    .p-card-body {
      padding: 0 !important;
    }

    :deep(.p-card-content) {
      padding: 0 !important;
    }

    .p-card-content {
      padding: 0 !important;
    }

    .custom-button-text {
      padding: 0;
    }
  }
}
</style>
