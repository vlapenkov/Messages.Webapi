<template>
  <div class="flex flex-column justify-content-between h-full">
    <toast position="top-right" group="tr" />
    <transition-fade>
      <template v-if="productShortsItems != null">
        <div v-if="productShortsItems.length > 0" class="grid">
          <div class="col-12">
            <production-toolbar-container></production-toolbar-container>
          </div>
          <div
            v-for="item in productShortsItems"
            :key="item.id"
            :class="{
              'col-3': viewMode === 'grid' && !showFilters,
              'col-4': viewMode === 'grid' && showFilters,
              'col-12': viewMode === 'list',
            }"
          >
            <production-list-item :production="item" @notify="notifyHandler">
            </production-list-item>
          </div>
        </div>
        <div
          v-else
          style="background-color: var(--surface-card)"
          class="w-full h-full flex justify-content-center border-round align-items-center"
        >
          <div class="text-center">
            <i class="pi pi-inbox text-8xl opacity-50"></i>
            <div class="p-component text-lg mt-3">Товаров не найдено</div>
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
      class="mt-2 border-1 shadow-1 products-paginator"
      v-if="pageNumber && pageSize && (currentPage?.totalItemCount ?? 0) > 0"
      @page="changePage"
      :rows="pageSize"
      :first="pageSize * (pageNumber - 1)"
      :totalRecords="currentPage?.totalItemCount ?? 0"
    ></prime-paginator>
  </div>
</template>

<script lang="ts">
import { productFullStore } from '@/app/product-full/state/product-full.store';

import { computed, defineComponent } from 'vue';
import { PrimePaginator } from '@/tools/prime-vue-components';
import { useToast } from 'primevue/usetoast';
import Toast from 'primevue/toast';
import { productionsStore } from '@/app/productions/state/productions.store';
import { useToastNotificationHandler } from '@/composables/toast-notification-handler.composable';
import { viewModeProvider } from '../presentational/providers/view-mode.provider';

export default defineComponent({
  components: { PrimePaginator, Toast },
  setup() {
    const toast = useToast();

    const productShortsItems = computed(() => productionsStore.currentPageItems.value);
    const { status: productShortsStatus, pageNumber, pageSize, currentPage } = productionsStore;

    const changePage = ({ page }: { page: number }) => {
      pageNumber.value = page + 1;
    };

    const { showFilters } = productionsStore;
    const notifyHandler = useToastNotificationHandler(toast);
    const viewMode = viewModeProvider.provide();
    return {
      notifyHandler,
      productFullStore,
      productShortsItems,
      productShortsStatus,
      pageNumber,
      pageSize,
      currentPage,
      changePage,
      showFilters,
      viewMode,
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

/**
TODO
Разобраться, почему на dev зоне цвет рамки - инверсия выбранной темы
*/
.products-paginator {
  border: solid #e9ecef !important;
  border-width: 0 !important;
  border-radius: 6px !important;
}
</style>
