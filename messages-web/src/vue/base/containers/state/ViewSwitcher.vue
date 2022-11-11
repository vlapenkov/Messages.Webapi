<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div v-if="show" class="flex flex-row gap-5">
    <div>Вид</div>
    <div v-for="mode in modes" :key="mode.mode" class="field-radiobutton m-0">
      <radio-button :inputId="mode.mode" name="mode" :value="mode.mode" v-model="viewMode" />
      <label :for="mode.mode">{{ mode.label }}</label>
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, PropType } from 'vue';
import type { IViewMode } from './@types/viewMode';
import type { DisplayMode } from './@types/viewTypes';

export const viewSwitcherProps = {
  modes: {
    type: Array as PropType<IViewMode[]>,
    default: (): IViewMode[] => [
      {
        mode: 'data-view',
        label: 'Сеткой',
      },
    ],
  },
  modelValue: {
    type: String as PropType<DisplayMode>,
  },
};

export default defineComponent({
  props: viewSwitcherProps,
  emits: {
    'update:modelValue': (_: DisplayMode) => true,
  },
  setup(props, { emit }) {
    const show = computed(() => props.modes.length > 1);
    const viewMode = computed({
      get: () => props.modelValue,
      set: (val) => {
        if (val == null) {
          return;
        }
        emit('update:modelValue', val);
      },
    });
    return { viewMode, show };
  },
});
</script>

<style scoped></style>
