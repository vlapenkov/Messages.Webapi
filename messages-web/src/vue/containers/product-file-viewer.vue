<template>
  <div class="mt-2 grid">
    <div class="col-12 md:col-6" v-for="doc in documents" :key="doc.fileId">
      <product-document-display :document="doc"></product-document-display>
    </div>
  </div>
</template>

<script lang="ts">
import { IProductDocument } from '@/app/product-full/@types/IProductDocument';
import { computed, defineComponent, PropType } from 'vue';

export default defineComponent({
  props: {
    files: {
      type: Array as PropType<IProductDocument[]>,
    },
  },
  setup(props) {
    const documents = computed(() => props.files ?? []);
    return { documents };
  },
});
</script>

<style scoped></style>
