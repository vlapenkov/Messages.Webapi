<template>
  <loading-status-handler :status="loadingStatus">
    <data-view
      paginator
      :rows="pageSize"
      :first="pageSize * (pageNumber - 1)"
      :totalRecords="totalItemsCount"
      class="border-round"
      :layout="viewLayout"
      :value="items"
      @page="changePage"
    >
      <template #list="{ data }">
        <div class="col-12">
          <data-card class="shadow-none" :data="data"> </data-card>
        </div>
      </template>
      <template #grid="{ data }">
        <div class="col-12 md:col-6 lg:col-4 p-2">
          <data-card class="border-1 h-full" :data="data"></data-card>
        </div>
      </template>
      <template #header>
        <div v-if="canAdd" class="flex justify-content-end">
          <prime-button-add @click="create" label="Добавить"></prime-button-add>
        </div>
      </template>
    </data-view>
  </loading-status-handler>
  <prime-dialog header="Создание нового элемента" v-model:visible="showDialog">
    <!-- <custom-form class="shadow-none" v-model:data="selectedData">
      <template #footer>
        <div v-if="(isEditable || canAdd) && mode != null" class="flex justify-content-end">
          <prime-button-add
            @click="saveChanges"
            v-if="mode === 'create'"
            label="Добавить"
          ></prime-button-add>
          <prime-button-save @click="saveChanges" v-else label="Сохранить"></prime-button-save>
        </div>
      </template>
    </custom-form> -->
  </prime-dialog>
</template>

<script lang="ts">
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { computed, defineComponent, ref, toRaw, watchEffect } from 'vue';
import Dialog from 'primevue/dialog';
import { screenLarge } from '@/app/core/services/window/window.service';
import { injectPageableCollectionState } from './PageableCollectionState.vue';

export default defineComponent({
  components: { PrimeDialog: Dialog },
  props: {
    reloadOnSave: {
      type: Boolean,
      default: false,
    },
  },
  setup() {
    const currentState = injectPageableCollectionState();

    const loadingStatus = computed<DataStatus | undefined>(() => currentState.value?.status.value);

    const pageSize = computed(() => currentState.value?.pageSize.value ?? 0);

    const pageNumber = computed(() => currentState.value?.pageNumber.value ?? 0);

    const totalItemsCount = computed(
      () => currentState.value?.currentPage.value?.totalItemCount ?? 0,
    );

    const prevItemsSize = computed(() => {
      console.log(pageNumber.value, pageSize.value, pageNumber.value > 0 && pageSize.value > 0);

      return pageNumber.value > 0 && pageSize.value > 0
        ? (pageNumber.value - 1) * pageSize.value
        : 0;
    });

    const items = computed(() => [
      ...[...new Array(prevItemsSize.value)].map(() => null),
      ...(currentState.value?.currentPage.value?.rows ?? []),
    ]);

    watchEffect(() => {
      console.log('page', toRaw(currentState.value?.currentPage.value));

      console.log('prevItemsSize', prevItemsSize.value);
      console.log('items', toRaw(items.value));
    });

    const isEditable = computed(() => false);
    const canAdd = computed(() => false);

    const showDialog = ref(false);

    const create = () => {
      // if (currentState.value == null) {
      //   return;
      // }
      // const { createItem, itemSelected } = currentState.value;
      // if (createItem == null || itemSelected === undefined) {
      //   throw new Error('Canot edit uneditable state!');
      // }

      // createItem();
      // showDialog.value = true;
      throw new Error('Not Implemented!');
    };

    // const mode = computed(() => currentState.value?.itemSelected?.value?.mode);

    // const selectedData = computed({
    //   get: () => currentState.value?.itemSelected?.value?.data,
    //   set: (val) => {
    //     if (currentState.value?.itemSelected?.value == null || val == null) {
    //       return;
    //     }
    //     currentState.value.itemSelected.value = new NotValidData(
    //       val,
    //       currentState.value.itemSelected.value.mode,
    //     );
    //   },
    // });

    // const saveChanges = () => {
    //   if (currentState.value != null && currentState.value.saveChanges != null) {
    //     currentState.value.saveChanges();
    //     showDialog.value = false;
    //     if (props.reloadOnSave) {
    //       currentState.value.getDataAsync({ force: true });
    //     }
    //   }
    // };

    const viewLayout = computed(() => (screenLarge.value ? 'grid' : 'list'));

    const changePage = ({ page }: { page: number }) => {
      console.log({ page });

      if (currentState.value == null || page == null) {
        return;
      }
      currentState.value.pageNumber.value = page + 1;
    };

    return {
      loadingStatus,
      items,
      isEditable,
      create,
      showDialog,
      // selectedData,
      // mode,
      // saveChanges,
      pageSize,
      pageNumber,
      totalItemsCount,
      changePage,
      viewLayout,
      canAdd,
    };
  },
});
</script>

<style scoped></style>
