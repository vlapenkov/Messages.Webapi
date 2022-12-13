<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div class="relative">
    <transition-fade>
      <div v-if="showFilters" class="absolute w-full z-1">
        <card class="shadow-7">
          <template #title>
            <div class="flex flex-row justify-content-end">
              <prime-button
                class="p-button-sm p-button-text text-color"
                icon="pi pi-times"
                label="Сбросить"
                @click="showFilters = false"
              ></prime-button>
            </div>
          </template>
          <template #content>
            <div class="grid">
              <div class="col-4">
                <dropdown
                  v-model="sectionModel"
                  :options="sectionOptions"
                  optionLabel="label"
                  placeholder="Область применения"
                  show-clear
                  :style="{ width: '100%' }"
                />
              </div>
              <div class="col-4">
                <dropdown
                  v-model="regionModel"
                  :options="regionOptions"
                  placeholder="Регион"
                  show-clear
                  :style="{ width: '100%' }"
                />
              </div>
              <div class="col-4">
                <dropdown
                  v-model="organizationModel"
                  :options="organizationOptions"
                  placeholder="Производитель"
                  show-clear
                  :style="{ width: '100%' }"
                />
              </div>
              <div class="col-12 mt-1 flex flex-row gap-4">
                <div class="field-radiobutton">
                  <radio-button inputId="status-available" name="city" value="Chicago" />
                  <label for="status-available">В наличии</label>
                </div>
                <div class="field-radiobutton">
                  <radio-button inputId="status-orderable" name="city" value="Los Angeles" />
                  <label for="status-orderable">Под заказ</label>
                </div>
              </div>
            </div>
          </template>
          <template #footer>
            <div class="flex flex-row justify-content-end">
              <prime-button @click="searchForProducts" label="Применить"></prime-button>
            </div>
          </template>
        </card>
      </div>
    </transition-fade>

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
            <card class="h-full news-card p-3">
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
            <card class="h-full news-card p-3">
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
            <card class="h-full news-card p-3">
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
            <card class="h-full news-card p-3">
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
import PopularSectionsCarousel from '@/vue/containers/sections/popular-sections-carousel.vue';
import PopularProductsList from '@/vue/containers/products/popular-products-list.vue';
import PopularOrganizationsList from '@/vue/containers/organizations/popular-organizations-list.vue';
import { productionsService } from '@/app/productions/services/productions.service';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { catalogFiltersStore } from '@/store/catalog-filters.store';
import { useCatalogFilters } from '@/composables/catalog-filters.composable';

export default defineComponent({
  components: {
    PopularSectionsCarousel,
    PopularProductsList,
    PopularOrganizationsList,
  },
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

    const {
      sectionModel,
      regionModel,
      organizationModel,
      sectionOptions,
      organizationOptions,
      regionOptions,
      searchQuery,
      searchForProducts,
    } = useCatalogFilters();

    return {
      sectionModel,
      regionModel,
      organizationModel,
      hasPhoto,
      sectionOptions,
      regionOptions,
      organizationOptions,
      searchForProducts,
      searchQuery,
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
