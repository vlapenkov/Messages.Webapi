<template>
  <chips v-model="attributeChips" separator="," />
</template>

<script lang="ts">
import { IProductAttribute } from '@/app/product-full/@types/IProductAttribute';
import type { ProductFullModel } from '@/app/product-full/models/product-full.model';
import { computed, defineComponent, toRaw } from 'vue';
import { useSelectedData } from '../base/presentational/state/collection/composables/selected-data.composable';

export default defineComponent({
  setup() {
    const product = useSelectedData<ProductFullModel>();

    const attributes = computed({
      get: () => product.value?.attributeValues ?? null,
      set: (docs) => {
        console.log('Обновляем атрибуты', docs);

        const oldProduct = product.value;
        console.log({ oldProduct, docs });
        if (oldProduct == null || docs == null) {
          return;
        }
        const newProduct = oldProduct.clone();
        newProduct.attributeValues = docs;
        product.value = newProduct;
      },
    });

    const attributeChips = computed({
      get: () => (attributes.value ?? []).map((a) => a.value),
      set: (newChips) => {
        console.log('saving', { newChips, attrs: toRaw(attributes.value) });

        if (product.value == null) {
          return;
        }
        if (newChips.length > (attributes.value?.length ?? 0)) {
          const attsToPush: IProductAttribute[] = [];
          for (let i = attributes.value?.length ?? 0; i < newChips.length; i += 1) {
            console.log({ i });

            const newAttr = newChips[i];
            attsToPush.push({
              attributeId: i,
              value: newAttr,
              baseProductId: product.value.id,
            });
          }
          const attrs = [...(attributes.value ?? [])];
          attrs.push(...attsToPush);
          attributes.value = attrs;
        } else if (attributes.value != null && newChips.length < attributes.value.length) {
          const attrsToChange = attributes.value.map((i) => toRaw(i));
          for (let i = 0; i < attributes.value.length; i += 1) {
            const chip = newChips[i];
            if (i < newChips.length) {
              if (attrsToChange[i] != null && chip !== attrsToChange[i].value) {
                attrsToChange.splice(i, 1);
              }
            } else {
              attrsToChange.splice(i, 1);
            }
          }
          attributes.value = attrsToChange;
        }
      },
    });

    return { attributeChips };
  },
});
</script>

<style scoped></style>
