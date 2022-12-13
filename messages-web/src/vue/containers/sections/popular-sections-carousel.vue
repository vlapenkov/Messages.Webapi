<template>
  <carousel
    :value="itemsWithDocumentId"
    :numVisible="4"
    :numScroll="1"
    :showIndicators="false"
    :showNavigators="true"
    :responsiveOptions="responsiveOptions"
    class="popular-sections-carousel"
  >
    <template #item="slotProps">
      <div
        class="flex flex-column"
        :style="{
          marginLeft: slotProps.index > 0 ? '0.5rem' : undefined,
          cursor: 'pointer',
        }"
      >
        <product-image
          :id="slotProps.data.documentId"
          :max-height="162"
          :min-width="160"
          :fit-width="true"
          :header-text="slotProps.data.name"
          @click="viewSection(slotProps.data)"
        />
      </div>
    </template>
  </carousel>
</template>

<script lang="ts">
import { SectionModel } from '@/app/sections/models/section.model';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { useSections } from '@/composables/sections.composable';
import { computed, defineComponent } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  setup() {
    const state = sectionsStore;

    const { list: items } = useSections();
    const itemsWithDocumentId = computed(() =>
      (items.value ?? []).filter((x) => x.documentId != null && x.documentId !== ''),
    );
    const responsiveOptions = [
      {
        breakpoint: '1400px',
        numVisible: 3,
        numScroll: 1,
      },
      {
        breakpoint: '1024px',
        numVisible: 2,
        numScroll: 1,
      },
      {
        breakpoint: '480px',
        numVisible: 1,
        numScroll: 1,
      },
    ];
    const router = useRouter();
    const viewSection = (item: SectionModel) => {
      router.push({ name: 'catalog', params: { id: item.id } });
    };
    return { state, items, itemsWithDocumentId, responsiveOptions, viewSection };
  },
});
</script>

<style lang="scss">
// .popular-sections-carousel {
//   :deep(.p-disabled) {
//     display: none;
//   }
// }
</style>
