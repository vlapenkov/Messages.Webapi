<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div class="flex flex-row justify-content-end gap-2">
    <div>
      <span v-if="false" class="p-float-label">
        <dropdown :options="sortOptions"></dropdown>
        <label></label>
      </span>
    </div>
    <select-button :options="viewOptions" optionLabel="value" dataKey="value" v-model="mode">
      <template #option="{ option }">
        <i :class="option.icon"></i>
      </template>
    </select-button>
  </div>
</template>

<script lang="ts">
import { viewModeProvider } from '@/vue/presentational/providers/view-mode.provider';
import { computed, defineComponent } from 'vue';

export default defineComponent({
  setup() {
    const viewOptions = [
      { value: 'grid', icon: 'pi pi-th-large' },
      { value: 'list', icon: 'pi pi-bars' },
    ] as const;

    const viewMode = viewModeProvider.inject();

    const mode = computed({
      get: () => viewOptions.find((o) => viewMode.value === o.value),
      set: (val) => {
        if (val != null) {
          viewMode.value = val.value;
        }
      },
    });
    const sortOptions = ['В алфавитном порядке', 'По цене по возрастанию', 'По цене по убыванию'];

    return {
      mode,
      viewOptions,
      sortOptions,
    };
  },
});
</script>

<style scoped></style>
