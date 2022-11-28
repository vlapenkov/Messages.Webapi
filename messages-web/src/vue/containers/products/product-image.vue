<template>
  <div :style="imageStyle" class="max-height border-round overflow-hidden">
    <img
      v-if="id != null && imageData != null"
      class="border-round"
      :style="{ minWidth: minWidth != null ? minWidth + 'px' : undefined }"
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
import { http } from '@/app/core/services/http/axios/axios.service';
import { computed, CSSProperties, defineComponent, ref, watch } from 'vue';

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
    id: {
      type: String,
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
