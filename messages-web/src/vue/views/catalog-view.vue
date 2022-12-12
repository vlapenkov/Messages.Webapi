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
          show-clear
          placeholder="Регион"
          :style="{ width: '100%' }"
        />
      </div>
      <div class="col-6">
        <dropdown
          show-clear
          v-model="organizationModel"
          :options="organizationOptions"
          placeholder="Производитель"
          :style="{ width: '100%' }"
        />
      </div>

      <div class="col-3">
        <sections-container v-model:selected="parentSectionId"></sections-container>
      </div>
      <div ref="productsContainerRef" class="col-9">
        <products-viewer />
      </div>
    </div>
  </app-page>
</template>

<script lang="ts">
import { IproductionsPageRequest } from '@/app/productions/@types/IproductionsPageRequest';
import { productionsService } from '@/app/productions/services/productions.service';
import { productionsStore } from '@/app/productions/state/productions.store';
import { useElementSize } from '@vueuse/core';
import { defineComponent, ref, watch } from 'vue';
import { useOrganizations } from '@/composables/organizations.composable';
import { useRouteQueryBinded } from '@/composables/bind-route-query.composable';
import { isNullOrEmpty } from '@/tools/string-tools';
import { viewModeProvider } from './providers/view-mode.provider';

export default defineComponent({
  setup() {
    const viewMode = viewModeProvider.provide();
    const { sectionId } = productionsStore;

    const switchViewMode = () => {
      viewMode.value = viewMode.value === 'user' ? 'admin' : 'user';
    };

    useRouteQueryBinded('sectionId', {
      type: 'number',
      ref: sectionId,
    });

    useRouteQueryBinded('region', {
      type: 'string',
      ref: productionsStore.region,
    });

    useRouteQueryBinded('organization', {
      type: 'string',
      ref: productionsStore.organization,
    });

    useRouteQueryBinded('searchQuery', {
      type: 'string',
      ref: productionsStore.searchQuery,
    });

    watch(
      [
        productionsStore.pageNumber,
        productionsStore.pageSize,
        productionsStore.searchQuery,
        sectionId,
        productionsStore.region,
        productionsStore.organization,
      ],
      ([pageNumber, pageSize, query, catalogSectionId, reg, org]) => {
        // console.log('Запрашиваем страницы', pageNumber, pageSize, query, catalogSectionId);
        const request: IproductionsPageRequest = {
          name: isNullOrEmpty(query) ? null : query,
          catalogSectionId,
          pageNumber,
          pageSize,
          producerName: org ?? null,
          region: reg ?? null,
        };

        productionsService.loadPage(request);
      },
      {
        immediate: true,
      },
    );

    const productsContainerRef = ref<HTMLElement>();
    const { width: productsContainerSize } = useElementSize(productsContainerRef);

    const { region: regionModel, organization: organizationModel } = productionsStore;

    const { organizations: organizationOptions, regions: regionOptions } = useOrganizations();

    return {
      parentSectionId: sectionId,
      search: productionsStore.searchQuery,
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
