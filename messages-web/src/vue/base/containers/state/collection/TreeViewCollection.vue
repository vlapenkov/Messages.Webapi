<template>
  <tree :value="tree"></tree>
</template>

<script lang="ts">
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { computed, defineComponent, toRaw, watchEffect } from 'vue';
import { collectionStateProvider } from './CollectionState.vue';

export default defineComponent({
  setup() {
    const currentState = collectionStateProvider.inject();

    const loadingStatus = computed<DataStatus | undefined>(() => currentState.value?.status.value);

    const isEditable = computed(
      () => currentState.value?.selectItem != null && currentState.value.saveChanges != null,
    );
    const canAdd = computed(
      () => currentState.value?.createItem != null && currentState.value.saveChanges != null,
    );

    if (currentState.value?.treeView == null) {
      throw new Error("Your state doesn't have a treeView provider");
    }

    const tree = currentState.value.treeView();

    watchEffect(() => {
      console.log('tree is', toRaw(tree.value));
    });

    return { loadingStatus, isEditable, canAdd, tree };
  },
});
</script>

<style scoped></style>
