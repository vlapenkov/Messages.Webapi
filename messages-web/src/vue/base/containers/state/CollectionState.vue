<template>
  <loading-status-handler :status="loadingStatus">
    <data-view :value="items">
      <template #list="{ data }">
        <div class="col-12">
          <data-card class="shadow-none" :data="data"> </data-card>
        </div>
      </template>
      <template #footer>
        <div v-if="isEditable" class="flex justify-content-end">
          <prime-button-add @click="create" label="Добавить"></prime-button-add>
        </div>
      </template>
    </data-view>
  </loading-status-handler>
  <prime-dialog v-model:visible="showDialog">
    <custom-form class="shadow-none" :data="selectedData"></custom-form>
  </prime-dialog>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { ICollectionEditableStore } from '@/app/core/services/harlem/custom/collection/editable/collection-editable.store';
import { IReadonlyCollectionStore } from '@/app/core/services/harlem/custom/collection/readonly/collection-readonly.store';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { computed, defineComponent, PropType, ref } from 'vue';
import Dialog from 'primevue/dialog';

export type SomeState<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> = IReadonlyCollectionStore<IModel, TModel> & Partial<ICollectionEditableStore<IModel, TModel>>;

export default defineComponent({
  components: { PrimeDialog: Dialog },
  props: {
    state: {
      type: Object as PropType<SomeState<IModel, ModelBase>>,
      required: true,
    },
  },
  setup(props) {
    const loadingStatus = computed<DataStatus | undefined>(() => props.state.loadingStatus.value);
    const items = props.state.itemsAsync();
    const isEditable = computed(
      () => props.state.selectItem != null && props.state.saveChanges != null,
    );

    const showDialog = ref(false);

    const create = () => {
      const { createItem, itemSelected } = props.state;
      if (createItem == null || itemSelected === undefined) {
        throw new Error('Canot edit uneditable state!');
      }

      createItem();
      showDialog.value = true;
    };

    const selectedData = computed(() => props.state.itemSelected?.value?.data);

    return { loadingStatus, items, isEditable, create, showDialog, selectedData };
  },
});
</script>

<style scoped></style>
