<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page :title="pageTitle">
    <div class="grid p-fluid">
      <div class="field col-12 md:col-3">
        <span class="p-float-label">
          <input-text id="productName" type="text" v-model="models.name.value" />
          <label for="productName">Название товара</label>
        </span>
      </div>
      <div class="field col-12">
        <span class="p-float-label">
          <prime-textarea
            id="productName"
            auto-resize
            type="text"
            v-model="models.description.value"
          />
          <label for="productName">Название товара</label>
        </span>
      </div>
    </div>
  </app-page>
</template>

<script lang="ts">
import { ProductFullModel } from '@/app/product-full/models/product-full.model';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { PrimeTextarea } from '@/tools/prime-vue-components';
import { defineComponent, onMounted, computed } from 'vue';

export default defineComponent({
  components: { PrimeTextarea },
  setup() {
    if (productFullStore.itemSelected == null) {
      throw new Error('Что-то не так с состоянием редактируемого товара');
    }
    const item = productFullStore.itemSelected;

    const mode = computed(() => productFullStore.itemSelected?.value?.mode);

    const selectedData = computed({
      get: () => item.value?.data ?? null,
      set: (val) => {
        if (val == null || item.value == null) {
          return;
        }
        item.value.data = val;
      },
    });

    const pageTitle = computed(() =>
      mode.value === 'create' ? 'Добавление товара' : 'Редактирование товара',
    );

    const getModelFor = <TKey extends keyof ProductFullModel & string>(
      key: TKey,
      fallback: ProductFullModel[TKey],
    ) =>
      computed({
        get: () => (selectedData.value == null ? fallback : selectedData.value[key]),
        set: (val) => {
          if (selectedData.value == null || val == null) {
            return;
          }
          const cloned = selectedData.value.clone();
          cloned.setKey(key, val);
          selectedData.value = cloned;
        },
      });

    onMounted(() => {
      if (productFullStore.createItem != null) {
        productFullStore.createItem();
      }
    });

    const models = {
      name: getModelFor('name', ''),
      fullName: getModelFor('fullName', ''),
      description: getModelFor('description', ''),
      codeTnVed: getModelFor('codeTnVed', ''),
      price: getModelFor('price', 0),
    };

    return { item, getModelFor, pageTitle, models };
  },
});
</script>

<style scoped></style>
