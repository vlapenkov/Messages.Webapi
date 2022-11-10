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
import { computed, defineComponent, PropType, ref, shallowRef, watch } from 'vue';
import TransitionFade from '@/vue/components/transitions/TransitionFade.vue';
import Dialog from 'primevue/dialog';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { Provider } from '@/app/core/tools/provider';
import { CollectionState } from '@/app/core/services/harlem/custom-stores/collection/@types/CollectionStore';
import { IViewMode } from '../@types/viewMode';
import { DisplayMode } from '../@types/viewTypes';

// Providers

export const collectionStateProvider = new Provider(
  () => shallowRef<CollectionState<IModel, ModelBase> | null>(null),
  '--collection-state-provided',
);

export const showDialogProvider = new Provider(() => ref(false), '--collection-state-show-dialog');

export const reloadOnSaveProvider = new Provider(
  () => ref(false),
  '--collection-state-reload-on-save',
);

export default defineComponent({
  components: { TransitionFade, PrimeDialog: Dialog },

  props: {
    state: {
      type: Object as PropType<CollectionState<IModel, ModelBase>>,
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
    const stateProvided = shallowRef<CollectionState<IModel, ModelBase> | null>(null);
    watch(
      () => props.state,
      (val) => {
        stateProvided.value = val;
      },
      { immediate: true },
    );
    collectionStateProvider.provideFrom(() => props.state);
    const showDialog = showDialogProvider.provide();

    reloadOnSaveProvider.provideFrom(() => props.reloadOnSave);

    const viewMode = ref<DisplayMode>(props.modes[0].mode);

    const showViewModes = computed(() => props.modes.length > 1);

    const create = () => {
      if (stateProvided.value == null) {
        return;
      }
      const { createItem, itemSelected } = stateProvided.value;
      if (createItem == null || itemSelected === undefined) {
        throw new Error('Canot edit uneditable state!');
      }

      createItem();
      showDialog.value = true;
    };

    const canAdd = computed(
      () => stateProvided.value?.createItem != null && stateProvided.value?.saveChanges != null,
    );

    const isEditable = computed(
      () => stateProvided.value?.selectItem != null && stateProvided.value.saveChanges != null,
    );

    const mode = computed(() => stateProvided.value?.itemSelected?.value?.mode);

    const selectedData = computed({
      get: () => stateProvided.value?.itemSelected?.value?.data,
      set: (val) => {
        if (stateProvided.value?.itemSelected?.value == null || val == null) {
          return;
        }
        stateProvided.value.itemSelected.value = new NotValidData(
          val,
          stateProvided.value.itemSelected.value.mode,
        );
      },
    });

    const saveChanges = () => {
      if (stateProvided.value != null && stateProvided.value.saveChanges != null) {
        stateProvided.value.saveChanges();
        showDialog.value = false;
        if (props.reloadOnSave) {
          stateProvided.value.getDataAsync({ force: true });
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
