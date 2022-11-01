<template>
  <div>
    <collection-state :state="sectionsStore"></collection-state>
  </div>
</template>

<script lang="ts">
import { sectionsStore } from '@/app/sections/state/sections.store';
import { defineComponent } from 'vue';
import CollectionState from '../base/containers/state/CollectionState.vue';

export default defineComponent({
  setup() {
    return {
      sectionsStore,
    };
  },
  components: { CollectionState },
});
</script>

<style scoped></style>
