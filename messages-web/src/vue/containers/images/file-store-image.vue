<template>
  <div class="file-store-image">
    <div :style="imageContainerStyle" class="max-height border-round overflow-hidden container">
      <transition-fade>
        <img
          v-if="imageData"
          class="border-round"
          :style="imageStyle"
          :class="{ 'min-w-full max-w-full': fitWidth }"
          :src="imageData"
          :alt="altText ?? 'Изображение товара'"
        />

        <img
          v-else
          class="border-round"
          :style="imageStyle"
          :class="{ 'min-w-full max-w-full': fitWidth }"
          :src="require('@/assets/images/fallback-image.png')"
          :alt="altText ?? 'Изображение товара'"
        />
      </transition-fade>
      <slot></slot>
    </div>
  </div>
</template>

<script lang="ts">
import { http } from '@/app/core/services/http/axios/axios.service';
import { computed, CSSProperties, defineComponent, PropType, ref, watch } from 'vue';

export default defineComponent({
  props: {
    fitWidth: {
      type: Boolean,
      default: false,
    },
    maxWidth: {
      type: Number,
      default: null,
    },
    minWidth: {
      type: Number,
      default: null,
    },
    maxHeight: {
      type: Number,
      default: 100,
    },
    minHeight: {
      type: Number,
      default: null,
    },
    objectFit: {
      type: String as PropType<CSSProperties['objectFit']>,
      default: null,
    },
    id: {
      type: String,
    },
    altText: {
      type: String,
      default: null,
    },
  },
  setup(props) {
    const imageData = ref<string>();
    const animationMode = ref<'wave' | 'none'>('wave');
    watch(
      () => props.id,
      (idVal) => {
        if (idVal != null && idVal !== '' && idVal.trim() !== '') {
          animationMode.value = 'wave';
          http.get(`api/files/${idVal}`).then((response) => {
            if (response.status === 200) {
              imageData.value = `data:image/png;base64,${response.data}`;
            } else {
              animationMode.value = 'none';
            }
          });
        } else {
          imageData.value = undefined;
          animationMode.value = 'none';
        }
      },
      {
        immediate: true,
      },
    );

    const imageContainerStyle = computed<CSSProperties>(() => ({
      '--custom--image--max-height': `${props.maxHeight}px`,
    }));

    const imageStyle = computed<CSSProperties>(() => ({
      maxWidth: props.maxWidth != null ? `${props.maxWidth}px` : undefined,
      minWidth: props.minWidth != null ? `${props.minWidth}px` : undefined,
      minHeight: props.minHeight != null ? `${props.minHeight}px` : undefined,
      objectFit: props.objectFit != null ? props.objectFit : undefined,
    }));

    return { imageData, imageContainerStyle, imageStyle, animationMode };
  },
});
</script>

<style lang="scss" scoped>
.file-store-image {
  .max-height {
    max-height: var(--custom--image--max-height);
  }
  .container {
    position: relative;
    text-align: center;
    color: white;
  }
}
</style>
