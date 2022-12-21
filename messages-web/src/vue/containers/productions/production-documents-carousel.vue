<template>
  <carousel class="re-skin" :numVisible="1" :numScroll="1" circular :value="images">
    <template #item="{ data }">
      <div class="flex h-full align-items-center justify-content-center">
        <prime-image preview :src="data.src" alt="document image" />
      </div>
    </template>
  </carousel>
</template>

<script lang="ts">
import { IProductDocument } from '@/app/product-full/@types/IProductDocument';
import { computed, defineComponent, PropType } from 'vue';
import Image from 'primevue/image';

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
    return { images };
  },
});
</script>

<style lang="scss" scoped>
.re-skin {
  :deep(.p-carousel-indicator) {
    button {
      background: #686b76;
      border-radius: 14.9049px;
    }
  }
}
</style>
