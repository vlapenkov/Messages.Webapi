<template>
  <app-page title="Организации">
    <card>
      <template #content>
        <data-table :value="organizations" responsiveLayout="scroll">
          <column header="Наименование" headerStyle="width: 40%">
            <template #body="slopProps">
              <div class="w-full flex flex-row align-items-center">
                <file-store-image
                  v-if="slopProps.data.documentId != null"
                  :max-width="50"
                  :max-height="50"
                  :id="slopProps.data.documentId"
                  class="mr-3"
                ></file-store-image>
                <span class="p-component">{{ slopProps.data.name }}</span>
              </div>
            </template>
          </column>
          <column header="Последнее редактирование" headerStyle="width: 30%">
            <template #body="slopProps">
              <span class="p-component">
                {{ formatDateString(slopProps.data.lastModified) }}
                {{
                  slopProps.data.lastModifiedBy != null && slopProps.data.lastModifiedBy != ''
                    ? ','
                    : undefined
                }}
                {{ slopProps.data.lastModifiedBy }}
              </span>
            </template>
          </column>
          <column field="statusText" header="Статус" headerStyle="width: 30%">
            <template #body="slopProps">
              <dropdown
                v-if="orgStatusModels != null"
                v-model="orgStatusModels[slopProps.data.id]"
                :options="statuses"
                optionLabel="name"
                class="w-full"
                :disabled="orgStatusModels[slopProps.data.id]?.value !== 0"
                @change="changed($event, slopProps.data.id)"
              />
            </template>
          </column>
        </data-table>
      </template>
    </card>
  </app-page>
</template>

<script lang="ts">
import { updateStatus } from '@/app/organizations/infrastructure/organization.http-service';
import { useOrganizations } from '@/composables/organizations.composable';
import { IStatus, useStatuses } from '@/composables/statuses.composable';
import { computed, defineComponent, Ref, ref, watch } from 'vue';

export default defineComponent({
  setup() {
    const { statuses, initial } = useStatuses();
    const { organizations } = useOrganizations();
    const formatDateString = (d: Date) => {
      if (d == null) return '';
      return d.toLocaleString('ru-RU', {
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
      });
    };
    const orgStatusModels = ref<Record<number, IStatus | undefined>>();
    const orgStatuses = computed<Record<number, IStatus | undefined>>(() => {
      const res: Record<number, IStatus | undefined> = {};
      if (initial.value == null) return res;
      organizations.value.forEach((org) => {
        Object.assign(res, {
          ...res,
          [org.id]: statuses.value.find((s) => s.name === org.statusText) ?? initial.value,
        });
      });
      return res;
    });
    watch(
      orgStatuses,
      (val) => {
        orgStatusModels.value = val;
      },
      {
        immediate: true,
      },
    );
    const changed = (val: Ref<IStatus>, id: number) => {
      const status = val.value.value;
      updateStatus(id, status);
    };
    return {
      orgStatuses,
      orgStatusModels,
      statuses,
      organizations,
      changed,
      formatDateString,
    };
  },
});
</script>

<style scoped></style>
