<!-- eslint-disable vue/require-v-for-key -->
<template>
  <template v-if="attributesExists">
    <div
      class="flex card-container blue-container overflow-hidden mt-1"
      v-for="attr in attributes"
      :key="attr.attributeId"
    >
      <div class="flex-none flex align-items-center justify-content-center">
        {{ getAttributeName(attr.attributeId) }}
      </div>
      <div class="flex-grow-1 flex align-items-center justify-content-center dotted-border"></div>
      <div class="flex-none flex align-items-center justify-content-center">
        {{ attr.value }}
      </div>
    </div>
  </template>
  <app-text v-else> Характеристики отсутствуют </app-text>
</template>

<script lang="ts">
import { IProductAttribute } from '@/app/product-full/@types/IProductAttribute';
import { IProductFullModel } from '@/app/product-full/@types/IProductFullModel';
import { useAttributes } from '@/composables/attributes.composable';
import { computed, defineComponent, PropType, ref, watch } from 'vue';

export default defineComponent({
  props: {
    product: {
      type: Object as PropType<IProductFullModel>,
      default: () => null,
    },
  },
  setup(props) {
    const attributeDefs = useAttributes();

    const attributes = ref<IProductAttribute[]>();
    watch(
      () => props.product,
      (propduct) => {
        if (
          propduct == null ||
          propduct.attributeValues == null ||
          propduct.attributeValues.length <= 0
        ) {
          attributes.value = [] as IProductAttribute[];
        }
        attributes.value = propduct.attributeValues;
      },
      {
        immediate: true,
      },
    );

    const getAttributeName = (id: number) => {
      if (attributeDefs.value != null) {
        const def = attributeDefs.value.find((c) => c.id === id);
        return def != null ? def.name : '-';
      }
      return '-';
    };

    const attributesExists = computed(() => (attributes.value?.length ?? 0) > 0);

    return { attributes, getAttributeName, attributesExists };
  },
});
</script>

<style lang="scss" scoped>
.dotted-border {
  // border-bottom: 1px dotted;
  background-image: linear-gradient(to right, #333 10%, rgba(255, 255, 255, 0) 0%);
  background-position: bottom;
  background-size: 8px 1px;
  background-repeat: repeat-x;
}
</style>
