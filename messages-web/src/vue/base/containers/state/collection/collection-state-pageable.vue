<template>
  <toolbar v-if="canAdd || modes.length > 1" class="mb-2 pt-2 pb-2">
    <template #start>
      <div class="flex flex-row gap-5">
        <view-switcher :modes="modes" v-model="viewMode"></view-switcher>
      </div>
    </template>
    <template #end>
      <div v-if="canAdd" class="flex justify-content-end">
        <create-item-button />
      </div>
    </template>
  </toolbar>
  <transition-fade>
    <div v-if="viewMode === 'data-view'">
      <slot name="data-view"></slot>
    </div>
    <div v-else-if="viewMode === 'tree-view'">
      <slot name="tree-view"></slot>
    </div>
  </transition-fade>
  <collection-state-paginator></collection-state-paginator>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { PageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/@types/PageableCollectionStore';
import { defineComponent, PropType, ref } from 'vue';
import { DisplayMode } from '../@types/viewTypes';
import { useEditableChecks } from './composables/editable-checks.composable';
import { createItemProvider } from './providers/create-item.provider';
import { getItemsCollectionProvider } from './providers/get-items-collection.provider';
import { itemSelectedProvider } from './providers/item-selected.provider';
import { itemsCollectionProvider } from './providers/items-collection.provider';
import { loadingStatusProvider } from './providers/loading-status.provider';
import {
  pageNumberProvider,
  pageSizeProvider,
  totalItemsCountProvider,
} from './providers/pages.provider';
import { reloadOnSaveProvider } from './providers/reload-on-save.provider';
import { selectItemProvider } from './providers/select-item.provider';
import { showDialogProvider } from './providers/show-dialog.provider';
import { viewSwitcherProps } from './view-switcher.vue';

export default defineComponent({
  props: {
    state: {
      type: Object as PropType<PageableCollectionStore<IModel, ModelBase>>,
      required: true,
    },
    reloadOnSave: {
      type: Boolean,
      default: false,
    },
    ...viewSwitcherProps,
  },
  setup(props) {
    loadingStatusProvider.provideFrom(() => props.state.status);
    showDialogProvider.provide();
    getItemsCollectionProvider.provideFrom(() => props.state.getPage);
    reloadOnSaveProvider.provideFrom(() => props.reloadOnSave);
    itemSelectedProvider.provideFrom(() => props.state.itemSelected);
    createItemProvider.provideFrom(() => props.state.createItem);
    itemsCollectionProvider.provideFrom(() => () => props.state.currentPageItems);
    selectItemProvider.provideFrom(() => props.state.selectItem);
    pageSizeProvider.provideFrom(() => props.state.pageSize.value ?? 0);
    pageNumberProvider.provideFrom(() => props.state.pageNumber.value ?? 0);
    totalItemsCountProvider.provideFrom(() => props.state.currentPage.value?.totalItemCount ?? 0);

    const { canAdd } = useEditableChecks();

    const viewMode = ref<DisplayMode>(props.modes[0].mode);

    return {
      canAdd,
      viewMode,
    };
  },
});
</script>

<style scoped></style>
