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
      if (props.value > 0 && splittedValue.length === 1) {
        splittedValue.push('0');
      }
      return splittedValue.join('.');
    });
    return { displayVal };
  },
});
</script>

<style lang="scss" scoped>
@import '@/assets/styles/_variables.scss';
.star-filled {
  &::before {
    content: url('@/assets/icons/star.svg');
  }
}

.star-yellow {
  color: map-get($map: $colors, $key: 'yellow');
}
</style>
