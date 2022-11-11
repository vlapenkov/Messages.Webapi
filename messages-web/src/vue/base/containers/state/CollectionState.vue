<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <toolbar class="mb-2 pt-2 pb-2">
    <template #start>
      <div v-if="showViewModes" class="flex flex-row gap-5">
        <div>Вид</div>
        <div v-for="mode in modes" :key="mode.mode" class="field-radiobutton m-0">
          <radio-button :inputId="mode.mode" name="mode" :value="mode.mode" v-model="viewMode" />
          <label :for="mode.mode">{{ mode.label }}</label>
        </div>
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
import { IViewMode } from './@types/viewMode';
import { DisplayMode } from './@types/viewTypes';
import { reloadOnSaveProvider } from './_providers/reload-on-save.provider';
import { showDialogProvider } from './_providers/show-dialog.provider';
import { itemsCollectionProvider } from './_providers/items-collection.provider';
import { itemSelectedProvider } from './_providers/item-selected.provider';
import { createItemProvider } from './_providers/create-item.provider';
import { getItemsCollectionProvider } from './_providers/get-items-collection.provider';
import { saveChangesProvider } from './_providers/save-changes.provider';
import { selectItemProvider } from './_providers/select-item.provider';
import { treeViewProvider } from './_providers/tree-view.provider';
import { loadingStatusProvider } from './_providers/loading-status.provider';

export default defineComponent({
  components: { TransitionFade, PrimeDialog: Dialog },

  props: {
    state: {
      type: Object as PropType<CollectionStore<IModel, ModelBase>>,
      required: true,
    },

    reloadOnSave: {
      type: Boolean,
      default: false,
    },

    modes: {
      type: Array as PropType<IViewMode[]>,
      default: (): IViewMode[] => [
        {
          mode: 'data-view',
          label: 'Сеткой',
        },
        {
          mode: 'tree-view',
          label: 'Деревом',
        },
      ],
    },
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

    const showViewModes = computed(() => props.modes.length > 1);
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
      showViewModes,
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
