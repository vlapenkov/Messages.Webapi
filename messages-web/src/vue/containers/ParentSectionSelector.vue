<template>
  <dropdown v-model="parentId" option-label="label" :options="itemsAsOptions"></dropdown>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import type { SectionModel } from '@/app/sections/models/section.model';
import { computed, defineComponent } from 'vue';
import { injectCollectionState } from '../base/containers/state/collection/CollectionState.vue';

export default defineComponent({
  setup() {
    const store = injectCollectionState();

    const itemsAsOptions = computed(() =>
      (store.value?.items()?.value ?? []).map((i) => {
        const item = i as SectionModel;
        return {
          label: `${item.name} (id: ${item.id})`,
          value: item.id,
        };
      }),
    );
    const selectedItem = computed({
      get: () => store.value?.itemSelected?.value?.data as SectionModel,
      set: (val) => {
        const mode = store.value?.itemSelected?.value?.mode;
        console.log({ val, mode });

        if (
          mode == null ||
          val == null ||
          store.value == null ||
          store.value?.itemSelected == null
        ) {
          return;
        }
        store.value.itemSelected.value = new NotValidData(val, mode);
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
