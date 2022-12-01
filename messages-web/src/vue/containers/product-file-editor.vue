<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div>
    <label>Вложения</label>
    <div class="mt-2 grid">
      <div class="col-12 md:col-6" v-for="doc in documents" :key="doc.fileId">
        <product-document-display :document="doc"></product-document-display>
      </div>
    </div>
    <div>
      <input mt-2 type="file" @input="onFileInput" />
    </div>
  </div>
</template>

<script lang="ts">
import type { ProductFullModel } from '@/app/product-full/models/product-full.model';
import { useBase64 } from '@vueuse/core';
import { computed, defineComponent, Ref, ref, toRaw, watch } from 'vue';
import { v4 as uuidv4 } from 'uuid';
import { useSelectedData } from '../base/presentational/state/collection/composables/selected-data.composable';

export default defineComponent({
  setup() {
    const product = useSelectedData<ProductFullModel>();
    const documents = computed({
      get: () => product.value?.documents ?? null,
      set: (docs) => {
        const oldProduct = product.value;
        // console.log({ oldProduct, docs });
        if (oldProduct == null || docs == null) {
          return;
        }
        const newProduct = oldProduct.clone();
        newProduct.documents = docs;
        product.value = newProduct;
      },
    });

    const file = ref() as Ref<File>;

    const { base64: fileB64 } = useBase64(file);

    watch(fileB64, (b64) => {
      if (file.value == null || b64 == null) {
        return;
      }
      const docs = (documents.value ?? []).map((x) => toRaw(x));
      docs.push({
        data: b64,
        fileId: uuidv4(),
        fileName: file.value.name,
      });
      documents.value = docs;
    });

    const onFileInput = (e: Event) => {
      const target = e.target as HTMLInputElement;
      const { files } = target;
      if (files == null) {
        return;
      }
      [file.value] = files;
    };

    return { documents, onFileInput };
  },
});
</script>

<style scoped></style>
