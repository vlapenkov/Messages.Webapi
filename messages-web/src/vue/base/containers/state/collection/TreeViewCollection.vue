<template>
  <tree :value="tree"></tree>
</template>

<script lang="ts">
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { computed, defineComponent, toRaw, watchEffect } from 'vue';
import { injectCollectionState } from './CollectionState.vue';

export default defineComponent({
  setup() {
    const currentState = injectCollectionState();

    const loadingStatus = computed<DataStatus | undefined>(() => currentState.value?.status.value);

    const isEditable = computed(
      () => currentState.value?.selectItem != null && currentState.value.saveChanges != null,
    );
    const canAdd = computed(
      () => currentState.value?.createItem != null && currentState.value.saveChanges != null,
    );

    const tree = computed(() =>
      currentState.value?.treeView ? currentState.value.treeView().value ?? [] : [],
    );

    watchEffect(() => {
      console.log('tree is', toRaw(tree.value));
    });

    return { loadingStatus, isEditable, canAdd, tree };
  },
});
</script>

<style scoped></style>
