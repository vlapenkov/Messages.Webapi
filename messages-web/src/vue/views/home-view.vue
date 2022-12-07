<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div>
    <div class="flex flex-row justify-space-between align-items-center mt-2">
      <div class="col-1 pl-0">
        <router-link to="catalog" :style="{ textDecoration: 'none' }">
          <prime-button
            class="text-sm font-normal p-bbutton-sm p-2"
            icon="pi pi-bars"
            label="Каталог"
          />
        </router-link>
      </div>
      <div class="col-7">
        <div
          class="p-inputgroup"
          :style="{
            width: '100%',
          }"
        >
          <input-text
            type="text"
            placeholder="Найти"
            class="p-2"
            :style="{
              width: '100%',
            }"
            v-model="searchQuery"
          />
          <prime-button @click="searchMe" icon="pi pi-search"></prime-button>
        </div>
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
        <router-link to="shopping-cart" :style="{ textDecoration: 'none' }">
          <prime-button
            class="text-sm font-normal p-bbutton-sm p-2 p-button-text p-button-secondary"
          >
            <div class="flex flex-column">
              <i class="pi pi-shopping-cart"></i>
              <span>Корзина</span>
            </div>
          </prime-button>
        </router-link>
      </div>
    </div>
    <div class="flex flex-row justify-space-between align-items-center">
      <div class="col-4 pl-0">
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
    <card class="home-content-card mt-2">
      <template #content>
        <div class="grid">
          <h1 class="p-component text-xl sm:text-2xl mb-1 mt-0">Популярные категории</h1>
          <prime-divider class="mt-0"></prime-divider>
          <popular-sections-carousel class="w-full"></popular-sections-carousel>
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
      </template>
    </card>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, ref } from 'vue';
import PopularSectionsCarousel from '@/vue/containers/sections/popular-sections-carousel.vue';
import PopularProductsList from '@/vue/containers/products/popular-products-list.vue';
import PopularOrganizationsList from '@/vue/containers/organizations/popular-organizations-list.vue';
import { productShortsService } from '@/app/product-shorts/services/product-shorts.service';
import { useRouter } from 'vue-router';
import { useOrganizations } from './composables/organizations.composable';
import { useSections } from './composables/sections.composable';

export default defineComponent({
  components: {
    PopularSectionsCarousel,
    PopularProductsList,
    PopularOrganizationsList,
  },
  setup() {
    const router = useRouter();
    onMounted(async () => {
      await productShortsService.loadPage({
        name: null,
        catalogSectionId: undefined,
        pageNumber: 1,
        pageSize: 12,
        producerName: null,
        region: null,
      });
    });
    const hasPhoto = ref(false);
    const sectionModel = ref();
    const regionModel = ref();
    const organizationModel = ref();
    const searchQuery = ref<string>();

    const { organizations: organizationOptions, regions: regionOptions } = useOrganizations();
    const sections = useSections();
    const sectionOptions = computed(() =>
      (sections.value ?? []).map((s) => ({ label: s.name, value: s.id })),
    );

    const searchMe = () => {
      console.log({
        sectionId: sectionModel.value,
        region: regionModel.value,
        organization: organizationModel.value,
        searchQuery: searchQuery.value,
      });

      router.push({
        name: 'catalog',
        query: {
          sectionId: sectionModel.value?.value,
          region: regionModel.value,
          organization: organizationModel.value,
          searchQuery: searchQuery.value,
        },
      });
    };

    return {
      sectionModel,
      regionModel,
      organizationModel,
      hasPhoto,
      sectionOptions,
      regionOptions,
      organizationOptions,
      searchMe,
      searchQuery,
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
