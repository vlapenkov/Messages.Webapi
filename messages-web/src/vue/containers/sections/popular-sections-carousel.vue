<template>
  <div class="grid">
    <div class="col-12">
      <carousel
        :value="itemsWithDocumentId"
        :numVisible="4"
        :numScroll="1"
        :showIndicators="false"
        :showNavigators="true"
        :responsiveOptions="responsiveOptions"
        class="popular-sections-carousel"
        :style="carouselStyle"
      >
        <template #item="slotProps">
          <div
            class="flex flex-column"
            :style="{
              marginLeft: slotProps.index > 0 ? '0.5rem' : undefined,
              cursor: 'pointer',
            }"
          >
            <file-store-image
              v-if="slotProps.data"
              :id="slotProps.data.documentId"
              :max-height="162"
              :min-height="162"
              :min-width="160"
              :fit-width="true"
              :header-text="slotProps.data.name"
              @click="viewSection(slotProps.data)"
            >
              <app-text
                tag="div"
                mode="image"
                class="absolute top-0 left-0 text-white text-left p-3"
              >
                {{ slotProps.data.name }}
              </app-text>
            </file-store-image>
            <skeleton v-else height="162px"> </skeleton>
          </div>
        </template>
      </carousel>
    </div>
  </div>
</template>

<script lang="ts">
import { screenLarge } from '@/app/core/services/window/window.service';
import { SectionModel } from '@/app/sections/models/section.model';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { useSections } from '@/composables/sections.composable';
import { computed, CSSProperties, defineComponent, ref } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  setup() {
    const state = sectionsStore;

    const { list: items } = useSections();
    const itemsWithDocumentId = computed(() =>
      items.value == null
        ? [null, null, null, null]
        : items.value.filter((x) => x.documentId != null && x.documentId !== ''),
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
    const scaleKoef = ref(1.0835);
    const carouselStyle = computed<CSSProperties>(() => ({
      transform: screenLarge.value ? `scale(${scaleKoef.value},${scaleKoef.value})` : undefined,
    }));
    const viewSection = (item: SectionModel) => {
      router.push({ name: 'catalog', params: { id: item.id }, query: { sectionId: item.id } });
    };
    return { state, items, itemsWithDocumentId, responsiveOptions, viewSection, carouselStyle };
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
