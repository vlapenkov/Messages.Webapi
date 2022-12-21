<template>
  <carousel></carousel>
</template>

<script lang="ts">
import { IProductDocument } from '@/app/product-full/@types/IProductDocument';
import { computed, defineComponent, PropType } from 'vue';

export default defineComponent({
  props: {
    docs: {
      type: Array as PropType<IProductDocument[]>,
      default: () => [],
    },
  },
  setup(props) {
    const sources = computed(() =>
      props.docs.map((doc) => ({ src: `${process.env.VUE_APP_API_URL}api/files/${doc.fileId}` })),
    );
    return { sources };
  },
});
</script>

<style scoped></style>
