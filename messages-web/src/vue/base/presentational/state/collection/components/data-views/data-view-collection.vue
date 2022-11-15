<template>
  <loading-status-handler>
    <data-view
      class="border-round"
      :class="{
        '-m-1': viewLayout === 'grid',
        'pt-1': viewLayout === 'list',
        'no-background': !defaultBackground,
      }"
      :layout="viewLayout"
      :value="items"
    >
      <template #list="{ data, index }">
        <slot name="list" :data="data" :index="index">
          <div class="col-12">
            <data-card
              class="shadow-none border-noround"
              :class="{
                'border-round-top': index === 0,
                'border-round-bottom': index === (items?.length ?? 0) - 1,
              }"
              :data="data"
            >
            </data-card>
          </div>
        </slot>
      </template>
      <template #grid="{ data, index }">
        <slot name="grid" :data="data" :index="index">
          <div class="col-12 md:col-6 xl:col-4 p-1">
            <data-card class="h-full" :data="data"></data-card>
          </div>
        </slot>
      </template>
    </data-view>
  </loading-status-handler>
</template>

<script lang="ts">
import { screenLarge } from '@/app/core/services/window/window.service';
import { defineComponent, computed } from 'vue';
import { useItems } from '../../composables/items.composable';

export default defineComponent({
  props: {
    defaultBackground: {
      type: Boolean,
      deafault: false,
    },
  },
  setup() {
    const items = useItems();

    const viewLayout = computed(() => (screenLarge.value ? 'grid' : 'list'));

    return {
      items,
      viewLayout,
    };
  },
});
</script>

<style lang="scss" scoped>
.no-background {
  :deep(.p-dataview-content) {
    background-color: var(--surface-ground);
  }
}
</style>
