<template>
  <div class="min-h-full flex flex-column flex-grow-1">
    <collection-state
      :modes="[{ label: 'Сеткой', mode: 'data-view' }]"
      class="flex-grow-1"
      :state="productShortsStore"
    >
      <template #toolbar-end>
        <prime-button-add :disabled="sectionId == null" @click="addNewProduct" />
      </template>
      <template #data-view>
        <data-view-collection background></data-view-collection>
      </template>
    </collection-state>
    <prime-dialog v-model:visible="showAddProductDialog">
      <single-item-state :state="productFullStore"></single-item-state>
    </prime-dialog>
  </div>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { sectionId, productShortsStore } from '@/app/product-shorts/state/product-shorts.store';
import { PrimeDialog } from '@/tools/prime-vue-components';
import { computed, defineComponent, ref, watch } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  setup() {
    const route = useRoute();
    watch(
      () => route.params.sectionId,
      (id) => {
        if (id == null) {
          if (sectionId.value != null) {
            sectionId.value = undefined;
          }
        } else {
          sectionId.value = +id;
        }
      },
      {
        immediate: true,
      },
    );

    const showAddProductDialog = ref(false);
    const mode = computed(() => productFullStore.itemSelected?.value?.mode);
    const addNewProduct = () => {
      if (productFullStore.createItem == null) {
        return;
      }
      productFullStore.createItem();

      const selectedItem = productFullStore.itemSelected?.value?.data;
      if (selectedItem == null) {
        return;
      }
      if (
        productFullStore.itemSelected?.value != null &&
        sectionId.value != null &&
        mode.value != null
      ) {
        const clone = selectedItem.clone();
        clone.catalogSectionId = sectionId.value;
        productFullStore.itemSelected.value = new NotValidData(clone, mode.value);
        showAddProductDialog.value = true;
      }
    };
    return {
      productShortsStore,
      sectionId,
      productFullStore,
      mode,
      addNewProduct,
      showAddProductDialog,
    };
  },
  components: { PrimeDialog },
});
</script>

<style scoped></style>
