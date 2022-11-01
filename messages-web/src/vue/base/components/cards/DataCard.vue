<template>
  <card>
    <template #title>
      <template v-if="data != null">
        {{ data.title.value }}
      </template>
      <skeleton class="h-2rem w-full"></skeleton>
    </template>
    <template #content>
      <template v-if="visibleFields != null">
        <card v-for="field in visibleFields" :key="field.label" class="shadow-none">
          <template #title>
            {{ field.label }}
          </template>
          <template #content>
            <slot :name="'display-' + field.key" :field="field">
              {{ field.value }}
            </slot>
          </template>
        </card>
      </template>
      <progress-spinner v-else></progress-spinner>
    </template>
  </card>
</template>

<script lang="ts">
import { ModelBase } from '@/app/core/models/base/model-base';
import { computed, defineComponent, PropType } from 'vue';

export default defineComponent({
  props: {
    data: {
      type: Object as PropType<ModelBase>,
    },
  },
  setup(props) {
    const visibleFields = computed(() => props.data?.fields?.filter((p) => p.visible));
    return { visibleFields };
  },
});
</script>

<style scoped></style>
