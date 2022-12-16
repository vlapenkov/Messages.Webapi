<template>
  <transition-fade>
    <template v-if="canView">
      <slot></slot>
    </template>
    <div v-else>
      <div class="w-full flex flex-row justify-content-center mt-6">
        <span class="p-component text-2xl">Войдите или зарегистрируйтесь</span>
      </div>
      <teleport to="body">
        <login-register-dialog :visible="visibleModel" @update:visible="updatevisibleModel" />
      </teleport>
    </div>
  </transition-fade>
</template>

<script lang="ts">
import { isAuthenticated } from '@/store/user.store';
import { computed, defineComponent, ref, watch } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  setup() {
    const route = useRoute();
    const requiresAuth = computed(() => route.meta.requiresAuth);
    const canView = computed(() => {
      if (!requiresAuth.value) return true;
      return isAuthenticated.value;
    });
    const visibleModel = ref(!canView.value);
    watch(canView, (r) => {
      visibleModel.value = !r;
    });
    const updatevisibleModel = (v: boolean) => {
      visibleModel.value = v;
    };
    return {
      canView,
      isAuthenticated,
      visibleModel,
      updatevisibleModel,
    };
  },
});
</script>

<style scoped></style>
