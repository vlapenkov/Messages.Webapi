<template>
  <card :class="{ 'no-body': fieldsEmpty }">
    <template #title>
      <template v-if="data != null">
        {{ data.title.value }}
      </template>
      <skeleton v-else class="h-2rem w-full"></skeleton>
    </template>
    <template #content>
      <template v-if="fieldsEmpty">
        <skeleton v-for="i in 5" :key="i" class="h-2rem w-full"></skeleton>
      </template>
      <template v-else>
        <div class="grid">
          <template v-for="field in visibleFields" :key="field.label">
            <div class="card-field col-12 md:col-6">
              <div class="name">{{ field.label }}</div>

              <div class="description">
                <custom-render
                  v-if="field.render(mode) != null"
                  :func="field.render(mode)"
                ></custom-render>
                <template v-else>
                  {{ field.value }}
                </template>
              </div>
            </div>
          </template>
        </div>
      </template>
    </template>
    <template #footer>
      <slot name="footer">
        <div v-if="canEdit" class="flex flex-row justify-content-end">
          <div>
            <prime-button-edit @click="select" label="Редактировать"></prime-button-edit>
          </div>
        </div>
      </slot>
    </template>
  </card>
</template>

<script lang="ts">
import { ModelBase } from '@/app/core/models/base/model-base';
import { ViewMode } from '@/app/core/models/decorators/@types/ViewMode';
import { computed, defineComponent, PropType } from 'vue';
import { selectItemProvider } from '../../containers/state/providers/select-item.provider';
import { showDialogProvider } from '../../containers/state/providers/show-dialog.provider';

export default defineComponent({
  props: {
    data: {
      type: Object as PropType<ModelBase>,
    },
    mode: {
      type: String as PropType<ViewMode>,
      default: 'view',
    },
  },
  setup(props) {
    const selectItem = selectItemProvider.inject();
    const showDialog = showDialogProvider.inject();
    const visibleFields = computed(() =>
      props.data?.fields?.filter((p) => p.hide !== 'always' && p.hide !== props.mode),
    );
    const fieldsEmpty = computed(
      () => visibleFields.value == null || visibleFields.value.length === 0,
    );

    const canEdit = computed(() => selectItem.value != null);

    const select = () => {
      if (selectItem.value == null || props.data == null) {
        throw new Error('Элемент нельзя редактировать');
      }

      selectItem.value(props.data.key);
      showDialog.value = true;
    };
    return { visibleFields, fieldsEmpty, canEdit, select };
  },
});
</script>

<style lang="scss" scoped>
.no-body {
  :deep(.p-card-content) {
    display: none;
  }
}

.card-field {
  .name {
    font-size: 1rem;
    font-weight: 700;
  }
  .description {
    margin: 0.5rem 0 1rem 0;
  }
}
</style>
