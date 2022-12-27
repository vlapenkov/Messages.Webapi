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
import { productionsStore } from '@/app/productions/state/productions.store';
import { useToastNotificationHandler } from '@/composables/toast-notification-handler.composable';
import Toast from 'primevue/toast';
import { useToast } from 'primevue/usetoast';
import { defineComponent, PropType, computed, watch } from 'vue';

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
    const { getPageState, loadPage } = productionsStore;

    watch(
      () => props.request,
      (r) => {
        loadPage(r);
      },
      {
        immediate: true,
      },
    );

    const pageState = computed(() => getPageState(props.request));

    const productions = computed(() => pageState.value.pageData.value?.rows ?? null);

    const notifyHandler = useToastNotificationHandler(toast);
    return { productions, notifyHandler };
  },
});
</script>

<style lang="scss"></style>
