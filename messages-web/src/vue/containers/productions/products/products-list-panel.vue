<template>
  <toast position="top-right" group="tr" />
  <div class="w-full grid mr-0">
    <template v-if="productions == null || productions.length === 0">
      <div v-for="i in request.pageSize" :key="i" class="col-3">
        <skeleton :animation="productions == null ? 'wave' : 'none'" height="350px" />
      </div>
    </template>
    <div v-else v-for="item in productions" :key="item.id" class="col-12 md:col-6 lg:col-3">
      <production-list-item :production="item" @notify="notifyHandler" />
    </div>
  </div>
</template>

<script lang="ts">
import { IproductionsPageRequest } from '@/app/productions/@types/IproductionsPageRequest';
import { productionsService } from '@/app/productions/services/productions.service';
import { productionsStore } from '@/app/productions/state/productions.store';
import { useToastNotificationHandler } from '@/composables/toast-notification-handler.composable';
import Toast from 'primevue/toast';
import { useToast } from 'primevue/usetoast';
import { defineComponent, PropType, watch } from 'vue';

export default defineComponent({
  components: { Toast },
  props: {
    request: {
      type: Object as PropType<IproductionsPageRequest>,
      required: true,
    },
  },
  setup(props) {
    const toast = useToast();

    watch(
      () => props.request,
      (request) => {
        productionsService.loadPage(request);
        productionsStore.pageNumber.value = request.pageNumber;
        productionsStore.pageSize.value = request.pageSize;
      },
      {
        immediate: true,
        deep: true,
      },
    );

    const notifyHandler = useToastNotificationHandler(toast);
    const { currentPageItems: productions } = productionsStore;
    return { productions, notifyHandler };
  },
});
</script>

<style lang="scss"></style>
