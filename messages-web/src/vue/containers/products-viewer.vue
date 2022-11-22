<template>
  <div class="grid">
    <template v-if="productShortsItems != null && productShortsItems.length > 0">
      <div v-for="item in productShortsItems" :key="item.id" class="col-3">
        <card class="h-full re-padding-card">
          <template #header>
            <product-image :id="item.documentId"></product-image>
          </template>
          <template #content>
            <div class="text-sm">{{ item.name }}</div>
            <div class="text-sm">{{ item.organization.name }}</div>
            <div class="text-sm">{{ item.organization.region }}</div>
            <router-link
              :to="{
                name: 'product',
                params: { id: item.id },
              }"
              style="text-decoration: none"
            >
              <template #default="{ href }">
                <prime-button
                  class="w-full p-button-sm py-1 mt-2"
                  label="редактировать"
                  :href="href"
                ></prime-button>
              </template>
            </router-link>
          </template>
        </card>
      </div>
    </template>
    <template v-else>
      <div v-for="i in 12" :key="i" class="col-3">
        <skeleton height="150px" />
      </div>
    </template>
  </div>
  <prime-paginator
    class="mt-2 border-1 shadow-1"
    v-if="pageNumber && pageSize && (currentPage?.totalItemCount ?? 0) > 0"
    @page="changePage"
    :rows="pageSize"
    :first="pageSize * (pageNumber - 1)"
    :totalRecords="currentPage?.totalItemCount ?? 0"
  ></prime-paginator>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { ProductShortModel } from '@/app/product-shorts/models/product-short.model';
import { productShortsStore } from '@/app/product-shorts/state/product-shorts.store';
import { addToCard } from '@/app/shopping-cart/infrastructure/shopping-cart.http-service';
import { computed, defineComponent, PropType, ref, watch } from 'vue';
import { PrimePaginator } from '@/tools/prime-vue-components';

export default defineComponent({
  components: { PrimePaginator },
  props: {
    viewMode: {
      type: String as PropType<'user' | 'admin'>,
      default: 'user',
    },
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
    const modeFull = computed(() => productFullStore.itemSelected?.value?.mode);
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
        modeFull.value != null
      ) {
        const clone = selectedItem.clone();
        clone.catalogSectionId = productShortsStore.parentSectionId.value;
        productFullStore.itemSelected.value = new NotValidData(clone, modeFull.value);
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

    const productShortsItems = computed(() => productShortsStore.currentPageItems.value);
    const { status: productShortsStatus, pageNumber, pageSize, currentPage } = productShortsStore;

    const changePage = ({ page }: { page: number }) => {
      pageNumber.value = page + 1;
    };

    return {
      addNewProduct,
      intoCart,
      modeFull,
      productFullStore,
      productShortsItems,
      productShortsStatus,
      productTitle,
      showFullProductDialog,
      saveChanges,
      selectProduct,
      pageNumber,
      pageSize,
      currentPage,
      changePage,
    };
  },
});
</script>

<style lang="scss" scoped>
.no-background {
  :deep(.p-dataview-content) {
    background-color: var(--surface-ground);
  }
}

.td-none {
  text-decoration: none !important;
}

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

.re-padding-card {
  :deep(.p-card-content) {
    padding-bottom: 0;
    padding-top: 0;
  }

  :deep(.p-card-body) {
    height: 100%;
    display: flex;
    flex-direction: column;
    .p-card-content {
      flex-grow: 1;
    }
  }
}
</style>
