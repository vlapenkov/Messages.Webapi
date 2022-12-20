<!-- eslint-disable vuejs-accessibility/form-control-has-label -->
<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <prime-dialog v-model:visible="visibilityModel" modal :draggable="false"
    :breakpoints="{ '900px': '75vw', '720px': '90vw' }" :style="{ 'width': '50vw', 'max-width': '150px' }"
    :closable="false">
    <template #header>
      <div class="flex card-container blue-container overflow-hidden">
        <div class="flex-none flex align-items-center justify-content-center border-round">
          <span class="text-sm font-semibold">Обработка...</span>
        </div>
      </div>
    </template>
    <progress-spinner style="width:100px; height:100px" aria-label="Загрузка" />
  </prime-dialog>
</template>

<script lang="ts">
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

    return { visibilityModel };
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

  .word-break {
    word-break: break-all;
  }
}
</style>
