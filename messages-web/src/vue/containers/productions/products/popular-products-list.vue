<template>
  <toast position="top-right" group="tr" />
  <div class="w-full grid mr-0">
    <template v-if="productions == null">
      <div v-for="i in 16" :key="i" class="col-3">
        <skeleton height="250px" />
      </div>
    </template>
    <div v-else v-for="item in productions" :key="item.id" class="col-3">
      <production-list-item :production="item" @notify="notifyHandler" />
    </div>
  </div>
</template>

<script lang="ts">
import { productionsStore } from '@/app/productions/state/productions.store';
import { useToastNotificationHandler } from '@/composables/toast-notification-handler.composable';
import Toast from 'primevue/toast';
import { useToast } from 'primevue/usetoast';
import { defineComponent } from 'vue';

export default defineComponent({
  components: { Toast },
  setup() {
    const toast = useToast();

    const notifyHandler = useToastNotificationHandler(toast);
    const { currentPageItems: productions } = productionsStore;
    return { productions, notifyHandler };
  },
});
</script>

<style lang="scss"></style>
