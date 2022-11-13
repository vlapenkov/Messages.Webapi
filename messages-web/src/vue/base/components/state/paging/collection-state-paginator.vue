<template>
  <transition enter-active-class="scalein animation-duration-200" appear>
    <prime-paginator
      v-if="totalItemsCount > pageSize"
      class="mt-1"
      :rows="pageSize"
      :first="pageSize * (pageNumber - 1)"
      :totalRecords="totalItemsCount"
      @page="changePage"
    ></prime-paginator>
  </transition>
</template>

<script lang="ts">
import { PrimePaginator } from '@/tools/prime-vue-components';
import { useChangePage } from '@/vue/base/containers/state/collection/composables/change-page.composable';
import {
  pageNumberProvider,
  pageSizeProvider,
  totalItemsCountProvider,
} from '@/vue/base/containers/state/collection/providers/pages.provider';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    const pageSize = pageSizeProvider.inject();
    const pageNumber = pageNumberProvider.inject();
    const totalItemsCount = totalItemsCountProvider.inject();
    const changePage = useChangePage();
    return { pageSize, pageNumber, totalItemsCount, changePage };
  },
  components: { PrimePaginator },
});
</script>

<style scoped></style>
