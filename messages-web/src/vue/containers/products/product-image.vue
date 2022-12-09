<template>
  <div class="product-image">
    <div :style="imageContainerStyle" class="max-height border-round overflow-hidden container">
      <img
        v-if="id != null && imageData != null"
        class="border-round"
        :style="imageStyle"
        :class="{ 'max-w-full': fitWidth }"
        :src="imageData"
        :alt="headerText ?? 'Изображение товара'"
      />
      <skeleton
        v-else
        :width="minWidth ? '' + minWidth + 'px' : undefined"
        :height="'' + maxHeight + 'px'"
      />
      <span
        v-if="headerText != null && id != null && imageData != null"
        class="top-left text-xl text-left"
        >{{ headerText }}</span
      >
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
    minWidth: {
      type: Number,
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
    headerText: {
      type: String,
      default: null,
    },
  },
  setup(props) {
    const imageData = ref<string>();
    watch(
      () => props.id,
      (idVal) => {
        if (idVal != null && idVal !== '' && idVal.trim() !== '') {
          http.get(`api/files/${idVal}`).then((response) => {
            if (response.status === 200) {
              imageData.value = `data:image/png;base64,${response.data}`;
            }
          });
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
      minWidth: props.minWidth != null ? `${props.minWidth}px` : undefined,
      minHeight: props.minHeight != null ? `${props.minHeight}px` : undefined,
      objectFit: props.objectFit != null ? props.objectFit : undefined,
    }));

    return { imageData, imageContainerStyle, imageStyle };
  },
});
</script>

<style lang="scss" scoped>
.product-image {
  .max-height {
    max-height: var(--custom--image--max-height);
  }
  .container {
    position: relative;
    text-align: center;
    color: white;
  }
  .top-left {
    position: absolute;
    top: 8px;
    left: 16px;
  }
}
</style>
