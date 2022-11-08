<template>
  <slot></slot>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { IPageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/@types/IPageableCollectionStore';
import { defineComponent, inject, PropType, provide, ShallowRef, shallowRef, watch } from 'vue';

export type SomePageableCollectionState<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> = IPageableCollectionStore<TIModel, TModel>;

const stateKey = Symbol('--collection-state-provided');

const provideState = (state: ShallowRef<SomePageableCollectionState<IModel, ModelBase> | null>) =>
  provide(stateKey, state);

export const injectPageableCollectionState = () =>
  inject<ShallowRef<SomePageableCollectionState<IModel, ModelBase> | null>>(
    stateKey,
    shallowRef(null),
  );

export default defineComponent({
  props: {
    state: {
      type: Object as PropType<SomePageableCollectionState<IModel, ModelBase>>,
      required: true,
    },
  },
  setup(props) {
    const stateProvided = shallowRef<SomePageableCollectionState<IModel, ModelBase> | null>(null);
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
