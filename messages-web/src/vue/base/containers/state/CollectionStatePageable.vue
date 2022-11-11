<template>
  <slot></slot>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { IPageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/@types/IPageableCollectionStore';
import { Provider } from '@/app/core/tools/provider';
import { defineComponent, PropType, shallowRef, watch } from 'vue';

export type SomePageableCollectionState<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> = IPageableCollectionStore<TIModel, TModel>;

export const pageableStateProvider = new Provider(
  () => shallowRef<SomePageableCollectionState<IModel, ModelBase> | null>(null),
  '--collection-state-provided',
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
    pageableStateProvider.provideFrom(() => props.state);
    return {};
  },
});
</script>

<style scoped></style>
