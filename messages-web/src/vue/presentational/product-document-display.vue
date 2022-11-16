<!-- eslint-disable vue/component-name-in-template-casing -->
<template>
  <img class="max-w-full" :src="b64Image" ref="imgRef" :alt="document.fileName" />
</template>

<script lang="ts">
import { IProductDocument } from '@/app/product-full/@types/IProductDocument';
import { useBase64 } from '@vueuse/core';
import { computed, defineComponent, PropType, Ref, ref, watch } from 'vue';

export default defineComponent({
  props: {
    document: {
      type: Object as PropType<IProductDocument>,
      required: true,
    },
  },
  setup(props) {
    const imgRef = ref() as Ref<HTMLImageElement>;
    const { base64: b64Image } = useBase64(imgRef);
    const fileData = computed(() => props.document.data);
    watch(
      fileData,
      (fd) => {
        b64Image.value = fd;
      },
      {
        immediate: true,
      },
    );

    return { imgRef, b64Image };
  },
});
</script>

<style scoped></style>
