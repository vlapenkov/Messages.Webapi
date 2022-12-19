<template>
  <app-page title="Организации">
    <card>
      <template #content>
        <data-table :value="organizations" responsiveLayout="scroll" class="no-background-table">
          <column field="name" header="Наименование" headerStyle="width: 40%" :sortable="true">
            <template #body="slopProps">
              <router-link
                :to="{ name: 'organization', params: { id: slopProps.data.id } }"
                class="no-underline text-color"
              >
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
              </router-link>
            </template>
          </column>
          <column
            field="lastModified"
            header="Дата изменения"
            headerStyle="width: 20%"
            :sortable="true"
          >
            <template #body="slopProps">
              <span class="p-component">
                {{ formatDateString(slopProps.data.lastModified) }}
              </span>
            </template>
          </column>
          <column
            field="lastModifiedBy"
            header="Кем изменено"
            headerStyle="width: 20%"
            :sortable="true"
          />
          <column field="statusText" header="Статус" headerStyle="width: 20%" :sortable="true">
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
import {
  IOrganizationStatus,
  useOrganizationStatuses,
} from '@/composables/organization-statuses.composable';
import { computed, defineComponent, onMounted, Ref, ref, watch } from 'vue';
import { organizationsStore } from '@/app/organizations/state/organizations.store';

export default defineComponent({
  setup() {
    onMounted(() => {
      // на бэке нет параметров для пэйджинга, грузим все
      organizationsService.loadPage({
        pageNumber: 1,
        pageSize: 8,
      });
    });
    const { statuses, initial } = useOrganizationStatuses();
    const { currentPageItems: organizations } = organizationsStore;
    const formatDateString = (d: Date) => {
      if (d == null) return '';
      return d.toLocaleString('ru-RU', {
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
      });
    };
    const orgStatusModels = ref<Record<number, IOrganizationStatus | undefined>>();
    const orgStatuses = computed<Record<number, IOrganizationStatus | undefined>>(() => {
      const res: Record<number, IOrganizationStatus | undefined> = {};
      if (initial.value == null) return res;
      (organizations.value ?? []).forEach((org) => {
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
    const changed = async (val: Ref<IOrganizationStatus>, id: number) => {
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

<style lang="scss" scoped>
:deep(div.p-dropdown.p-component.p-inputwrapper.p-inputwrapper-filled
    span.p-dropdown-label.p-inputtext) {
  padding: 6px;
}
:deep(.p-card-content) {
  padding: 0;
}
</style>
