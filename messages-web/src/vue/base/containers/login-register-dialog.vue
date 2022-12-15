<template>
  <prime-dialog
    v-model:visible="visibilityModel"
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
        width="50"
        height="50"
        class="col-6 col-offset-3 mb-6"
      />
      <prime-button label="Войти" class="col-6 col-offset-3 p-button-sm mb-3" @click="login" />
      <router-link to="register" class="no-underline">
        <prime-button
          label="Зарегистрироваться"
          class="col-6 col-offset-3 p-button-sm p-button-text"
          @click="register"
        />
      </router-link>
    </div>
  </prime-dialog>
</template>

<script lang="ts">
import { login } from '@/app/core/services/keycloak/keycloak.service';
import { PrimeDialog } from '@/tools/prime-vue-components';
import { computed, defineComponent } from 'vue';

export default defineComponent({
  components: { PrimeDialog },
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
  },
  emits: {
    'update:visible': (_: boolean) => true,
  },
  setup(props, { emit }) {
    const visibilityModel = computed({
      get: () => props.visible,
      set: (v) => emit('update:visible', v),
    });
    const register = () => {
      visibilityModel.value = false;
    };
    return {
      visibilityModel,
      register,
      login,
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
