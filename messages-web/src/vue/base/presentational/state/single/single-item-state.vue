<template>
  <slot :item="item">
    <loading-status-handler>
      <transition-fade>
        <data-card v-if="mode == null" :data="item"></data-card>
        <custom-form v-else :data="item"></custom-form>
      </transition-fade>
    </loading-status-handler>
  </slot>
</template>

<script lang="ts">
import { ModelBase } from '@/app/core/models/base/model-base';
import { ISingleItemStore } from '@/app/core/services/harlem/custom-stores/single-Item/@types/ISingleItemStore';
import { defineComponent, PropType } from 'vue';
import { createItemProvider } from '../collection/providers/create-item.provider';
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
    createItemProvider.provideFrom(() => props.state.createItem);
    saveChangesProvider.provideFrom(() => props.state.saveChanges);
    itemSelectedProvider.provideFrom(() => props.state.itemSelected);
    const mode = editOrCreateModeProvider.provideFrom(() => props.state.itemSelected?.value?.mode);
    const item = props.state.itemSmart();
    return { item, mode };
  },
});
</script>

<style scoped></style>
