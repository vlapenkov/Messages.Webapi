<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <card>
    <template #title>
      <template v-if="data"> Создание нового элемента </template>
      <skeleton v-else class="h-2rem w-full"></skeleton>
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
            <input-number v-else :id="field.key" v-model="field.model"></input-number>
            <label :for="field.key">{{ field.label }}</label>
          </span>
        </div>
      </div>
    </template>
  </card>
</template>

<script lang="ts">
import { ModelBase } from '@/app/core/models/base/model-base';
import { computed, defineComponent, PropType, watchEffect } from 'vue';

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
  emits: {
    'update:data': (_: ModelBase) => true,
  },
  setup(props, { emit }) {
    const getProp = (key: string) =>
      computed({
        get: () => (props.data ? props.data[key as keyof ModelBase] : null),
        set: (val) => {
          if (props.data == null) {
            return;
          }
          emit('update:data', { ...props.data, [key]: val } as unknown as ModelBase);
        },
      });
    const visibleFields = computed(() =>
      props.data == null
        ? null
        : [
            props.data.title,
            ...(props.data.fields ?? []).filter(
              (p) => p.hide !== 'always' && p.hide !== props.mode,
            ),
          ].map((i) => {
            console.log('fields are', props.data?.fields);

            console.log('iitem', { ...i });
            return { ...i, model: getProp(i.key) };
          }),
    );

    watchEffect(() => {
      console.log({ visibleFields });
    });

    return { visibleFields };
  },
});
</script>

<style scoped></style>
