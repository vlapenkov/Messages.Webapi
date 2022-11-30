<template>
  <carousel
    :value="itemsWithDocumentId"
    :numVisible="4"
    :numScroll="1"
    :showIndicators="false"
    :showNavigators="true"
  >
    <template #item="slotProps">
      <div
        class="flex flex-column"
        :style="{
          marginLeft: slotProps.index > 0 ? '0.5rem' : undefined,
        }"
      >
        <product-image
          :id="slotProps.data.documentId"
          :max-height="162"
          :min-width="160"
          :fit-width="true"
          :header-text="slotProps.data.name"
        />
      </div>
    </template>
  </carousel>
</template>

<script lang="ts">
import { SectionModel } from '@/app/sections/models/section.model';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { CollectionStoreMixed } from '@/vue/base/presentational/state/collection/collection-state.vue';
import { computed, defineComponent, WritableComputedRef } from 'vue';

export default defineComponent({
  setup() {
    const state = sectionsStore as CollectionStoreMixed;
    if (state.items == null) {
      throw new Error('Что-то пошло не так');
    }
    const items = state.items({ force: true }) as WritableComputedRef<SectionModel[]>;
    const itemsWithDocumentId = computed(() =>
      (items.value ?? []).filter((x) => x.documentId != null && x.documentId !== ''),
    );
    return { state, items, itemsWithDocumentId };
  },
});
</script>

<style scoped></style>
