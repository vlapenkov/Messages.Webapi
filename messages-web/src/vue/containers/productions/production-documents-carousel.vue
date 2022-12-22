<template>
  <div ref="containerRef" :style="itemStyle">
    <carousel
      v-if="images.length > 1"
      :numVisible="1"
      :numScroll="1"
      class="re-skin-caroulsel"
      circular
      :value="images"
    >
      <template #item="{ data }">
        <div class="flex h-full align-items-center justify-content-center max-h-full lh-0">
          <prime-image class="image-rounded" preview :src="data.src" :alt="data.name" />
        </div>
      </template>
    </carousel>
    <template v-else-if="images.length === 1" v-for="{ src, name } in images" :key="src">
      <prime-image class="lh-0 image-rounded image-full-width" preview :src="src" :alt="name" />
    </template>
    <template v-else>
      <img
        class="lh-0 w-full"
        preview
        :src="require('@/assets/images/fallback-image.png')"
        alt="placeholder"
      />
    </template>
  </div>
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
        return {
          src: `${process.env.VUE_APP_API_URL}api/files/${doc.fileId}/${ext}`,
          name: doc.fileName,
        };
      }),
    );
    const containerRef = ref();
    const { width: carouselWidth } = useElementSize(containerRef);

    const itemStyle = computed<CSSProperties>(() => ({
      '--prime-image-carousel-max-width': `${carouselWidth.value - 96}px`,
      '--prime-image-max-width': `${carouselWidth.value}px`,
    }));

    return { images, containerRef, itemStyle };
  },
});
</script>

<style lang="scss" scoped>
@import '@/assets/styles/_variables.scss';

.re-skin-caroulsel {
  :deep(.p-carousel-indicator) {
    button {
      background: map-get($colors, 'weak');
      border-radius: 14.9049px;
      height: 4px;
      box-shadow: none;
    }
    &.p-highlight {
      button {
        background: #686b76;
      }
    }
  }
  :deep(img) {
    max-width: var(--prime-image-carousel-max-width);
    width: var(--prime-image-carousel-max-width);
  }
}

$radius: var(--border-radius);
.image-rounded {
  :deep(img) {
    border-radius: $radius;
  }
  :deep(.p-image-preview-indicator) {
    border-radius: $radius;
  }
}

.image-full-width {
  :deep(img) {
    max-width: var(--prime-image-max-width);
    width: var(--prime-image-max-width);
  }
}

.lh-0 {
  line-height: 0;
}
</style>
