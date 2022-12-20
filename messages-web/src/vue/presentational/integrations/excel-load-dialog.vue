<!-- eslint-disable vuejs-accessibility/form-control-has-label -->
<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <prime-dialog v-model:visible="visibilityModel" :draggable="false" modal
    :breakpoints="{ '900px': '75vw', '720px': '90vw' }" :style="{ 'width': '50vw', 'max-width': '400px' }"
    class="re-padding" closable>
    <template #header>
      <div class="flex card-container blue-container overflow-hidden">
        <div class="flex-none flex align-items-center justify-content-center border-round">
          <img :src="require('@/assets/images/excel.svg')" alt="Excel" width="30" :style="{
            objectFit: 'cover',
            borderRadius: '0.5rem',
          }" class="mr-3" />
          <span class="text-lg font-semibold">Загрузка из Exel</span>
        </div>
      </div>
    </template>
    <div class="w-full flex flex-column">
      <span class="col-12 mx-1 word-break">{{ file?.name != null ? file.name : 'Файл не выбран'
      }}</span>
      <file-upload mode="basic" id="excel-file"
        accept=".csv, application/vnd.ms-excel, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        :maxFileSize="10000000" @input="onFileInput" :auto="true" :customUpload="true" chooseLabel="Выбрать файл"
        class="col-6 col-offset-3 p-button-sm mb-3" />
      <prime-button label="Загрузить" class="col-6 col-offset-3 p-button-sm mb-3" @click="close"
        :disabled="!uploadPossible" />
    </div>
  </prime-dialog>
</template>

<script lang="ts">
import { PrimeDialog } from '@/tools/prime-vue-components';
import { useBase64 } from '@vueuse/core';
import { computed, defineComponent, ref, watch } from 'vue';

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
    'fileSelected': (_: { name: string; b64: string; }) => true
  },
  setup(props, { emit }) {

    const visibilityModel = computed({
      get: () => props.visible,
      set: (v) => emit('update:visible', v),
    });

    const file = ref();
    const { base64: fileB64 } = useBase64(file);

    const uploadPossible = ref<boolean>(false);

    watch(file, (val) => {
      if (val != null) {
        uploadPossible.value = true;
      }
    })

    const onFileInput = (e: Event) => {
      const target = e.target as HTMLInputElement;
      const { files } = target;
      if (files == null) {
        return;
      }
      [file.value] = files;
    };

    watch(visibilityModel, (val) => {
      if (!val) file.value = null;
    })

    const close = () => {
      if (file?.value?.name != null && fileB64?.value != null) {
        emit('fileSelected', { name: file.value.name, b64: fileB64.value })
        visibilityModel.value = false;
      }
    };

    return {
      visibilityModel, file, uploadPossible, close, onFileInput
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

  .word-break {
    word-break: break-all;
  }
}
</style>
