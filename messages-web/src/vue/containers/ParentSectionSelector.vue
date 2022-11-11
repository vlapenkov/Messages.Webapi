<template>
  <dropdown v-model="parentId" option-label="label" :options="itemsAsOptions"></dropdown>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import type { SectionModel } from '@/app/sections/models/section.model';
import { computed, defineComponent } from 'vue';
import { itemSelectedProvider } from '../base/containers/state/_providers/item-selected.provider';
import { itemsCollectionProvider } from '../base/containers/state/_providers/items-collection.provider';

export default defineComponent({
  setup() {
    const items = itemsCollectionProvider.inject();
    const itemSelected = itemSelectedProvider.inject();

    const itemsAsOptions = computed(() =>
      (items.value?.()?.value ?? []).map((i) => {
        const item = i as SectionModel;
        return {
          label: `${item.name} (id: ${item.id})`,
          value: item.id,
        };
      }),
    );
    const selectedItem = computed({
      get: () => itemSelected.value?.value?.data as SectionModel,
      set: (val) => {
        const mode = itemSelected.value?.value?.mode;
        console.log({ val, mode });

        if (mode == null || val == null || itemSelected.value == null) {
          return;
        }
        itemSelected.value.value = new NotValidData(val, mode);
      },
    });
    const parentId = computed({
      get: () => itemsAsOptions.value.find((i) => i.value === selectedItem.value?.parentSectionId),
      set: (option) => {
        if (selectedItem.value == null) {
          return;
        }
        const cloned = selectedItem.value.clone() as SectionModel;
        cloned.parentSectionId = option?.value ?? null;
        selectedItem.value = cloned;
        console.log({ option });
      },
    });
    return { itemsAsOptions, parentId };
  },
});
</script>

<style scoped></style>
