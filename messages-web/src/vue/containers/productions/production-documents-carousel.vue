<template>
  <carousel
    class="re-skin"
    ref="carouselRef"
    :numVisible="1"
    :numScroll="1"
    circular
    :value="images"
  >
    <template #item="{ data }">
      <div
        class="flex h-full align-items-center justify-content-center max-h-full"
        style="line-height: 0"
      >
        <prime-image :style="itemStyle" preview :src="data.src" alt="document image" />
      </div>
    </template>
  </carousel>
</template>

<script lang="ts">
import { IProductDocument } from '@/app/product-full/@types/IProductDocument';
import { computed, CSSProperties, defineComponent, PropType, ref } from 'vue';
import Image from 'primevue/image';
import { useElementSize } from '@vueuse/core';

export default defineComponent({
  components: { PrimeImage: Image },
  props: {
    docs: {
      type: Array as PropType<IProductDocument[]>,
      default: () => [],
    },
  },
  setup(props) {
    const images = computed(() =>
      props.docs.map((doc) => {
        const splitted = doc.fileName.split('.');
        const ext = splitted[splitted.length - 1];
        return { src: `${process.env.VUE_APP_API_URL}api/files/${doc.fileId}/${ext}` };
      }),
    );
    const carouselRef = ref();
    const { width: carouselWidth } = useElementSize(carouselRef);

    const itemStyle = computed<CSSProperties>(() => ({
      '--prime-image-max-width': `${carouselWidth.value - 96}px`,
    }));

    return { images, carouselRef, itemStyle };
  },
});
</script>

<style lang="scss" scoped>
$radius: var(--border-radius);
.re-skin {
  :deep(.p-carousel-indicator) {
    button {
      background: #686b76;
      border-radius: 14.9049px;
    }
  }
  :deep(img) {
    max-width: var(--prime-image-max-width);
    border-radius: $radius;
  }
  :deep(.p-image-preview-indicator) {
    border-radius: $radius;
  }
}
</style>
