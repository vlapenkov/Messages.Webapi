<template>
  <card class="re-padding" :class="{ 'no-body': fieldsEmpty, 'no-footer': !canEditOrDelete }">
    <template #title>
      <template v-if="data != null && !data.title.noLabel">
        {{ data.title.value }}
      </template>
      <skeleton v-else-if="!data?.title.noLabel" class="h-2rem w-full"></skeleton>
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
        <div v-if="canEditOrDelete" class="flex flex-row justify-content-end">
          <edit-item-button />
          <delete-item-button />
        </div>
      </slot>
    </template>
  </card>
</template>

<script lang="ts">
import { ModelBase } from '@/app/core/models/base/model-base';
import { ViewMode } from '@/app/core/models/decorators/@types/ViewMode';
import { defineComponent, PropType, computed } from 'vue';
import { useEditableChecks } from '../../composables/editable-checks.composable';
import { modelProvider } from '../../providers/model-provider';

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
    modelProvider.provideFrom(() => props.data);
    const visibleFields = computed(() =>
      props.data?.fields?.filter((p) => p.hide !== 'always' && p.hide !== props.mode),
    );
    const fieldsEmpty = computed(
      () => visibleFields.value == null || visibleFields.value.length === 0,
    );

    const { canEdit, canDelete } = useEditableChecks();

    const canEditOrDelete = computed(() => canEdit.value || canDelete.value);

    return { visibleFields, fieldsEmpty, canEditOrDelete };
  },
});
</script>

<style lang="scss" scoped>
.no-body {
  :deep(.p-card-content) {
    display: none;
  }
}
.no-footer {
  :deep(.p-card-footer) {
    display: none;
  }
}

.re-padding {
  :deep(.p-card-content) {
    padding-bottom: 0;
  }
  :deep(.p-card-body) {
    height: 100%;
    display: flex;
    flex-direction: column;
    .p-card-content {
      flex-grow: 1;
    }
  }
}

.card-field {
  .name {
    font-size: 1rem;
    font-weight: 700;
  }
}
</style>
