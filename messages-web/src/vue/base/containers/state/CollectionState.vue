<template>
  <slot></slot>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { ICollectionEditableStore } from '@/app/core/services/harlem/custom/collection/editable/collection-editable.store';
import { IReadonlyCollectionStore } from '@/app/core/services/harlem/custom/collection/readonly/collection-readonly.store';
import { defineComponent, inject, PropType, provide, shallowRef, ShallowRef, watch } from 'vue';

export type SomeState<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> = IReadonlyCollectionStore<IModel, TModel> & Partial<ICollectionEditableStore<IModel, TModel>>;

const stateKey = Symbol('--collection-state');

const provideState = (state: ShallowRef<SomeState<IModel, ModelBase> | null>) =>
  provide(stateKey, state);

export const injectCollectionState = () =>
  inject<ShallowRef<SomeState<IModel, ModelBase> | null>>(stateKey, shallowRef(null));

export default defineComponent({
  props: {
    state: {
      type: Object as PropType<SomeState<IModel, ModelBase>>,
      required: true,
    },
  },
  setup(props) {
    const stateProvided = shallowRef<SomeState<IModel, ModelBase> | null>(null);
    watch(
      () => props.state,
      (val) => {
        stateProvided.value = val;
      },
      { immediate: true },
    );
    provideState(stateProvided);
    return {};
  },
});
</script>

<style scoped></style>
