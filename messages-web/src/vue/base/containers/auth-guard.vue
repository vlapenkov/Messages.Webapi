<template>
  <transition-fade>
    <div>
      <teleport to="body">
        <login-register-dialog />
      </teleport>
      <template v-if="canView">
        <slot></slot>
      </template>
      <div v-else>
        <div class="w-full flex flex-row justify-content-center mt-6">
          <span class="p-component text-2xl">Войдите или зарегистрируйтесь</span>
        </div>
      </div>
    </div>
  </transition-fade>
</template>

<script lang="ts">
import { showRegisterDialog } from '@/store/register.store';
import { isAuthenticated } from '@/store/user.store';
import { computed, defineComponent, watch } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  setup() {
    const route = useRoute();
    const requiresAuth = computed(() => route.meta.requiresAuth);
    const canView = computed(() => {
      if (!requiresAuth.value) return true;
      return isAuthenticated.value;
    });
    watch(
      canView,
      (r) => {
        showRegisterDialog.value = !r;
      },
      {
        immediate: true,
      },
    );
    return {
      canView,
      isAuthenticated,
    };
  },
});
</script>

<style scoped></style>
