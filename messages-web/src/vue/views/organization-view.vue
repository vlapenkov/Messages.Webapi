<template>
  <app-page :title="'Карточка организации'">
    <card>
      <template #content>
        <div class="grid">
          <div class="col-12 flex flex-column">
            <div class="text-2xl font-medium">{{ models.mainInfo.name }}</div>
            <!-- <div class="text-base text-primary mt-1">
              <span class="text-color-secondary">КодыОКПД2:</span>&nbsp;<span></span>
            </div> -->
            <div class="text-base mt-1">
              <span class="text-color-secondary">Статус:</span>&nbsp;<span>{{
                models.mainInfo.statusText
              }}</span>
            </div>
            <div class="text-base mt-1">
              <span class="text-color-secondary">Регион:</span>&nbsp;<span>{{
                models.mainInfo.region
              }}</span>
            </div>
            <div class="text-base text-info mt-1">
              <span class="text-color-secondary">Дата актуализации:</span>&nbsp;<span>{{
                models.mainInfo.lastModified
              }}</span>
            </div>
          </div>
        </div>
      </template>
    </card>

    <card class="mt-3">
      <template #content>
        <tab-view ref="tabview1">
          <tab-panel header="Сведения о предприятии">
            <data-view :value="models.regData" responsiveLayout="scroll">
              <template #list="slotProps">
                <div class="col-12">
                  <div class="grid mt-2">
                    <div class="col-4">
                      <span class="font-medium">{{ slotProps.data.key }}</span>
                    </div>
                    <div class="col-8">
                      <span>{{ slotProps.data.value }}</span>
                    </div>
                  </div>
                </div>
              </template>
            </data-view>
          </tab-panel>
          <tab-panel header="Контактные данные">
            <data-view :value="models.contacts" responsiveLayout="scroll">
              <template #list="slotProps">
                <div class="col-12">
                  <div class="grid mt-2">
                    <div class="col-4">
                      <span class="font-medium">{{ slotProps.data.key }}</span>
                    </div>
                    <div class="col-8">
                      <span>{{ slotProps.data.value }}</span>
                    </div>
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
import { defineComponent, watch, computed } from 'vue';
import { useRoute } from 'vue-router';

interface IKeyValue {
  key: string;
  value: string;
}

export default defineComponent({
  setup() {
    const route = useRoute();
    const item = organizationFullStore.organization;
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
        dateStr = d.toLocaleString();
      }
      return dateStr;
    };

    const models = computed(() => ({
      mainInfo: {
        name: item.value?.name,
        statusText: item.value?.statusText,
        region: item.value?.region,
        lastModified: getDateStr(item.value?.lastModified),
      },
      regData: [
        { key: 'Полное наименование', value: item.value?.fullName },
        { key: 'ОГРН', value: item.value?.ogrn },
        { key: 'ИНН', value: item.value?.inn },
        { key: 'КПП', value: item.value?.kpp },
        { key: 'ОКВЕД', value: item.value?.okved },
        { key: 'ОКВЕД 2', value: item.value?.okved2 },
      ] as IKeyValue[],
      contacts: [
        { key: 'Город', value: item.value?.city },
        { key: 'Адрес', value: item.value?.address },
        { key: 'Сайт', value: item.value?.site },
      ] as IKeyValue[],
    }));

    return { models };
  },
});
</script>

<style scoped></style>
