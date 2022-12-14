<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <prime-dialog
    header="Добавление категории"
    :breakpoints="{ '900px': '75vw', '720px': '90vw' }"
    :style="{ 'width': '50vw', 'max-width': '800px' }"
    class="re-padding"
    :draggable="false"
    modal
    v-model:visible="showDialog"
  >
    <div class="p-fluid grid pt-4">
      <div class="field col-12">
        <span class="p-float-label">
          <input-text id="section-name" v-model="selectedName"></input-text>
          <label for="section-name">Название</label>
        </span>
      </div>
      <div class="field col-12">
        <span class="p-float-label">
          <parent-section-selector id="parent-section-id"></parent-section-selector>
          <label for="parent-section-id">Родительская категория</label>
        </span>
      </div>
    </div>
    <prime-divider></prime-divider>
    <div class="w-full flex flex-row justify-content-end">
      <prime-button-add
        @click="saveChanges"
        v-if="mode === 'create'"
        label="Создать категорию"
      ></prime-button-add>
    </div>
  </prime-dialog>
</template>

<script lang="ts">
import { PrimeDialog } from '@/tools/prime-vue-components';
import { computed, defineComponent } from 'vue';
import PrimeDivider from '@/vue/base/presentational/prime/prime-divider.vue';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { Creation } from '@/app/core/services/harlem/tools/not-valid-data';
import ParentSectionSelector from '../parent-section-selector.vue';

export default defineComponent({
  props: {
    show: {
      type: Boolean,
      default: false,
    },
  },
  emits: {
    'update:show': (arg: boolean) => typeof arg === 'boolean',
  },
  setup(props, { emit }) {
    const { sectionSelected, saveChanges } = sectionsStore;
    const mode = computed(() => sectionSelected.value?.mode);
    const selectedData = computed({
      get: () => sectionSelected.value?.data ?? null,
      set: (dataVal) => {
        if (sectionSelected.value != null && dataVal != null) {
          sectionSelected.value = new Creation(dataVal);
        }
      },
    });
    const showDialog = computed({
      get: () => props.show,
      set: (val) => {
        emit('update:show', val);
        setTimeout(() => {
          sectionSelected.value = null;
        }, 500);
      },
    });

    const selectedName = computed({
      get: () => selectedData.value?.name,
      set: (val) => {
        if (selectedData.value == null || val == null) {
          return;
        }
        const sval = selectedData.value.clone();
        sval.name = val;
        selectedData.value = sval;
      },
    });

    return { mode, saveChanges, showDialog, selectedName };
  },
  components: { PrimeDialog, ParentSectionSelector, PrimeDivider },
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
