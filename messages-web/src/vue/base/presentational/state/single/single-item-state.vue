<template>
  <slot :item="item">
    <loading-status-handler>
      <transition-fade>
        <data-card v-if="mode == null" :data="item">
          <template #footer>
            <slot name="footer-view"></slot>
          </template>
        </data-card>
        <custom-form v-else v-model:data="selectedData">
          <template #footer>
            <slot name="footer-edit"> </slot>
          </template>
        </custom-form>
      </transition-fade>
    </loading-status-handler>
  </slot>
</template>

<script lang="ts">
import { ModelBase } from '@/app/core/models/base/model-base';
import { ISingleItemStore } from '@/app/core/services/harlem/custom-stores/single-Item/@types/ISingleItemStore';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { computed, defineComponent, PropType } from 'vue';
import { createItemProvider } from '../collection/providers/create-item.provider';
import { deleteItemProvider } from '../collection/providers/delete-item.provider';
import { editOrCreateModeProvider } from '../collection/providers/edit-or-create-mode.provider';
import { itemSelectedProvider } from '../collection/providers/item-selected.provider';
import { loadingStatusProvider } from '../collection/providers/loading-status.provider';
import { saveChangesProvider } from '../collection/providers/save-changes.provider';
import { showDialogProvider } from '../collection/providers/show-dialog.provider';
import { selectItemSingleProvider } from './propviders/select-item-single.provider';
import { singleItemProvider } from './propviders/single-item.provider';

export default defineComponent({
  props: {
    state: {
      type: Object as PropType<ISingleItemStore<ModelBase>>,
      required: true,
    },
  },
  setup(props) {
    loadingStatusProvider.provideFrom(() => props.state.status);
    showDialogProvider.provide();
    singleItemProvider.provideFrom(() => props.state.itemSmart);
    selectItemSingleProvider.provideFrom(() => props.state.selectItem ?? null);
    deleteItemProvider.provideFrom(() => props.state.deleteItem);
    createItemProvider.provideFrom(() => props.state.createItem);
    saveChangesProvider.provideFrom(() => props.state.saveChanges);
    const itemSelected = itemSelectedProvider.provideFrom(() => props.state.itemSelected);
    const mode = editOrCreateModeProvider.provideFrom(() => props.state.itemSelected?.value?.mode);
    const selectedData = computed({
      get: () => itemSelected?.value?.value?.data as ModelBase | undefined,
      set: (val) => {
        // console.log('setting', { val, si: itemSelected.value, mode: mode.value });

        if (itemSelected.value == null || val == null || mode.value == null) {
          return;
        }
        itemSelected.value.value = new NotValidData(val, mode.value);
      },
    });

    const item = props.state.itemSmart();
    return { item, mode, selectedData };
  },
});
</script>

<style lang="scss"></style>
