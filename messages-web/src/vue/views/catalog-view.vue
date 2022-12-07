<template>
  <app-page title="Каталог товаров">
    <template #subheader>
      <div
        :style="{ maxWidth: productsContainerSize + 'px' }"
        class="flex-grow-1 flex flex-row justify-content-between gap-2"
      >
        <span class="p-input-icon-left w-full">
          <i class="pi pi-search" />
          <input-text class="w-full" v-model="search" type="text" placeholder="Поиск" />
        </span>
        <prime-button
          class="flex-shrink-0 p-button-secondary"
          @click="switchViewMode"
          v-tooltip.bottom="
            viewMode === 'user'
              ? 'Перейти в режим администрирования'
              : 'Перейти в режим пользователя'
          "
          :icon="viewMode === 'user' ? 'pi pi-unlock' : 'pi pi-lock-open'"
        ></prime-button>
      </div>
    </template>
    <div class="grid mt-1">
      <div class="col-6">
        <dropdown
          v-model="regionModel"
          :options="regionOptions"
          placeholder="Регион"
          :style="{ width: '100%' }"
        />
      </div>
      <div class="col-6">
        <dropdown
          v-model="organizationModel"
          :options="organizationOptions"
          placeholder="Производитель"
          :style="{ width: '100%' }"
        />
      </div>

      <div class="col-3">
        <sections-container v-model:selected="selectedKey"></sections-container>
      </div>
      <div ref="productsContainerRef" class="col-9">
        <products-viewer />
      </div>
    </div>
  </app-page>
</template>

<script lang="ts">
import { IproductsPageRequest } from '@/app/product-shorts/@types/IproductsPageRequest';
import { productShortsService } from '@/app/product-shorts/services/product-shorts.service';
import { productShortsStore } from '@/app/product-shorts/state/product-shorts.store';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { useElementSize } from '@vueuse/core';
import { computed, defineComponent, ref, watch } from 'vue';
import { useRouteQuery } from '@vueuse/router';
import { useOrganizations } from './composables/organizations.composable';
import { viewModeProvider } from './providers/view-mode.provider';

export default defineComponent({
  setup() {
    // const paramsToSectionId = (
    //   val: RouteLocationNormalized | RouteLocationNormalizedLoaded,
    // ): number | undefined => {
    //   const id: number | undefined = parseInt(val.params.id as string, 10);
    //   return id != null && !Number.isNaN(id) ? id : undefined;
    // };

    // onBeforeRouteUpdate((to) => {
    //   productShortsStore.parentSectionId.value = paramsToSectionId(to);
    // });
    // onBeforeMount(() => {
    //   productShortsStore.parentSectionId.value = paramsToSectionId(route);
    // });
    const viewMode = viewModeProvider.provide();

    const switchViewMode = () => {
      viewMode.value = viewMode.value === 'user' ? 'admin' : 'user';
    };

    const sectionId = useRouteQuery<string | null>('sectionId');
    const region = useRouteQuery<string | null>('region');
    const organization = useRouteQuery<string | null>('organization');

    watch(sectionId, (val) => {
      if (val == null) {
        return;
      }
      const id = parseInt(val as string, 10);
      if (productShortsStore.parentSectionId.value === id) {
        return;
      }
      productShortsStore.parentSectionId.value = id;
    });
    watch(region, (val) => {
      if (val == null) {
        return;
      }
      if (productShortsStore.region.value === val) {
        return;
      }
      productShortsStore.region.value = val;
    });
    watch(organization, (val) => {
      if (val == null) {
        return;
      }
      if (productShortsStore.organization.value === val) {
        return;
      }
      productShortsStore.organization.value = val;
    });

    watch(
      [
        productShortsStore.pageNumber,
        productShortsStore.pageSize,
        productShortsStore.searchQuery,
        productShortsStore.parentSectionId,
      ],
      ([pageNumber, pageSize, query, catalogSectionId]) => {
        // console.log('Запрашиваем страницы', pageNumber, pageSize, query, catalogSectionId);
        const request: IproductsPageRequest = {
          name: query == null || query === '' || query.trim() === '' ? null : query,
          catalogSectionId,
          pageNumber,
          pageSize,
        };
        productShortsService.loadPage(request);
      },
      {
        immediate: true,
      },
    );

    const selectedKey = computed({
      get: () => productShortsStore.parentSectionId.value,
      set: (val) => {
        sectionId.value = val == null ? '' : `${val}`;
      },
    });
    const productsContainerRef = ref<HTMLElement>();
    const { width: productsContainerSize } = useElementSize(productsContainerRef);

    const regionModel = ref<string>();
    const organizationModel = ref<string>();

    const { organizations: organizationOptions, regions: regionOptions } = useOrganizations();

    return {
      sectionsStore,
      selectedKey,
      search: productShortsStore.searchQuery,
      productsContainerRef,
      productsContainerSize,
      viewMode,
      switchViewMode,
      regionModel,
      organizationModel,
      regionOptions,
      organizationOptions,
    };
  },
});
</script>

<style lang="scss" scoped>
.reshape-tree {
  :deep(.p-tree) {
    margin-top: 0 !important;
    border: none;
  }
}
</style>
