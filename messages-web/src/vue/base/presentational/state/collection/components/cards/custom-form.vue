<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <card>
    <template #content>
      <div class="p-fluid grid">
        <div v-for="field in visibleFields" :key="field.label" class="field col-12">
          <span v-if="!field.noLabel" class="p-float-label">
            <custom-render
              v-if="field.render(mode) != null"
              :func="field.render(mode)"
            ></custom-render>
            <input-text
              v-model="field.model.value"
              v-else-if="field.control === 'text'"
              type="text"
              :id="field.key"
            >
            </input-text>
            <input-number v-else :id="field.key" v-model="field.model.value"></input-number>
            <label :for="field.key">{{ field.label }}</label>
          </span>
          <custom-render
            v-else-if="field.render(mode) != null"
            :func="field.render(mode)"
          ></custom-render>
        </div>
      </div>
    </template>
    <template #footer>
      <slot name="footer"></slot>
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
          const newData = props.data.clone();
          newData.setKey(key, val);
          emit('update:data', newData);
        },
      });
    const visibleFields = computed(() =>
      props.data == null || props.data.fields == null
        ? null
        : [
            props.data.title,
            ...(props.data.fields ?? []).filter(
              (p) => p.hide !== 'always' && p.hide !== props.mode,
            ),
          ]
            .filter((p) => p != null && p.key != null)
            .map((i) => ({ ...i, model: getProp(i?.key) })),
    );

    // watchEffect(() => {
    //   console.log('fields are', toRaw(props.data), toRaw(props.data?.fields));
    //   console.log('check', visibleFields.value, props.data?.title);
    // });

    return { visibleFields };
  },
});
</script>

<style scoped></style>
