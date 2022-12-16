<!-- eslint-disable vue/no-unused-vars -->
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
import { organizationHttpService } from '@/app/organization-full/infrastructure/organozation-full.http-service';
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
          [org.id]: statuses.value.find((s) => s.value === +org.statusText) ?? initial.value,
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
      console.log(status, id);
      organizationHttpService.setStatus({
        id,
        status,
      });
    };
    // watch(orgStatusModels, (val, prev) => {
    //   console.log(val, prev);
    //   if (val == null || prev == null) return;

    //   const valKeys = Object.keys(val);
    //   const prevKeys = Object.keys(prev);
    //   if (valKeys.length !== prevKeys.length) return;

    //   interface IRes {
    //     orgId: number;
    //     newVal: IStatus | undefined;
    //   }
    //   const changed: IRes[] = [];
    //   valKeys.forEach((key) => {
    //     const valField = val[+key];
    //     const prevField = prev[+key];
    //     if (valField !== prevField) {
    //       changed.push({
    //         orgId: +key,
    //         newVal: valField,
    //       });
    //     }
    //   });
    //   console.log(changed);
    // });
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
