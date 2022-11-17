<template>
  <div class="min-h-full flex flex-column flex-grow-1">
    <collection-state
      :modes="[{ label: 'Сеткой', mode: 'data-view' }]"
      class="flex-grow-1"
      :state="productShortsStore"
    >
      <template #toolbar-end>
        <prime-button-add
          label="Добавить товар"
          :disabled="categoryId == null"
          @click="addNewProduct"
        />
      </template>
      <template #data-view>
        <data-view-collection>
          <template #item-footer="{ data }">
            <prime-button-edit @click="selectProduct(data)" />
          </template>
        </data-view-collection>
      </template>
    </collection-state>
    <prime-dialog
      :header="mode === 'create' ? 'Создание нового товара' : 'Редактирование товара'"
      :breakpoints="{ '900px': '75vw', '720px': '90vw' }"
      :style="{ 'width': '50vw', 'max-width': '800px' }"
      class="re-padding"
      :draggable="false"
      modal
      v-model:visible="showFullProductDialog"
    >
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
    </prime-dialog>
  </div>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { ProductShortModel } from '@/app/product-shorts/models/product-short.model';
import { sectionId, productShortsStore } from '@/app/product-shorts/state/product-shorts.store';
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
      console.log({ item });

      productFullStore
        .getDataAsync({
          force: true,
          arguments: item.id,
        })
        .then(() => {
          if (productFullStore.selectItem != null) {
            productFullStore.selectItem();
            showFullProductDialog.value = true;
          }
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
