<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page title="Создание организации">
    <div class="flex flex-column">
      <card class="add-organization-inner-card">
        <template #content>
          <div class="w-full h-full grid">
            <div class="col-6">
              <h3 class="mt-0">Сведения о предприятии</h3>
              <div class="field">
                <label for="ogrn">ОГРН</label>
                <input-text
                  id="ogrn"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.ogrn"
                />
              </div>
              <div class="field">
                <label for="inn">ИНН</label>
                <input-text
                  id="inn"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.inn"
                />
              </div>
              <div class="field">
                <label for="kpp">КПП</label>
                <input-text
                  id="kpp"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.kpp"
                />
              </div>
              <div class="field">
                <label for="full-name">Полное наименование</label>
                <input-text
                  id="full-name"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.fullName"
                />
              </div>
              <div class="field">
                <label for="short-name">Сокращенное наименование</label>
                <input-text
                  id="short-name"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.name"
                />
              </div>
              <div class="field">
                <label for="status">Статус</label>
                <input-text
                  id="status"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.statusText"
                />
              </div>
            </div>
            <div class="col-6">
              <h3 class="mt-0">Банковские реквизиты</h3>
              <div class="field">
                <label for="bik">БИК</label>
                <input-text
                  id="bik"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.bik"
                />
              </div>
              <div class="field">
                <label for="rc">Р/с</label>
                <input-text
                  id="rc"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.rc"
                />
              </div>
              <div class="field">
                <label for="bank">Банк</label>
                <input-text
                  id="bank"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.bank"
                />
              </div>
              <div class="field">
                <label for="cor-acc">Кор/счёт</label>
                <input-text
                  id="cor-acc"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.corAcc"
                />
              </div>
            </div>
            <div class="col-6">
              <h3 class="mt-5">Отрасль</h3>
              <div class="field">
                <label for="okved">ОКВЭД</label>
                <input-text
                  id="okved"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.okved"
                />
              </div>
              <div class="field">
                <label for="okved2">ОКВЭД 2</label>
                <input-text
                  id="okved2"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.okved2"
                />
              </div>
            </div>
            <div class="col-6"></div>
            <div class="col-6">
              <h3 class="mt-5">Контактные данные</h3>
              <div class="field">
                <label for="country">Страна</label>
                <input-text
                  id="country"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.country"
                />
              </div>
              <div class="field">
                <label for="region">Регион</label>
                <input-text
                  id="region"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.region"
                />
              </div>
              <div class="field">
                <label for="city">Город</label>
                <input-text
                  id="city"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.city"
                />
              </div>
              <div class="field">
                <label for="address">Адрес</label>
                <input-text
                  id="address"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.address"
                />
              </div>
              <div class="field">
                <label for="site">Сайт</label>
                <input-text
                  id="site"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.site"
                />
              </div>
            </div>
            <div class="col-6"></div>
            <div class="col-6">
              <div class="mt-5">
                <div class="field">
                  <checkbox inputId="buyer" v-model="formState.isBuyer" :binary="true" />
                  <label for="buyer" class="m-0 ml-1">Покупатель</label>
                </div>
                <div class="field">
                  <checkbox inputId="seller" v-model="formState.isSeller" :binary="true" />
                  <label for="seller" class="m-0 ml-1">Продавец</label>
                </div>
              </div>
            </div>
          </div>
        </template>
      </card>
    </div>
  </app-page>
</template>

<script lang="ts">
import { defineComponent, reactive } from 'vue';

interface IOrganizationFormModel {
  name: string;
  fullName: string;
  ogrn: string;
  inn: string;
  kpp: string;
  country?: string;
  region: string;
  city: string;
  address: string;
  site: string;
  okved: string;
  okved2: string;
  statusText: string;
  bik?: string;
  rc?: string;
  bank?: string;
  corAcc?: string;
  isBuyer?: boolean;
  isSeller?: boolean;
}

export default defineComponent({
  setup() {
    const formState = reactive<IOrganizationFormModel>({
      ogrn: '',
      inn: '',
      kpp: '',
      fullName: '',
      name: '',
      statusText: '',
      okved: '',
      okved2: '',
      country: '',
      region: '',
      city: '',
      address: '',
      site: '',
      bik: '',
      rc: '',
      bank: '',
      corAcc: '',
      isBuyer: false,
      isSeller: false,
    });

    return {
      formState,
    };
  },
});
</script>

<style lang="scss" scoped>
.add-organization-inner-card {
  .field {
    display: block;
    margin-bottom: 1rem;
    &:last-child {
      margin-bottom: 0;
    }
  }

  :deep(.p-inputtext) {
    display: block;
    margin-bottom: 0.5rem;

    &:last-child {
      margin-bottom: 0;
    }
  }
  :deep(.p-card-content) {
    padding-top: 0;
    padding-bottom: 0;
  }
}
</style>
