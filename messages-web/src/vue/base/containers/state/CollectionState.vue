<template>
  <loading-status-handler :status="loadingStatus">
    <data-view :value="items">
      <template #list="{ data }">
        <div class="col-12 md:col-6">
          <data-card class="shadow-none" :data="data"></data-card>
        </div>
      </template>
    </data-view>
  </loading-status-handler>
</template>

<script lang="ts">
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { IReadonlyCollectionStore } from '@/app/core/services/harlem/custom/collection/readonly/collection-readonly.store';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { computed, defineComponent, PropType } from 'vue';

export default defineComponent({
  props: {
    state: {
      type: Object as PropType<IReadonlyCollectionStore<IModel, ModelBase>>,
      required: true,
    },
  },
  setup(props) {
    const loadingStatus = computed<DataStatus | undefined>(() => props.state.loadingStatus.value);
    const items = props.state.itemsAsync();

    return { loadingStatus, items };
  },
});
</script>

<style scoped></style>
