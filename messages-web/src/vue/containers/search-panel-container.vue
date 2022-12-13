<template>
  <div ref="searchPanelRef" class="flex flex-row justify-space-between align-items-center mt-2">
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
        <prime-button class="text-sm font-normal p-bbutton-sm p-2 p-button-text p-button-secondary">
          <div class="flex flex-column">
            <i class="pi pi-shopping-cart" v-if="cartCapacity > 0" v-badge="cartCapacity"></i>
            <i class="pi pi-shopping-cart" v-else></i>
            <span>Корзина</span>
          </div>
        </prime-button>
      </router-link>
    </div>
  </div>
</template>

<script lang="ts">
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { defineComponent, ref } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  setup() {
    const router = useRouter();

    const sectionModel = ref();
    const regionModel = ref();
    const organizationModel = ref();
    const searchQuery = ref<string>();

    const searchMe = () => {
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

    const { totalQuantity: cartCapacity } = shoppingCartStore;

    const searchPanelRef = ref();

    return { searchMe, searchQuery, cartCapacity, searchPanelRef };
  },
});
</script>

<style lang="scss" scoped></style>
