<template>
  <div class="flex flex-column justify-content-between h-full">
    <toast position="top-right" group="tr" />
    <transition-fade>
      <template v-if="productShortsItems != null">
        <div v-if="productShortsItems.length > 0" class="grid">
          <template v-if="viewMode === 'user'">
            <div v-for="item in productShortsItems" :key="item.id" class="col-3">
              <card class="h-full re-padding-card">
                <template #header>
                  <product-image :max-height="140" :id="item.documentId"></product-image>
                </template>
                <template #content>
                  <div class="p-2 flex flex-column justify-content-between gap-1">
                    <prime-button
                      class="p-button-text text-sm font-bold custom-button-text"
                      @click="viewProduct(item)"
                    >
                      {{ item.name }}</prime-button
                    >
                    <div class="text-sm text-primary">{{ item.organization.name }}</div>
                    <div class="text-sm">{{ item.organization.region }}</div>
                    <div
                      class="flex flex-row gap-1 align-items-stretch justify-content-between mt-2"
                    >
                      <span>
                        <prime-button
                          @click="addToCart(item)"
                          class="p-button-sm h-full py-1"
                          label="Заказать"
                        >
                        </prime-button>
                      </span>

                      <prime-button
                        disabled
                        icon="pi pi-heart"
                        class="p-button-secondary py-1"
                      ></prime-button>
                      <prime-button
                        disabled
                        icon="pi pi-chart-bar"
                        class="p-button-secondary py-1"
                      ></prime-button>
                      <prime-button
                        disabled
                        icon="pi pi-arrows-h"
                        class="p-button-secondary py-1"
                      ></prime-button>
                    </div>
                  </div>
                </template>
              </card>
            </div>
          </template>
          <template v-else>
            <card class="mx-2 mt-2">
              <template #title>
                <div class="flex card-container blue-container overflow-hidden">
                  <div class="flex-none flex">
                    <span class="text-xl">Товары</span>
                  </div>
                  <div class="flex-grow-1 flex"></div>
                  <div class="flex-none flex">
                    <prime-button
                      icon="pi pi-plus"
                      class="p-button-sm py-1 px-1 ml-1"
                      @click="addNewProduct"
                    >
                    </prime-button>
                  </div>
                </div>
              </template>

              <template #content>
                <data-table :value="productShortsItems">
                  <column field="id" header="ИД"> </column>
                  <column field="name" header="Название"> </column>
                  <!-- <column field="description" header="Описание"> </column> -->
                  <column field="created" header="Создан">
                    <template #body="{ data }">
                      {{ (data.created as Date).toLocaleString() }}
                    </template>
                  </column>
                  <column field="lastModified" header="Изменён">
                    <template #body="{ data }">
                      {{ (data.lastModified as Date).toLocaleString() }}
                    </template>
                  </column>
                  <column field="price" header="Цена"> </column>
                  <column header="">
                    <template #body="{ data }">
                      <prime-button
                        icon="pi pi-pencil"
                        class="p-button-sm p-button py-2 px-0 mr-1"
                        @click="editProduct(data)"
                      ></prime-button>
                      <prime-button
                        icon="pi pi-trash"
                        class="p-button-sm p-button-danger py-2 px-0 mr-1"
                      >
                      </prime-button>
                    </template>
                  </column>
                </data-table>
              </template>
            </card>
          </template>
        </div>
        <div
          v-else
          style="background-color: var(--surface-card)"
          class="w-full h-full flex justify-content-center border-round align-items-center"
        >
          <div class="text-center">
            <i class="pi pi-inbox text-8xl opacity-50"></i>
            <div class="p-component text-lg mt-3">Товаров не найдено</div>
            <prime-button-add
              class="mt-2"
              v-if="viewMode === 'admin'"
              label="Добавить товар"
              @click="addNewProduct"
            />
          </div>
        </div>
      </template>
      <template v-else>
        <div class="grid">
          <div v-for="i in 12" :key="i" class="col-3">
            <skeleton height="150px" />
          </div>
        </div>
      </template>
    </transition-fade>
    <prime-paginator
      class="mt-2 border-1 shadow-1"
      v-if="pageNumber && pageSize && (currentPage?.totalItemCount ?? 0) > 0"
      @page="changePage"
      :rows="pageSize"
      :first="pageSize * (pageNumber - 1)"
      :totalRecords="currentPage?.totalItemCount ?? 0"
    ></prime-paginator>
  </div>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { ProductShortModel } from '@/app/product-shorts/models/product-short.model';
