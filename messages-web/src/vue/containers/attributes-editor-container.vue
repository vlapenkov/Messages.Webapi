<template>
  <div class="flex flex-column gap-3">
    <template v-for="m in model" :key="m.attributeId">
      <div class="p-fluid grid">
        <div class="col-6 md:col-3">
          <div class="p-1">
            {{ getAttributeName(m.attributeId) }}
          </div>
        </div>
        <div class="col-5 md:col-6">{{ m.value }}</div>
        <div class="col-1">
          <prime-button
            @click="removeAttr(m.attributeId)"
            icon="pi pi-times"
            class="p-button-secondary p-button-outlined"
          ></prime-button>
        </div>
      </div>
    </template>

    <div class="p-fluid grid">
      <prime-input-dropdown
        class="col-6 md:col-3"
        :options="attributesFiltered"
        v-model="selectedAttribute"
        optionLabel="name"
        optionValue="id"
        label="Характеристика"
      ></prime-input-dropdown>
      <prime-input-text v-model="selectedValue" class="col-5 md:col-6" label="Значение">
      </prime-input-text>
      <div class="col-1">
        <prime-button
          :disabled="!isAttrValid"
          @click="addNewAttr"
          icon="pi pi-plus"
          class="p-button-secondary"
        ></prime-button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { IProductAttribute } from '@/app/product-full/@types/IProductAttribute';
import { useAttributes } from '@/composables/attributes.composable';
import { isNullOrEmpty } from '@/tools/string-tools';
import { computed, defineComponent, PropType, ref } from 'vue';

export default defineComponent({
  props: {
    modelValue: {
      type: Array as PropType<IProductAttribute[]>,
    },
  },
  emits: {
    'update:modelValue': (_: IProductAttribute[] | undefined) => true,
  },
  setup(props, { emit }) {
    const attributes = useAttributes();
    const model = computed({
      get: () => props.modelValue,
      set: (val) => {
        emit('update:modelValue', val);
      },
    });
    const attributesFiltered = computed(() =>
      model.value == null || model.value.length === 0
        ? attributes.value
        : // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
          attributes.value?.filter((at) => model.value!.every((m) => m.attributeId !== at.id)),
    );
    const getAttributeName = (id: number) =>
      attributes.value?.find((i) => i.id === id)?.name ?? 'Неизвестный атрибут';
    const selectedAttribute = ref<number>();
    const selectedValue = ref<string>();
    const isAttrValid = computed(() => !isNullOrEmpty(selectedValue.value));
    const addNewAttr = () => {
      if (selectedAttribute.value == null || selectedValue.value == null) {
        return;
      }
      model.value = [
        ...(model.value ?? []),
        {
          attributeId: selectedAttribute.value,
          value: selectedValue.value,
        },
      ];
      selectedAttribute.value = undefined;
      selectedValue.value = undefined;
    };
    const removeAttr = (id: number) => {
      if (model.value == null) {
        return;
      }
      model.value = model.value.filter((x) => x.attributeId !== id);
    };
    return {
      attributesFiltered,
      model,
      getAttributeName,
      selectedAttribute,
      selectedValue,
      isAttrValid,
      addNewAttr,
      removeAttr,
    };
  },
});
</script>

<style scoped></style>
