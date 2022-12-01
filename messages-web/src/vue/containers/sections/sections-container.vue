<template>
  <loading-status-handler>
    <card class="re-shape" :class="{ 'hide-footer': !isAdmin }">
      <template #title> <span class="text-xl"> Категории </span> </template>
      <template #content>
        <tree
          class="label-full"
          selection-mode="single"
          v-model:selectionKeys="selectedKeys"
          v-model:expandedKeys="expandedKeys"
          :value="tree"
        >
        </tree>
      </template>
      <template #footer>
        <div v-if="isAdmin" class="flex flex-row justify-content-end">
          <create-item-button :label="null"></create-item-button>
        </div>
      </template>
    </card>
  </loading-status-handler>
  <selected-item-dialog></selected-item-dialog>
</template>

<script lang="ts">
import { sectionsStore } from '@/app/sections/state/sections.store';
import { CollectionStoreMixed } from '@/vue/base/presentational/state/collection/collection-state.vue';
import { createItemProvider } from '@/vue/base/presentational/state/collection/providers/create-item.provider';
import { deleteItemProvider } from '@/vue/base/presentational/state/collection/providers/delete-item.provider';
import { editOrCreateModeProvider } from '@/vue/base/presentational/state/collection/providers/edit-or-create-mode.provider';
import { itemSelectedProvider } from '@/vue/base/presentational/state/collection/providers/item-selected.provider';
import { itemsCollectionProvider } from '@/vue/base/presentational/state/collection/providers/items-collection.provider';
import { loadingStatusProvider } from '@/vue/base/presentational/state/collection/providers/loading-status.provider';
import { refreshProvider } from '@/vue/base/presentational/state/collection/providers/refresh.provider';
import { reloadOnSaveProvider } from '@/vue/base/presentational/state/collection/providers/reload-on-save.provider';
import { saveChangesProvider } from '@/vue/base/presentational/state/collection/providers/save-changes.provider';
import { selectItemProvider } from '@/vue/base/presentational/state/collection/providers/select-item.provider';
import { showDialogProvider } from '@/vue/base/presentational/state/collection/providers/show-dialog.provider';
import { viewModeProvider } from '@/vue/views/providers/view-mode.provider';
import { TreeSelectionKeys } from 'primevue/tree';
import { computed, defineComponent, ref, watch } from 'vue';

export default defineComponent({
  props: {
    selected: {
      type: Number,
      default: undefined,
    },
  },
  emits: {
    'update:selected': (_: number | undefined) => true,
  },
  setup(props, { emit }) {
    const state = sectionsStore as CollectionStoreMixed;
    const viewMode = viewModeProvider.inject();
    const isAdmin = computed(() => viewMode.value === 'admin');
    if (state.items != null) {
      itemsCollectionProvider.provideFrom(() => state.items);
    }
    refreshProvider.provideFrom(() => state.getDataAsync);
    showDialogProvider.provide();
    selectItemProvider.provideFrom(() => state.selectItem);
    createItemProvider.provideFrom(() => state.createItem);
    saveChangesProvider.provideFrom(() => state.saveChanges);
    deleteItemProvider.provideFrom(() => state.deleteItem);
    itemSelectedProvider.provideFrom(() => state.itemSelected);
    editOrCreateModeProvider.provideFrom(() => state.itemSelected?.value?.mode);
    loadingStatusProvider.provideFrom(() => state.status);
    reloadOnSaveProvider.provide(ref(true));
    if (state.treeView == null) {
      throw new Error('Что-то пошло не так');
    }

    const tree = state.treeView();

    const selectedKeys = ref<TreeSelectionKeys>();
    watch(
      () => props.selected,
      (value) => {
        if (value == null) {
          selectedKeys.value = undefined;
          return;
        }
        const key: Record<string, boolean> = { [value as unknown as string]: true };
        selectedKeys.value = key;
      },
      {
        immediate: true,
      },
    );

    const expandedKeys = computed<TreeSelectionKeys>(() => {
      const result: Record<string, boolean> = {};
      tree.value.forEach((node) => {
        if (node.key == null) {
          return;
        }
        result[node.key] = true;
      });
      return result;
    });

    const selectedKey = computed(() => {
      const keys = selectedKeys.value;
      if (keys == null) {
        return undefined;
      }
      const sk = Object.keys(keys).find((k) => keys[k] === true);
      return sk == null ? undefined : +sk;
    });

    watch(selectedKey, (sk) => {
      if (sk != null) {
        emit('update:selected', sk);
      }
    });

    return { tree, selectedKey, selectedKeys, expandedKeys, isAdmin };
  },
});
</script>

<style lang="scss" scoped>
.re-shape {
  :deep(.p-card-body) {
    padding: 0.5rem 1rem;
  }

  :deep(.p-card-content) {
    padding: 0.5rem 0;
  }

  :deep(.p-card-footer) {
    padding: 0.5rem 0;
  }

  :deep(.p-tree) {
    padding: 0;
    border: none;
  }
}

.hide-footer {
  :deep(.p-card-footer) {
    display: none;
  }
}
</style>
