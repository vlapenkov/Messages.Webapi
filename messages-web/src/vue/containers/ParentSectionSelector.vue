<template>
  <dropdown v-model="parentId" option-label="label" :options="itemsAsOptions"></dropdown>
</template>

<script lang="ts">
import type { ICollectionEditableStore } from '@/app/core/services/harlem/custom/collection/editable/collection-editable.store';
import type { ISectionModel, SectionModel } from '@/app/sections/models/section.model';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { computed, defineComponent, ShallowRef } from 'vue';
import { injectCollectionState } from '../base/containers/state/CollectionState.vue';

export default defineComponent({
  setup() {
    const store = injectCollectionState() as ShallowRef<ICollectionEditableStore<
      ISectionModel,
      SectionModel
    > | null>;

    const itemsAsOptions = computed(() =>
      (store.value?.items.value ?? []).map((i) => ({
        label: `${i.name} (id: ${i.id})`,
        value: i.id,
      })),
    );
    const selectedItem = computed({
      get: () => store.value?.itemSelected.value?.data,
      set: (val) => {
        const mode = store.value?.itemSelected.value?.mode;
        console.log({ val, mode });

        if (mode == null || val == null || store.value == null) {
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
        const cloned = selectedItem.value.clone();
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
