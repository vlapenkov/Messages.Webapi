<template>
  <slot></slot>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { ICollectionStoreAdd } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionstoreAdd';
import { ICollectionStoreDelete } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionStoreDelete';
import { ICollectionStoreEdit } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionstoreEdit';
import { ICollectionStoreRead } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionStoreRead';
import { ICollectionStoreSave } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionStoreSave';
import { ICollectionStoreSelectedItem } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionStoreSelectedItem';
import { defineComponent, inject, PropType, provide, shallowRef, ShallowRef, watch } from 'vue';

export type SomeState<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> = ICollectionStoreRead<IModel, TModel> &
  Partial<ICollectionStoreAdd> &
  Partial<ICollectionStoreSelectedItem<IModel, TModel>> &
  Partial<ICollectionStoreEdit> &
  Partial<ICollectionStoreAdd> &
  Partial<ICollectionStoreSave> &
  Partial<ICollectionStoreDelete>;

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
