<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <toolbar class="mb-2 pt-2 pb-2">
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
  <slot>
    <selected-item-dialog></selected-item-dialog>
  </slot>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { defineComponent, PropType, ref } from 'vue';
import { CollectionStore } from '@/app/core/services/harlem/custom-stores/collection/@types/CollectionStore';
import { DisplayMode } from '../@types/viewTypes';
import { reloadOnSaveProvider } from './providers/reload-on-save.provider';
import { showDialogProvider } from './providers/show-dialog.provider';
import { itemsCollectionProvider } from './providers/items-collection.provider';
import { itemSelectedProvider } from './providers/item-selected.provider';
import { createItemProvider } from './providers/create-item.provider';
import { getItemsCollectionProvider } from './providers/get-items-collection.provider';
import { saveChangesProvider } from './providers/save-changes.provider';
import { selectItemProvider } from './providers/select-item.provider';
import { treeViewProvider } from './providers/tree-view.provider';
import { loadingStatusProvider } from './providers/loading-status.provider';
import { viewSwitcherProps } from './view-switcher.vue';
import { editOrCreateModeProvider } from './providers/edit-or-create-mode.provider';
import { useEditableChecks } from './composables/editable-checks.composable';

export default defineComponent({
  props: {
    state: {
      type: Object as PropType<CollectionStore<IModel, ModelBase>>,
      required: true,
    },
    reloadOnSave: {
      type: Boolean,
      default: false,
    },
    ...viewSwitcherProps,
  },
  setup(props) {
    showDialogProvider.provide();
    loadingStatusProvider.provideFrom(() => props.state.status);
    reloadOnSaveProvider.provideFrom(() => props.reloadOnSave);
    itemsCollectionProvider.provideFrom(() => props.state.items);
    selectItemProvider.provideFrom(() => props.state.selectItem);
    saveChangesProvider.provideFrom(() => props.state.saveChanges);
    itemSelectedProvider.provideFrom(() => props.state.itemSelected);
    createItemProvider.provideFrom(() => props.state.createItem);
    editOrCreateModeProvider.provideFrom(() => props.state.itemSelected?.value?.mode);
    getItemsCollectionProvider.provideFrom(() => props.state.getDataAsync);
    treeViewProvider.provideFrom(() => props.state.treeView);
    const viewMode = ref<DisplayMode>(props.modes[0].mode);
    const { canAdd } = useEditableChecks();
    return {
      viewMode,
      canAdd,
    };
  },
});
</script>

<style scoped></style>
