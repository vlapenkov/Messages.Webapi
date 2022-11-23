<template>
  <div>
    <tree
      class="label-full"
      selection-mode="single"
      v-model:selectionKeys="selectedKeys"
      :value="tree"
    >
    </tree>
  </div>
</template>

<script lang="ts">
import { sectionsStore } from '@/app/sections/state/sections.store';
import { CollectionStoreMixed } from '@/vue/base/presentational/state/collection/collection-state.vue';
import { TreeSelectionKeys } from 'primevue/tree';
import { computed, defineComponent, ref } from 'vue';

export default defineComponent({
  setup() {
    const state = sectionsStore as CollectionStoreMixed;
    if (state.treeView == null) {
      throw new Error('Что-то пошло не так');
    }
    const tree = state.treeView();
    const selectedKeys = ref<TreeSelectionKeys>();

    const selectedKey = computed(() => {
      const keys = selectedKeys.value;
      if (keys == null) {
        return undefined;
      }
      const sk = Object.keys(keys).find((k) => keys[k] === true);
      return sk == null ? undefined : +sk;
    });

    return { tree, selectedKey, selectedKeys };
  },
});
</script>

<style scoped></style>
