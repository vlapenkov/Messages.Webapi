<template>
  <input-switch class="mt-1" v-model="checked" />
</template>

<script lang="ts">
import type { ShoppingCartModel } from '@/app/shopping-cart/models/shopping-cart.model';
import { computed, defineComponent, Ref, WritableComputedRef } from 'vue';
import { itemsCollectionProvider } from '../base/presentational/state/collection/providers/items-collection.provider';
import { modelProvider } from '../base/presentational/state/collection/providers/model-provider';

export default defineComponent({
  setup() {
    const itemsRef = itemsCollectionProvider.inject();
    const itemRef = modelProvider.inject() as Ref<ShoppingCartModel | undefined>;
    if (itemsRef.value == null || itemRef.value == null) {
      throw new Error('Что-то пошло не так!');
    }
    const items = itemsRef.value() as WritableComputedRef<ShoppingCartModel[] | null>;

    const checked = computed({
      get: () => itemRef.value?.selected ?? false,
      set: (val) => {
        if (items.value == null || itemRef.value == null) {
          return;
        }
        const currentItemey = itemRef.value.key;
        const itemsToModify = items.value.map((i) => i.clone());
        const indexfToChange = itemsToModify.findIndex((i) => i.key === currentItemey);
        if (indexfToChange < 0) {
          return;
        }
        itemsToModify[indexfToChange].selected = val;
        items.value = itemsToModify;
      },
    });

    return { checked };
  },
});
</script>

<style scoped></style>
