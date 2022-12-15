<template>
  <transition-fade>
    <template v-if="!requiresAuth">
      <slot></slot>
    </template>
    <div v-else>Не авторизован</div>
  </transition-fade>
</template>

<script lang="ts">
import { isAuthenticated } from '@/store/user.store';
import { computed, defineComponent } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  setup() {
    const route = useRoute();
    const requiresAuth = computed(() => route.meta.requiresAuth);
    return {
      requiresAuth,
      isAuthenticated,
    };
  },
});
</script>

<style scoped></style>
