<template>
  <transition enter-active-class="scalein animation-duration-200" appear>
    <prime-paginator
      v-if="totalItemsCount > pageSize"
      class="mt-1 border-1 shadow-1"
      :rows="pageSize"
      :first="pageSize * (pageNumber - 1)"
      :totalRecords="totalItemsCount"
      @page="changePage"
    ></prime-paginator>
  </transition>
</template>

<script lang="ts">
import { PrimePaginator } from '@/tools/prime-vue-components';
import { usePages } from '@/vue/base/containers/state/collection/composables/pages.composable';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return { ...usePages() };
  },
  components: { PrimePaginator },
});
</script>

<style scoped></style>
