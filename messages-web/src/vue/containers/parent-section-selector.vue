<template>
  <dropdown v-model="parentId" option-label="label" :options="itemsAsOptions"></dropdown>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import type { SectionModel } from '@/app/sections/models/section.model';
import { computed, defineComponent } from 'vue';
import { itemSelectedProvider } from '../base/presentational/state/collection/providers/item-selected.provider';
import { itemsCollectionProvider } from '../base/presentational/state/collection/providers/items-collection.provider';

export default defineComponent({
  setup() {
    const itemsWrapped = itemsCollectionProvider.inject();
    const itemSelected = itemSelectedProvider.inject();
    if (itemsWrapped.value == null) {
      throw new Error('Something went wrong');
    }
    const items = itemsWrapped.value();
    const itemsAsOptions = computed(() =>
      (items.value ?? []).map((i) => {
        const item = i as SectionModel;
        return {
          label: item.name,
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
