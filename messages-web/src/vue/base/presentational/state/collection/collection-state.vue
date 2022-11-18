<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div class="flex flex-column">
    <toolbar v-if="canAdd || modes.length > 1" class="mb-2 pt-2 pb-2 pr-2">
      <template #start>
        <slot name="toolbar-start">
          <div class="flex flex-row gap-5">
            <view-switcher :modes="modes" v-model="viewMode"></view-switcher>
          </div>
        </slot>
      </template>
      <template #end>
        <slot name="toolbar-end">
          <div v-if="canAdd" class="flex justify-content-end">
            <create-item-button />
          </div>
        </slot>
      </template>
    </toolbar>
    <div class="flex-grow-1">
      <transition-fade>
        <div v-if="viewMode === 'data-view'">
          <slot name="data-view"></slot>
        </div>
        <div v-else-if="viewMode === 'tree-view'">
          <slot name="tree-view"></slot>
        </div>
      </transition-fade>
    </div>
    <collection-state-paginator></collection-state-paginator>
    <slot>
      <selected-item-dialog></selected-item-dialog>
    </slot>
  </div>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { computed, defineComponent, PropType, ref, WritableComputedRef } from 'vue';
import { CollectionStore } from '@/app/core/services/harlem/custom-stores/collection/@types/CollectionStore';
import { PageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/@types/PageableCollectionStore';
import { DisplayMode } from '../@types/viewTypes';
import { reloadOnSaveProvider } from './providers/reload-on-save.provider';
import { showDialogProvider } from './providers/show-dialog.provider';
import { itemsCollectionProvider } from './providers/items-collection.provider';
import { itemSelectedProvider } from './providers/item-selected.provider';
import { createItemProvider } from './providers/create-item.provider';
import { saveChangesProvider } from './providers/save-changes.provider';
import { selectItemProvider } from './providers/select-item.provider';
import { treeViewProvider } from './providers/tree-view.provider';
import { loadingStatusProvider } from './providers/loading-status.provider';
import { viewSwitcherProps } from './components/view-switcher.vue';
import { editOrCreateModeProvider } from './providers/edit-or-create-mode.provider';
import { refreshProvider } from './providers/refresh.provider';
import {
  pageSizeProvider,
  pageNumberProvider,
  totalItemsCountProvider,
} from './providers/pages.provider';
import { deleteItemProvider } from './providers/delete-item.provider';

type CollectionStoreMixed = Partial<CollectionStore<IModel, ModelBase>> &
  Partial<PageableCollectionStore<IModel, ModelBase>>;

export default defineComponent({
  props: {
    state: {
      type: Object as PropType<CollectionStoreMixed>,
      required: true,
    },
    reloadOnSave: {
      type: Boolean,
      default: false,
    },
    ...viewSwitcherProps,
  },
  setup(props) {
    // Список элементов
    if (props.state.items != null) {
      itemsCollectionProvider.provideFrom(() => props.state.items);
    } else if (props.state.currentPageItems != null) {
      itemsCollectionProvider.provideFrom(
        () => () => props.state.currentPageItems as WritableComputedRef<ModelBase<IModel>[] | null>,
      );
    }
    refreshProvider.provideFrom(() => props.state.getDataAsync);
    // Диалог (не обязательно) с добавлением или редактированием элемента коллекции
    // Статус видимости, экшн выбора, экшн добавления, выбранный элемент, экшн на сохранение, редактируем или добавляем
    showDialogProvider.provide();
    selectItemProvider.provideFrom(() => props.state.selectItem);
    createItemProvider.provideFrom(() => props.state.createItem);
    saveChangesProvider.provideFrom(() => props.state.saveChanges);
    deleteItemProvider.provideFrom(() => props.state.deleteItem);
    itemSelectedProvider.provideFrom(() => props.state.itemSelected);
    editOrCreateModeProvider.provideFrom(() => props.state.itemSelected?.value?.mode);
    // Идёт ли загрузка
    loadingStatusProvider.provideFrom(() => props.state.status);
    // Древовидная сттруктура (Нужно использовать специальный декоратор в стейте)
    treeViewProvider.provideFrom(() => props.state.treeView);
    // Настройки компонента
    reloadOnSaveProvider.provideFrom(() => props.reloadOnSave);
    // Пагинация
    pageSizeProvider.provideFrom(() => props.state.pageSize);
    pageNumberProvider.provideFrom(() => props.state.pageNumber);
    totalItemsCountProvider.provideFrom(() => props.state.currentPage?.value?.totalItemCount ?? 0);
    // Конец
    const viewMode = ref<DisplayMode>(props.modes[0].mode);
    const canAdd = computed(
      () => props.state.createItem != null && props.state.saveChanges != null,
    );
    return {
      viewMode,
      canAdd,
    };
  },
});
</script>

<style scoped></style>
