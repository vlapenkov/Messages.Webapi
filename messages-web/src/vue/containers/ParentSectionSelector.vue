<template>
  <dropdown v-model="parentId" option-label="label" :options="itemsAsOptions"></dropdown>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import type { SectionModel } from '@/app/sections/models/section.model';
// eslint-disable-next-line import/no-cycle
import { sectionsStore } from '@/app/sections/state/sections.store';
import { computed, defineComponent } from 'vue';

export default defineComponent({
  setup(_) {
    const { items, itemSelected } = sectionsStore;
    const itemsAsOptions = computed(() =>
      (items.value ?? []).map((i) => ({
        label: `${i.name} (id: ${i.id})`,
        value: i.id,
      })),
    );
    const selectedItem = computed({
      get: () => itemSelected.value?.data,
      set: (val) => {
        const mode = itemSelected.value?.mode;
        console.log({ val, mode });

        if (mode == null || val == null) {
          return;
        }
        itemSelected.value = new NotValidData(val, mode);
      },
    });
    const parentId = computed({
      get: () => ({
        label: selectedItem.value?.name ?? 'Не выбрано',
        value: selectedItem.value?.parentSectionId ?? null,
      }),
      set: (option) => {
        if (selectedItem.value == null) {
          return;
        }
        selectedItem.value = {
          ...selectedItem.value,
          parentSectionId: option.value ?? null,
        } as unknown as SectionModel;
        console.log({ option });
      },
    });
    return { itemsAsOptions, parentId };
  },
});
</script>

<style scoped></style>
