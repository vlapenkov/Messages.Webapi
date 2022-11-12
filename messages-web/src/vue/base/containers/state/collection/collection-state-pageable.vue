<template>
  <toolbar v-if="canAdd || modes.length > 1" class="mb-2 pt-2 pb-2">
    <template #start>
      <div class="flex flex-row gap-5">
        <view-switcher :modes="modes" v-model="viewMode"></view-switcher>
      </div>
    </template>
    <template #end>
      <div v-if="canAdd" class="flex justify-content-end">
        <prime-button-add @click="create" label="Добавить"></prime-button-add>
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
  <paginator
    class="mt-1"
    :rows="pageSize"
    :first="pageSize * (pageNumber - 1)"
    :totalRecords="totalItemsCount"
    @page="changePage"
  ></paginator>

  <prime-dialog
    :header="`${mode === 'create' ? 'Создание нового' : 'Редактирование'} элемента`"
    v-model:visible="showDialog"
  >
    <custom-form class="shadow-none" v-model:data="selectedData">
      <template #footer>
        <div v-if="(isEditable || canAdd) && mode != null" class="flex justify-content-end">
          <prime-button-add
            @click="saveChanges"
            v-if="mode === 'create'"
            label="Добавить"
          ></prime-button-add>
          <prime-button-save @click="saveChanges" v-else label="Сохранить"></prime-button-save>
        </div>
      </template>
    </custom-form>
  </prime-dialog>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { PageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/@types/PageableCollectionStore';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import Dialog from 'primevue/dialog';
import { computed, defineComponent, PropType, ref } from 'vue';
import { DisplayMode } from '../@types/viewTypes';
import { createItemProvider } from './providers/create-item.provider';
import { itemSelectedProvider } from './providers/item-selected.provider';
import { itemsCollectionProvider } from './providers/items-collection.provider';
import { loadingStatusProvider } from './providers/loading-status.provider';
import { selectItemProvider } from './providers/select-item.provider';
import { showDialogProvider } from './providers/show-dialog.provider';
import { viewSwitcherProps } from './view-switcher.vue';

export default defineComponent({
  components: { PrimeDialog: Dialog },
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

    const showDialog = showDialogProvider.provide();
    const getData = itemsCollectionProvider.provideFrom(() => () => props.state.currentPageItems);
    const itemSelected = itemSelectedProvider.provideFrom(() => props.state.itemSelected);
    const createItem = createItemProvider.provideFrom(() => props.state.createItem);
    selectItemProvider.provideFrom(() => props.state.selectItem);

    const pageSize = computed(() => props.state.pageSize.value ?? 0);

    const pageNumber = computed(() => props.state.pageNumber.value ?? 0);

    const totalItemsCount = computed(() => props.state.currentPage.value?.totalItemCount ?? 0);

    const canAdd = computed(
      () => props.state.createItem != null && props.state.saveChanges != null,
    );

    const isEditable = computed(
      () => props.state.selectItem != null && props.state.saveChanges != null,
    );

    const mode = computed(() => props.state.itemSelected?.value?.mode);

    const create = () => {
      if (createItem.value == null || itemSelected.value === undefined) {
        throw new Error('Canot edit uneditable state!');
      }

      createItem.value();
      showDialog.value = true;
    };

    const selectedData = computed({
      get: () => itemSelected?.value?.value?.data,
      set: (val) => {
        if (itemSelected.value == null || val == null || mode.value == null) {
          return;
        }
        itemSelected.value.value = new NotValidData(val, mode.value);
      },
    });

    const saveChanges = () => {
      if (props.state != null && props.state.saveChanges != null && getData.value != null) {
        props.state.saveChanges();
        showDialog.value = false;
      }
    };

    const viewMode = ref<DisplayMode>(props.modes[0].mode);

    const changePage = ({ page }: { page: number }) => {
      if (props.state.pageNumber.value == null) {
        return;
      }
      // eslint-disable-next-line vue/no-mutating-props
      props.state.pageNumber.value = page + 1;
    };
    return {
      canAdd,
      isEditable,
      mode,
      saveChanges,
      viewMode,
      showDialog,
      create,
      selectedData,
      pageSize,
      pageNumber,
      totalItemsCount,
      changePage,
    };
  },
});
</script>

<style scoped></style>
