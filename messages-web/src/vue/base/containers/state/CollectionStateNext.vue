<template>
  <div></div>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { CollectionStore } from '@/app/core/services/harlem/custom-stores/collection/@types/CollectionStore';
import { PageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/@types/PageableCollectionStore';
import { defineComponent, PropType } from 'vue';
import { loadingStatusProvider } from './providers/loading-status.provider';

type AbstractCollectionState<TIModel extends IModel, TModel extends ModelBase<TIModel>> =
  | PageableCollectionStore<TIModel, TModel>
  | CollectionStore<TIModel, TModel>;

export default defineComponent({
  props: {
    state: {
      type: Object as PropType<AbstractCollectionState<IModel, ModelBase>>,
      required: true,
    },
  },
  setup(props) {
    loadingStatusProvider.provideFrom(() => props.state.status);

    return {};
  },
});
</script>

<style scoped></style>
