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
  <prime-dialog header="Создание нового элемента" v-model:visible="showDialog">
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
import { computed, defineComponent, PropType, ref } from 'vue';
import TransitionFade from '@/vue/components/transitions/TransitionFade.vue';
import Dialog from 'primevue/dialog';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { CollectionStore } from '@/app/core/services/harlem/custom-stores/collection/@types/CollectionStore';
import { DisplayMode } from './@types/viewTypes';
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
import ViewSwitcher, { viewSwitcherProps } from './ViewSwitcher.vue';

export default defineComponent({
  components: { TransitionFade, PrimeDialog: Dialog, ViewSwitcher },

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
    const showDialog = showDialogProvider.provide();
    loadingStatusProvider.provideFrom(() => props.state.status);
    reloadOnSaveProvider.provideFrom(() => props.reloadOnSave);
    itemsCollectionProvider.provideFrom(() => props.state.items);
    selectItemProvider.provideFrom(() => props.state.selectItem);
    saveChangesProvider.provideFrom(() => props.state.saveChanges);
    const itemSelected = itemSelectedProvider.provideFrom(() => props.state.itemSelected);
    const createItem = createItemProvider.provideFrom(() => props.state.createItem);
    const getData = getItemsCollectionProvider.provideFrom(() => props.state.getDataAsync);
    treeViewProvider.provideFrom(() => props.state.treeView);

    const viewMode = ref<DisplayMode>(props.modes[0].mode);

    const create = () => {
      if (createItem.value == null || itemSelected.value === undefined) {
        throw new Error('Canot edit uneditable state!');
      }

      createItem.value();
      showDialog.value = true;
    };

    const canAdd = computed(
      () => props.state.createItem != null && props.state.saveChanges != null,
    );

    const isEditable = computed(
      () => props.state.selectItem != null && props.state.saveChanges != null,
    );

    const mode = computed(() => props.state.itemSelected?.value?.mode);

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
        if (props.reloadOnSave) {
          getData.value({ force: true });
        }
      }
    };

    return {
      viewMode,
      create,
      canAdd,
      isEditable,
      mode,
      showDialog,
      selectedData,
      saveChanges,
    };
  },
});
</script>

<style scoped></style>
