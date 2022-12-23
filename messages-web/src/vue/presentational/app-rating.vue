<template>
  <div
    class="flex flex-row gap-1 align-content-center text-md"
    :class="{ 'opacity-0': value === 0 }"
  >
    <i class="star-filled star-yellow"></i>
    <span>
      {{ displayVal }}
    </span>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent } from 'vue';

export default defineComponent({
  props: {
    value: {
      type: Number,
      default: 0,
    },
  },
  setup(props) {
    const displayVal = computed(() => {
      const splittedValue = String(props.value).split('.');
      if (splittedValue.length === 1) {
        splittedValue.push('0');
      }
      return splittedValue.join('.');
    });
    return { displayVal };
  },
});
</script>

<style scoped></style>
