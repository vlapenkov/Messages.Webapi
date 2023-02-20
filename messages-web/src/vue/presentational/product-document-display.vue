<!-- eslint-disable vue/component-name-in-template-casing -->
<template>
  <img
    class="max-w-full border-round fit-cover"
    :src="fileData"
    ref="imgRef"
    :alt="document.fileName"
  />
</template>

<script lang="ts">
import { IProductDocument } from '@/app/product-full/@types/IProductDocument';
import { computed, defineComponent, PropType, Ref, ref } from 'vue';

export default defineComponent({
  props: {
    document: {
      type: Object as PropType<IProductDocument>,
      required: true,
    },
  },
  setup(props) {
    const imgRef = ref() as Ref<HTMLImageElement>;

    const fileExt = computed(() => props.document.fileName.split('.')[1]);

    const fileData = computed(
      () =>
        `data:image/${fileExt.value};base64,${props.document.data.replace(
          /data:image\/(\w*);base64,/,
          '',
        )}`,
    );

    return { imgRef, fileData };
  },
});
</script>

<style lang="scss" scoped>
.fit-cover {
  object-fit: cover;
}
</style>