import { productShortsStore } from '@/app/product-shorts/state/product-shorts.store';
import { addToCart } from '@/app/shopping-cart/infrastructure/shopping-cart.http-service';
import { computed, defineComponent, watch } from 'vue';
import { PrimePaginator } from '@/tools/prime-vue-components';
import { useToast } from 'primevue/usetoast';
import { useRouter } from 'vue-router';
import Toast from 'primevue/toast';
import { viewModeProvider } from '../views/providers/view-mode.provider';

export default defineComponent({
  components: { PrimePaginator, Toast },
  props: {
    categoryId: {
      type: Number,
    },
  },
  setup(props) {
    const viewMode = viewModeProvider.inject();
    const toast = useToast();
    const router = useRouter();
    watch(
      () => props.categoryId,
      (id) => {
        productShortsStore.parentSectionId.value = id;
      },
      {
        immediate: true,
      },
    );

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
      if (productFullStore.itemSelected?.value != null && modeFull.value != null) {
        const clone = selectedItem.clone();
        clone.catalogSectionId = -1;
        productFullStore.itemSelected.value = new NotValidData(clone, modeFull.value);
        router.push('/edit-product');
      }
    };
    const viewProduct = (item: ProductShortModel) => {
      router.push({ name: 'product', params: { id: item.id } });
    };

    const saveChanges = async () => {
      if (productFullStore.saveChanges == null) {
        return;
      }
      await productFullStore.saveChanges();
    };

    const editProduct = (item: ProductShortModel) => {
      productFullStore
        .getDataAsync({
          force: true,
          arguments: {
            id: item.id,
          },
        })
        .then(() => {
          if (productFullStore.selectItem != null) productFullStore.selectItem();
          if (productFullStore.itemSelected?.value != null) {
            const clone = productFullStore.itemSelected.value.data.clone();
            productFullStore.itemSelected.value = new NotValidData(
              clone,
              productFullStore.itemSelected?.value.mode,
            );
            router.push('/edit-product');
          }
        });
    };

    const productTitle = computed(
      () => (productFullStore.itemSmart().value.title.value as string) ?? '',
    );

    const productShortsItems = computed(() => productShortsStore.currentPageItems.value);
    const { status: productShortsStatus, pageNumber, pageSize, currentPage } = productShortsStore;

    const changePage = ({ page }: { page: number }) => {
      pageNumber.value = page + 1;
    };

    const addProductToShopingCart = async (model: ProductShortModel) => {
      await addToCart({
        productId: model.id,
        quantity: 1,
      });
      // console.log({ toast });

      toast.add({
        severity: 'success',
        group: 'tr',
        detail: `${model.name} был успешно добавлен в корзину`,
        life: 3000,
      });
    };

    return {
      addNewProduct,
      modeFull,
      productFullStore,
      productShortsItems,
      productShortsStatus,
      productTitle,
      saveChanges,
      viewProduct,
      editProduct,
      pageNumber,
      pageSize,
      viewMode,
      currentPage,
      changePage,
      addToCart: addProductToShopingCart,
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
    padding: 0;
  }

  :deep(.p-card-header) {
    line-height: 0;
    text-align: center;
  }
}

.re-shape {
  :deep(.p-card-body) {
    padding: 0.5rem 1rem;
  }

  :deep(.p-card-content) {
    padding: 0.5rem 0;
  }

  :deep(.p-card-footer) {
    padding: 0.5rem 0;
  }
}

.custom-button-text {
  padding: 0;
}
</style>
