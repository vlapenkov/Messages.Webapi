<template>
  <div class="pt-2">
    <div ref="searchRef" class="flex flex-row justify-content-between gap-2">
      <div class="flex flex-grow-1 flex-row justify-content-between align-items-center gap-2">
        <div>
          <router-link to="catalog" :style="{ textDecoration: 'none' }">
            <prime-button icon="pi pi-bars " label="Каталог" />
          </router-link>
        </div>
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
        <div>
          <prime-button-weak icon="pi pi-sliders-h" @click="showFilters = !showFilters" />
        </div>
      </div>
      <div class="flex flex-row justify-content-between align-items-center gap-2">
        <div>
          <prime-button
            class="text-sm font-normal p-bbutton-sm p-2 p-button-text p-button-secondary text-color"
          >
            <div class="flex flex-column">
              <i class="pi pi-heart font-semibold"></i>
              <span>Избранное</span>
            </div>
          </prime-button>
        </div>
        <div>
          <prime-button
            class="text-sm font-normal p-bbutton-sm p-2 p-button-text p-button-secondary text-color"
          >
            <div class="flex flex-column">
              <i class="pi pi-chart-bar font-semibold"></i>
              <span>Сравнение</span>
            </div>
          </prime-button>
        </div>
        <div>
          <prime-button
            class="text-sm font-normal p-bbutton-sm p-2 p-button-text p-button-secondary text-color"
          >
            <div class="flex flex-column">
              <i
                class="pi pi-sort-alt font-semibold"
                :style="{
                  transform: 'rotate(90deg)',
                }"
              ></i>
              <span>Аналоги</span>
            </div>
          </prime-button>
        </div>
        <div>
          <router-link to="shopping-cart" :style="{ textDecoration: 'none' }">
            <prime-button
              class="text-sm font-normal p-bbutton-sm p-2 p-button-text p-button-secondary text-color"
            >
              <div class="flex flex-column">
                <i
                  class="pi pi-shopping-cart badge-small badge-margin font-semibold"
                  v-if="cartCapacity > 0"
                  v-badge.info="cartCapacity"
                ></i>
                <i class="pi pi-shopping-cart font-semibold" v-else></i>
                <span>Корзина</span>
              </div>
            </prime-button>
          </router-link>
        </div>
      </div>
    </div>
    <teleport to="body">
      <catalog-filters-container></catalog-filters-container>
    </teleport>
    <div>
      <router-view v-slot="{ Component }">
        <transition-fade>
          <component :is="Component"></component>
        </transition-fade>
      </router-view>
    </div>
  </div>
</template>

<script lang="ts">
import { useCatalogFilters } from '@/composables/catalog-filters.composable';
import { useCartTotalQuantity } from '@/composables/shopping-cart.composables';
import { useElementSize } from '@vueuse/core';
import { defineComponent, ref } from 'vue';
import { useRouter } from 'vue-router';
import { searchHeightProvider } from './providers/search-height.provider';

export default defineComponent({
  setup() {
    const { searchForProducts, searchQuery, showFilters } = useCatalogFilters();

    const router = useRouter();
    router.beforeEach((to, from) => {
      if (to.name !== from.name) {
        showFilters.value = false;
      }
    });
    const cartCapacity = useCartTotalQuantity();
    const searchRef = ref();

    const { height: searchHeight } = useElementSize(searchRef);

    searchHeightProvider.provide(searchHeight);

    return { searchMe: searchForProducts, searchQuery, cartCapacity, showFilters, searchRef };
  },
});
</script>

<style lang="scss" scoped>
i.badge-small {
  :deep(span) {
    font-size: 9px;
  }
}

i.badge-margin {
  :deep(.p-badge) {
    margin-right: 0.5rem;
  }
}
</style>
