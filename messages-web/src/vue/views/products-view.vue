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
          :disabled="sectionId == null"
          @click="addNewProduct"
        />
      </template>
      <template #data-view>
        <data-view-collection>
          <template #item-footer>
            <prime-button-edit />
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
      v-model:visible="showAddProductDialog"
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

    const saveChanges = async () => {
      if (productFullStore.saveChanges == null) {
        return;
      }
      await productFullStore.saveChanges();
      showAddProductDialog.value = false;
    };

    return {
      productShortsStore,
      sectionId,
      productFullStore,
      mode,
      addNewProduct,
      showAddProductDialog,
      saveChanges,
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
