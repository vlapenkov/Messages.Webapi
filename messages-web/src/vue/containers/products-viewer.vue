<template>
  <div class="min-h-full flex flex-column flex-grow-1 imaged">
    <collection-state
      :modes="[{ label: 'Сеткой', mode: 'data-view' }]"
      class="flex-grow-1"
      :state="productShortsStore"
    >
      <template #actions>
        <prime-button-add
          label="Добавить товар"
          :disabled="categoryId == null"
          @click="addNewProduct"
        />
      </template>
      <template #data-view>
        <data-view-collection>
          <template #item-footer="{ data }">
            <div class="flex flex-row justify-content-end gap-2">
              <prime-button @click="intoCart(data)" icon="pi pi-shopping-cart" label="В корзину" />
              <prime-button
                icon="pi pi-eye"
                class="p-button-secondary"
                @click="selectProduct(data)"
              />
            </div>
          </template>
        </data-view-collection>
      </template>
    </collection-state>
    <prime-dialog
      :header="
        mode === 'create'
          ? 'Создание нового товара'
          : mode === 'edit'
          ? 'Редактирование товара'
          : productTitle
      "
      :breakpoints="{ '900px': '75vw', '720px': '90vw' }"
      :style="{ 'width': '50vw', 'max-width': '800px' }"
      class="re-padding"
      :draggable="false"
      modal
      v-model:visible="showFullProductDialog"
    >
      <div class="px-2">
        <single-item-state :state="productFullStore">
          <template #footer-edit>
            <div class="flex flex-row justify-content-end">
              <prime-button-add
                @click="saveChanges"
                label="Добавить товар"
                v-if="mode === 'create'"
              />
              <prime-button-save
                @click="saveChanges"
                label="Сохранить изменения"
                v-if="mode === 'edit'"
              />
            </div>
          </template>
        </single-item-state>
      </div>
    </prime-dialog>
  </div>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { ProductShortModel } from '@/app/product-shorts/models/product-short.model';
import { sectionId, productShortsStore } from '@/app/product-shorts/state/product-shorts.store';
import { addToCard } from '@/app/shopping-cart/infrastructure/shopping-cart.http-service';
import { PrimeDialog } from '@/tools/prime-vue-components';
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
        sectionId.value = id;
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
        sectionId.value != null &&
        mode.value != null
      ) {
        const clone = selectedItem.clone();
        clone.catalogSectionId = sectionId.value;
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
  components: { PrimeDialog },
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
