<template>
  <dropdown v-model="parentId" option-label="label" :options="itemsAsOptions"></dropdown>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import type { SectionModel } from '@/app/sections/models/section.model';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { useSections } from '@/composables/sections.composable';
import { computed, defineComponent } from 'vue';

export default defineComponent({
  setup() {
    const { sectionSelected } = sectionsStore;
    const { list: items } = useSections();
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
      get: () => sectionSelected.value?.data as SectionModel,
      set: (val) => {
        const mode = sectionSelected.value?.mode;
        // console.log({ val, mode });

        if (mode == null || val == null || sectionSelected.value == null) {
          return;
        }
        sectionSelected.value = new NotValidData(val, mode);
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
        // ({ option });
      },
    });
    return { itemsAsOptions, parentId };
  },
});
</script>

<style scoped></style>
