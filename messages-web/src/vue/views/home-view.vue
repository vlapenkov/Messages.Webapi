<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div>
    <div class="grid mt-4">
      <div class="col-12">
        <h1 class="p-component text-xl sm:text-2xl mb-1 mt-0">Популярные категории</h1>
        <prime-divider class="mt-0"></prime-divider>
        <popular-sections-carousel class="w-full"></popular-sections-carousel>
      </div>
      <div class="col-12 mt-5">
        <h1 class="p-component text-xl sm:text-2xl mb-1">Популярные товары</h1>
        <prime-divider class="mt-0"></prime-divider>
        <popular-products-list></popular-products-list>
      </div>
      <div class="col-12 mt-5">
        <h1 class="p-component text-xl sm:text-2xl mb-1">Производители</h1>
        <prime-divider class="mt-0"></prime-divider>
        <popular-organizations-list></popular-organizations-list>
      </div>
      <div class="col-12 mt-5">
        <h1 class="p-component text-xl sm:text-2xl mb-1">Дайджесты</h1>
        <prime-divider class="mt-0"></prime-divider>
        <div class="grid w-full h-full">
          <div class="col-3">
            <card class="h-full news-card p-3 shadow-none">
              <template #content>
                <div class="w-full flex flex-row">
                  <span
                    class="w-full font-semibold text-xl"
                    :style="{ overflowWrap: 'break-word' }"
                  >
                    Высокоскоростные гибридные шаговые электродвигатели
                  </span>
                </div>
                <div class="flex flex-row mt-2">
                  <span>
                    В дайджесте представлена информация о наиболее актуальных и перспективных
                    разработках в ракетно-космической отрасли на основе патентной информации.
                  </span>
                </div>
              </template>
            </card>
          </div>

          <div class="col-3">
            <card class="h-full news-card p-3 shadow-none">
              <template #content>
                <div class="w-full flex flex-row">
                  <span
                    class="w-full font-semibold text-xl"
                    :style="{ overflowWrap: 'break-word' }"
                  >
                    Миниатюрные электромеханические устройства
                  </span>
                </div>
                <div class="flex flex-row mt-2">
                  <span>
                    В дайджесте представлена информация о наиболее актуальных и перспективных
                    разработках в ракетно-космической отрасли на основе патентной информации.
                  </span>
                </div>
              </template>
            </card>
          </div>

          <div class="col-3">
            <card class="h-full news-card p-3 shadow-none">
              <template #content>
                <div class="w-full flex flex-row">
                  <span
                    class="w-full font-semibold text-xl"
                    :style="{ overflowWrap: 'break-word' }"
                  >
                    Системы и средства запуска космических аппаратов
                  </span>
                </div>
                <div class="flex flex-row mt-2">
                  <span>
                    В дайджесте представлена информация о наиболее актуальных и перспективных
                    разработках в ракетно-космической отрасли на основе патентной информации.
                  </span>
                </div>
              </template>
            </card>
          </div>

          <div class="col-3">
            <card class="h-full news-card p-3 shadow-none">
              <template #content>
                <div class="w-full flex flex-row">
                  <span
                    class="w-full font-semibold text-xl"
                    :style="{ overflowWrap: 'break-word' }"
                  >
                    Системы квантовой связи для космических аппаратов
                  </span>
                </div>
                <div class="flex flex-row mt-2">
                  <span>
                    В дайджесте представлена информация о наиболее актуальных и перспективных
                    разработках в ракетно-космической отрасли на основе патентной информации.
                  </span>
                </div>
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
  .p-card-content {
    padding-top: 0;
  }

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
}
</style>
