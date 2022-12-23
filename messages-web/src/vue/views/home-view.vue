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
        <div class="flex flex-row justify-content-between align-items-end mr-1">
          <app-text mode="header" class="p-component">Популярные товары</app-text>
          <router-link class="no-underline" :to="{ name: 'catalog' }">
            <div class="flex flex-row gap-2 align-items-center">
              <div class="pb-1">
                <app-text class="p-component" mode="primary"> Все товары и услуги </app-text>
              </div>
              <div>
                <app-text tag="i" class="pi pi-arrow-right" mode="primary"> </app-text>
              </div>
            </div>
          </router-link>
        </div>
        <prime-divider class="mt-2"></prime-divider>
        <popular-products-list></popular-products-list>
      </div>
      <div class="col-12 mt-5">
        <div class="flex flex-row justify-content-between align-content-end">
          <app-text mode="header" class="p-component">Производители</app-text>
          <router-link v-if="isContentManager" class="no-underline" :to="{ name: 'organizations' }">
            <div class="flex flex-row gap-2 align-items-center">
              <div class="pb-1">
                <app-text class="p-component" mode="primary"> Все производители </app-text>
              </div>
              <div>
                <app-text tag="i" class="pi pi-arrow-right" mode="primary"> </app-text>
              </div>
            </div>
          </router-link>
        </div>

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
import { computed, defineComponent, ref } from 'vue';
import { catalogFiltersStore } from '@/store/catalog-filters.store';
import { userInfo } from '@/store/user.store';
import { useCartTotalQuantity } from '@/composables/shopping-cart.composables';

export default defineComponent({
  setup() {
    const { showFilters } = catalogFiltersStore;

    const hasPhoto = ref(false);

    const cartCapacity = useCartTotalQuantity();

    const isContentManager = computed(() =>
      userInfo.value?.role.find((r) => r === 'content_manager'),
    );

    return {
      hasPhoto,
      cartCapacity,
      showFilters,
      isContentManager,
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
