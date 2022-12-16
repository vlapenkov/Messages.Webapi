<template>
  <app-page title="Организации">
    <card>
      <template #content>
        <data-table :value="organizations" responsiveLayout="scroll">
          <column header="Наименование" headerStyle="width: 40%">
            <template #body="slopProps">
              <div class="w-full flex flex-row align-items-center">
                <img
                  v-if="slopProps.data.documentId == null"
                  :src="require('@/assets/images/profile.svg')"
                  alt="Изображение профиля"
                  width="50"
                  height="50"
                  :style="{
                    objectFit: 'cover',
                    borderRadius: '0.5rem',
                  }"
                  class="mr-3"
                />
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
          <column header="Дата изменения" headerStyle="width: 20%">
            <template #body="slopProps">
              <span class="p-component">
                {{ formatDateString(slopProps.data.lastModified) }}
              </span>
            </template>
          </column>
          <column field="lastModifiedBy" header="Кем изменено" headerStyle="width: 20%" />
          <column field="statusText" header="Статус" headerStyle="width: 20%">
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
import { organizationsService } from '@/app/organizations/services/organization.service';
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
    const changed = async (val: Ref<IStatus>, id: number) => {
      const status = val.value.value;
      await organizationsService.updateStatus(id, status);
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
