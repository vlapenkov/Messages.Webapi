<template>
  <div :style="imageStyle" class="max-height border-round overflow-hidden">
    <img
      v-if="documents != null && documents.length > 0 && imageData != null"
      class="border-round"
      :style="{ width: '100%' }"
      :class="{ 'max-w-full': fitWidth }"
      :src="imageData"
      alt="Изображение товара"
    />
    <skeleton
      v-else
      :width="minWidth ? '' + minWidth + 'px' : undefined"
      :height="'' + maxHeight + 'px'"
    ></skeleton>
  </div>
</template>

<script lang="ts">
import { IProductDocument } from '@/app/product-full/@types/IProductDocument';
import { computed, CSSProperties, defineComponent, PropType, ref, watch } from 'vue';

export default defineComponent({
  props: {
    fitWidth: {
      type: Boolean,
      default: false,
    },
    minWidth: {
      type: Number,
    },
    maxHeight: {
      type: Number,
      default: 300,
    },
    documents: {
      type: Array as PropType<IProductDocument[]>,
      default: () => [],
    },
  },
  setup(props) {
    const imageData = ref<string>();
    watch(
      () => props.documents,
      (docs) => {
        if (docs != null && docs.length > 0) {
          imageData.value = `data:image/png;base64,${docs[0].data}`;
        }
      },
      {
        immediate: true,
      },
    );

    const imageStyle = computed<CSSProperties>(() => ({
      '--custom--image--max-height': `${props.maxHeight}px`,
    }));

    return { imageData, imageStyle };
  },
});
</script>

<style lang="scss" scoped>
.max-height {
  max-height: var(--custom--image--max-height);
}
</style>
