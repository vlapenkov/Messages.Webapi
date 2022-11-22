<template>
  <div class="min-h-full flex flex-column flex-grow-1 imaged">
    <data-view></data-view>
  </div>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { ProductShortModel } from '@/app/product-shorts/models/product-short.model';
import { productShortsStore } from '@/app/product-shorts/state/product-shorts.store';
import { addToCard } from '@/app/shopping-cart/infrastructure/shopping-cart.http-service';
import { computed, defineComponent, ref, watch } from 'vue';

export default defineComponent({
  props: {
    categoryId: {
      type: Number,
    },
  },
  setup(props) {
    watch(
      () => props.categoryId,
      (id) => {
        productShortsStore.parentSectionId.value = id;
      },
      {
        immediate: true,
      },
    );

    const showFullProductDialog = ref(false);
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
        productShortsStore.parentSectionId.value != null &&
        mode.value != null
      ) {
        const clone = selectedItem.clone();
        clone.catalogSectionId = productShortsStore.parentSectionId.value;
        productFullStore.itemSelected.value = new NotValidData(clone, mode.value);
        showFullProductDialog.value = true;
      }
    };

    const saveChanges = async () => {
      if (productFullStore.saveChanges == null) {
        return;
      }
      await productFullStore.saveChanges();
      showFullProductDialog.value = false;
    };
    const selectProduct = (item: ProductShortModel) => {
      productFullStore
        .getDataAsync({
          force: true,
          arguments: item.id,
        })
        .then(() => {
          if (productFullStore.selectItem != null) {
            showFullProductDialog.value = true;
          }
        });
    };

    const productTitle = computed(
      () => (productFullStore.itemSmart().value.title.value as string) ?? '',
    );

    const intoCart = (item: ProductShortModel) => {
      addToCard({
        productId: item.id,
        quantity: 1,
      });
    };

    return {
      productShortsStore,
      productFullStore,
      mode,
      addNewProduct,
      showFullProductDialog,
      saveChanges,
      selectProduct,
      productTitle,
      intoCart,
    };
  },
});
</script>

<style lang="scss">
.re-padding {
  .p-card {
    box-shadow: none;
  }
  .p-dialog-content {
    padding: 1rem;
    .p-card-body {
      padding: 0;
    }
    .p-card-content {
      padding-bottom: 0;
    }
  }
  .p-dialog-header {
    padding-bottom: 0;
  }
}
</style>
