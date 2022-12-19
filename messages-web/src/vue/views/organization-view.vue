<template>
  <app-page title="Данные об организации">
    <card class="re-padding">
      <template #content>
        <div>
          <div class="w-full h-full grid">
            <div class="col-3">
              <div class="w-full flex flex-row justify-content-center align-items-center">
                <file-store-image
                  v-if="item?.documentId != null"
                  :id="item?.documentId"
                  :max-height="150"
                  :fitWidth="true"
                  :objectFit="'cover'"
                ></file-store-image>
                <img
                  v-if="item?.documentId == null"
                  :src="require('@/assets/images/profile.svg')"
                  alt="Изображение профиля"
                  width="150"
                  height="150"
                  :style="{
                    objectFit: 'cover',
                    borderRadius: '0.5rem',
                  }"
                />
              </div>
            </div>
            <div class="col-8">
              <div class="flex flex-column">
                <div class="text-xl font-medium mb-3">{{ mainInfo.name }}</div>
                <div class="text-base mt-1">
                  <span class="text-color-secondary">Регион:</span>&nbsp;
                  <span>
                    {{ mainInfo.region }}
                  </span>
                </div>
                <div class="text-base mt-1">
                  <span class="text-color-secondary">Статус:</span>&nbsp;
                  <span
                    :style="{
                      color: statusColor,
                    }"
                  >
                    {{ mainInfo.statusText }}
                  </span>
                </div>
                <div class="text-base text-info mt-1">
                  <span class="text-600">Дата актуализации:</span>&nbsp;
                  <span class="text-600">
                    {{ mainInfo.lastModified }}
                  </span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </template>
    </card>

    <card class="re-padding mt-3">
      <template #content>
        <tab-view ref="tabview1">
          <tab-panel v-for="i in info" :key="i.header" :header="i.header">
            <data-view :value="i.data" responsiveLayout="scroll">
              <template #list="slotProps">
                <div class="h-full w-full grid mt-2">
                  <div class="col-4">
                    <span class="font-medium">{{ slotProps.data.key }}</span>
                  </div>
                  <div class="col-8">
                    <span>{{ slotProps.data.value }}</span>
                  </div>
                </div>
              </template>
            </data-view>
          </tab-panel>
        </tab-view>
      </template>
    </card>
  </app-page>
</template>

<script lang="ts">
import { organizationFullStore } from '@/app/organization-full/state/organization-full.store';
import { useOrganizationStatuses } from '@/composables/organization-statuses.composable';
import { defineComponent, watch, computed } from 'vue';
import { useRoute } from 'vue-router';

interface IKeyValue {
  key: string;
  value: string;
}

interface IInfoValue {
  header: string;
  data: IKeyValue[];
}

export default defineComponent({
  setup() {
    const route = useRoute();
    const item = organizationFullStore.organization;
    const { statuses } = useOrganizationStatuses();
    const statusColor = computed(
      () => statuses.value.find((x) => x.name === item.value?.statusText)?.color,
    );
    watch(
      () => route.params.id as string | undefined,
      (id) => {
        if (id == null || id === '' || id.trim() === '') {
          return;
        }
        organizationFullStore.getDataAsync(+id);
      },
      {
        immediate: true,
      },
    );

    const getDateStr = (date: string | undefined) => {
      let dateStr = '';
      if (date != null) {
        const d = new Date(date);
        dateStr = d.toLocaleString('ru-RU', {
          year: 'numeric',
          month: 'numeric',
          day: 'numeric',
        });
      }
      return dateStr;
    };

    const mainInfo = computed(() => ({
      name: item.value?.name,
      statusText: item.value?.statusText,
      region: item.value?.region,
      lastModified: getDateStr(item.value?.lastModified),
    }));

    const info = computed<IInfoValue[]>(() => [
      {
        header: 'Сведения о предприятии',
        data: [
          { key: 'Полное наименование', value: item.value?.fullName },
          { key: 'Сокращенное наименование', value: item.value?.name },
          { key: 'ОГРН', value: item.value?.ogrn },
          { key: 'ИНН', value: item.value?.inn },
          { key: 'КПП', value: item.value?.kpp },
        ] as IKeyValue[],
      },
      {
        header: 'Отрасль',
        data: [
          { key: 'ОКВЕД', value: item.value?.okved },
          { key: 'ОКВЕД 2', value: item.value?.okved2 },
        ] as IKeyValue[],
      },
      {
        header: 'Банковские реквизиты',
        data: [
          { key: 'Банк', value: item.value?.bankName },
          { key: 'Корреспондентский счет', value: item.value?.corrAccount },
          { key: 'Номер счета', value: item.value?.account },
          { key: 'БИК', value: item.value?.bik },
        ] as IKeyValue[],
      },
      {
        header: 'Контактные данные',
        data: [
          { key: 'Контактный телефон', value: item.value?.phone },
          { key: 'E-mail', value: item.value?.email },
          { key: 'Сайт', value: item.value?.site },
          { key: 'Регион', value: item.value?.region },
          { key: 'Город', value: item.value?.city },
          { key: 'Юридический адрес', value: item.value?.address },
          { key: 'Фактический адрес', value: item.value?.factAddress },
          { key: 'Широта', value: item.value?.latitude },
          { key: 'Долгота', value: item.value?.longitude },
        ] as IKeyValue[],
      },
    ]);

    return { item, info, mainInfo, statusColor };
  },
});
</script>

<style lang="scss" scopped>
.re-padding {
  .p-card-body {
    padding-top: 0;
    padding-bottom: 0;
  }
}
</style>
