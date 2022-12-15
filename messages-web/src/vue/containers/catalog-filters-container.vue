<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <transition-fade>
    <app-container v-if="showFilters" :style="{ marginTop }" class="absolute left-0 top-0 w-full">
      <div class="z-1">
        <card class="shadow-7">
          <template #title>
            <div class="flex flex-row justify-content-end">
              <prime-button
                class="p-button-sm p-button-text text-color"
                icon="pi pi-times"
                label="Сбросить"
                @click="undo"
              ></prime-button>
            </div>
          </template>
          <template #content>
            <div class="grid">
              <div class="col-4">
                <tree-select
                  class="w-full"
                  :options="sectionsTree"
                  v-model="sectionModelTree"
                  placeholder="Область применения"
                ></tree-select>
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
              <div v-if="false" class="col-12 mt-1 flex flex-row gap-4">
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
    </app-container>
  </transition-fade>
</template>

<script lang="ts">
import { useCatalogFilters } from '@/composables/catalog-filters.composable';
import { catalogFiltersStore } from '@/store/catalog-filters.store';
import { computed, defineComponent, watchEffect } from 'vue';
import { headerHeightProvider } from '../presentational/providers/headerHeightProvider';
import { searchHeightProvider } from '../views/providers/search-height.provider';

export default defineComponent({
  setup() {
    const headerHeight = headerHeightProvider.inject();
    const searchHeight = searchHeightProvider.inject();
    const marginTop = computed(() => `${headerHeight.value + searchHeight.value + 10}px`);
    watchEffect(() => {
      console.log('marginTop', marginTop.value);
    });
    const { showFilters, ...rest } = useCatalogFilters();
    const undo = () => {
      showFilters.value = false;
      catalogFiltersStore.undoMainFilters();
    };

    return { ...rest, showFilters, marginTop, undo };
  },
});
</script>

<style scoped></style>
