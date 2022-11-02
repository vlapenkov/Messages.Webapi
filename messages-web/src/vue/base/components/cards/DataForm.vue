<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <card>
    <template #title>
      <template v-if="data">
        {{ data.title.value }}
      </template>
      <skeleton class="h-2rem w-full"></skeleton>
    </template>
    <template #content>
      <div class="p-fluid grid">
        <div v-for="field in visibleFields" :key="field.label" class="field col-12">
          <span class="p-float-label">
            <custom-render
              v-if="field.render(mode) != null"
              :func="field.render(mode)"
            ></custom-render>
            <input-text
              v-model="field.value"
              v-else-if="field.control === 'text'"
              type="text"
              :id="field.key"
            >
            </input-text>
            <input-number v-else :id="field.key" v-model="field.value"></input-number>
            <label :for="field.key">{{ field.label }}</label>
          </span>
        </div>
      </div>
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
    mode: {
      type: String,
      default: 'edit',
    },
  },
  setup(props) {
    const visibleFields = computed(() => (props.data?.fields ?? []).filter((p) => p.visible));

    return { visibleFields };
  },
});
</script>

<style scoped></style>
