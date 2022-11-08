<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <toolbar class="mb-2" v-if="showToolbar">
    <template #end>
      <div class="flex flex-row gap-5">
        <div>Вид</div>
        <div class="field-radiobutton m-0">
          <radio-button inputId="data-view" name="city" value="data-view" v-model="viewMode" />
          <label for="data-view">Спиcок</label>
        </div>
        <div class="field-radiobutton m-0">
          <radio-button inputId="tree-view" name="city" value="tree-view" v-model="viewMode" />
          <label for="tree-view">Дерево</label>
        </div>
      </div>
    </template>
  </toolbar>
  <transition-fade>
    <div v-if="viewMode === 'data-view'">
      <slot name="data-view"></slot>
    </div>
    <div v-else-if="viewMode === 'tree-view'">
      <slot name="tree-view"></slot>
    </div>
  </transition-fade>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { ICollectionStoreTree } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionStoreTree';
import { ICollectionStoreAdd } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionstoreAdd';
import { ICollectionStoreDelete } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionStoreDelete';
import { ICollectionStoreEdit } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionstoreEdit';
import { ICollectionStoreRead } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionStoreRead';
import { ICollectionStoreSave } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionStoreSave';
import { ICollectionStoreSelectedItem } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionStoreSelectedItem';
import {
  computed,
  defineComponent,
  inject,
  onMounted,
  PropType,
  provide,
  ref,
  shallowRef,
  ShallowRef,
  watch,
} from 'vue';
import TransitionFade from '@/vue/components/transitions/TransitionFade.vue';

export type SomeCollectionState<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> = ICollectionStoreRead<IModel, TModel> &
  Partial<ICollectionStoreAdd> &
  Partial<ICollectionStoreSelectedItem<IModel, TModel>> &
  Partial<ICollectionStoreEdit> &
  Partial<ICollectionStoreAdd> &
  Partial<ICollectionStoreSave> &
  Partial<ICollectionStoreDelete> &
  Partial<ICollectionStoreTree>;

const stateKey = Symbol('--collection-state-provided');

const provideState = (state: ShallowRef<SomeCollectionState<IModel, ModelBase> | null>) =>
  provide(stateKey, state);

export const injectCollectionState = () =>
  inject<ShallowRef<SomeCollectionState<IModel, ModelBase> | null>>(stateKey, shallowRef(null));

export default defineComponent({
  components: { TransitionFade },
  props: {
    state: {
      type: Object as PropType<SomeCollectionState<IModel, ModelBase>>,
      required: true,
    },
  },
  setup(props, { slots }) {
    const stateProvided = shallowRef<SomeCollectionState<IModel, ModelBase> | null>(null);
    watch(
      () => props.state,
      (val) => {
        stateProvided.value = val;
      },
      { immediate: true },
    );
    provideState(stateProvided);

    const hasDataView = computed(() => slots['data-view'] != null);

    const hasTreeView = computed(() => slots['tree-view'] != null);

    const viewMode = ref<'data-view' | 'tree-view'>();

    onMounted(() => {
      if (hasDataView.value) {
        viewMode.value = 'data-view';
      } else if (hasTreeView.value) {
        viewMode.value = 'tree-view';
      }
    });

    const showToolbar = computed(
      () => [hasDataView, hasTreeView].filter((c) => c.value).length > 1,
    );

    return { hasTreeView, hasDataView, showToolbar, viewMode };
  },
});
</script>

<style scoped></style>
