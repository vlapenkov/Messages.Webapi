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
          :value="tree || undefined"
        >
        </tree>
      </template>
      <template #footer>
        <div v-if="isAdmin" class="flex flex-row justify-content-end">
          <prime-button-add @click="createCategory" :label="null"></prime-button-add>
        </div>
      </template>
    </card>
  </loading-status-handler>
  <add-section-dialog :show="showDialog"></add-section-dialog>
</template>

<script lang="ts">
import { sectionsStore } from '@/app/sections/state/sections.store';
import { useSections } from '@/composables/sections.composable';
import { loadingStatusProvider } from '@/vue/base/presentational/state/collection/providers/loading-status.provider';
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
    const { status, startCreation } = sectionsStore;
    const viewMode = viewModeProvider.inject();
    const isAdmin = computed(() => viewMode.value === 'admin');
    loadingStatusProvider.provideFrom(() => status);

    const { tree } = useSections();

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
      tree.value?.forEach((node) => {
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

    const showDialog = ref<boolean>(false);

    const createCategory = () => {
      startCreation();
      showDialog.value = true;
    };

    return { tree, selectedKey, selectedKeys, expandedKeys, isAdmin, showDialog, createCategory };
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
