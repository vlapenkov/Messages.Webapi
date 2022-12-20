<template>
  <app-page title="Организации" class="organizations-manager-page">
    <div class="w-full flex flex-row justify-content-end align-items-center mb-3">
      <span class="p-component text-color-secondary">Сортировка:</span>
      <dropdown
        class="ml-2"
        :style="{ width: '380px' }"
        :options="ordersByOrganizations"
        optionLabel="name"
        placeholder="Выберите"
        v-model="orderByModel"
      />
    </div>
    <card>
      <template #content>
        <div v-if="organizationStatus.status === 'loaded'">
          <data-table
            :value="organizations"
            responsiveLayout="scroll"
            class="no-background-table"
            :sortField="orderByModel.sortField"
            :sortOrder="orderByModel.sortOrder"
          >
            <column field="name" header="Наименование" headerStyle="width: 40%">
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
            <column field="lastModified" header="Дата изменения" headerStyle="width: 20%">
              <template #body="slopProps">
                <span class="p-component">
                  {{ formatDateString(slopProps.data.lastModified) }}
                </span>
              </template>
            </column>
            <column field="lastModifiedBy" header="Кем изменено" headerStyle="width: 15%" />
            <column field="createdBy" header="Кто создал" headerStyle="width: 15%" />
            <column field="statusText" header="Статус" headerStyle="width: 10%">
              <template #body="slopProps">
                <dropdown
                  v-if="orgStatusModels != null && orgStatusModels[slopProps.data.id]?.value === 0"
                  v-model="orgStatusModels[slopProps.data.id]"
                  :options="statuses"
                  optionLabel="name"
                  class="w-full"
                  @change="changed($event, slopProps.data.id)"
                />
                <tag
                  v-else
                  :value="orgStatusModels != null && orgStatusModels[slopProps.data.id]?.name"
                  :style="{
                    backgroundColor:
                      orgStatusModels != null && orgStatusModels[slopProps.data.id]?.color,
                  }"
                  class="w-full"
                />
              </template>
            </column>
          </data-table>
        </div>
        <div v-else class="flex flex-column gap-1">
          <skeleton v-for="i in 15" :key="i" height="70px"></skeleton>
        </div>
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

interface IOrderByOrganizations {
  name: string;
  sortField: string;
  sortOrder: number;
}

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
    const { currentPageItems: organizations, status: organizationStatus } = organizationsStore;
    const formatDateString = (d: Date) => {
      if (d == null) return '';
      return d.toLocaleString('ru-RU', {
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
        hour: 'numeric',
        minute: 'numeric',
        second: 'numeric',
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
    const ordersByOrganizations: IOrderByOrganizations[] = [
      {
        sortField: 'name',
        sortOrder: 1,
        name: 'по названию (возрастание)',
      },
      {
        sortField: 'name',
        sortOrder: -1,
        name: 'по названию (убывание)',
      },
      {
        sortField: 'lastModified',
        sortOrder: 1,
        name: 'по дате изменения (возрастание)',
      },
      {
        sortField: 'lastModified',
        sortOrder: -1,
        name: 'по дате изменения (убывание)',
      },
      {
        sortField: 'lastModifiedBy',
        sortOrder: 1,
        name: 'по изменяющему (возрастание)',
      },
      {
        sortField: 'lastModifiedBy',
        sortOrder: -1,
        name: 'по изменяющему (убывание)',
      },
      {
        sortField: 'createdBy',
        sortOrder: 1,
        name: 'по создающему (возрастание)',
      },
      {
        sortField: 'createdBy',
        sortOrder: -1,
        name: 'по создающему (убывание)',
      },
      {
        sortField: 'statusText',
        sortOrder: 1,
        name: 'по статусу (возрастание)',
      },
      {
        sortField: 'statusText',
        sortOrder: -1,
        name: 'по статусу (убывание)',
      },
    ];
    const orderByModel = ref<IOrderByOrganizations>(ordersByOrganizations[0]);
    return {
      ordersByOrganizations,
      orderByModel,
      organizationStatus,
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
.organizations-manager-page {
  :deep(span.p-dropdown-label.p-inputtext) {
    padding: 6px;
  }
  :deep(.p-card-content) {
    padding: 0;
  }
}
</style>
