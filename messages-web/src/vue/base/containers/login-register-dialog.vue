<template>
  <prime-dialog
    v-model:visible="isShowDialog"
    :draggable="false"
    modal
    :breakpoints="{ '900px': '75vw', '720px': '90vw' }"
    :style="{ 'width': '50vw', 'max-width': '400px' }"
    class="re-padding"
    closable
  >
    <template #header>
      <div class="w-full"></div>
    </template>
    <div class="w-full flex flex-column">
      <img
        src="@/assets/images/logo.svg"
        alt=""
        width="70"
        height="70"
        class="col-6 col-offset-3 mb-4"
      />
      <prime-button
        label="Войти"
        class="col-6 col-offset-3 p-button-sm mb-2"
        @click="handleLogin"
      />
      <router-link :to="{ name: 'register' }" class="no-underline">
        <prime-button
          label="Зарегистрироваться"
          class="col-6 col-offset-3 p-button-sm p-button-text"
        />
      </router-link>
    </div>
  </prime-dialog>
</template>

<script lang="ts">
import { authService } from '@/app/core/services/auth/auth.service';
import { registerStore } from '@/store/register.store';
import { PrimeDialog } from '@/tools/prime-vue-components';
import { defineComponent } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  components: { PrimeDialog },
  setup() {
    const route = useRoute();
    const { isShowDialog } = registerStore;
    const handleLogin = async () => {
      await authService.loginRedirect(route);
    };
    return {
      isShowDialog,
      handleLogin,
    };
  },
});
</script>

<style lang="scss">
.re-padding {
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
</style>
