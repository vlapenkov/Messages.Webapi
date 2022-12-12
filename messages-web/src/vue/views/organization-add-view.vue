<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page title="Профиль организации">
    <div class="flex flex-column">
      <card class="add-organization-inner-card">
        <template #content>
          <div>
            <h2 class="mt-0">1. Общие сведения об организации</h2>
            <div class="w-full h-full grid">
              <div class="col-4 field">
                <label for="full-name" class="text-600">Полное наименование</label>
                <input-text
                  id="full-name"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.fullName"
                />
              </div>
              <div class="col-4 field">
                <label for="ogrn" class="text-600">ОГРН</label>
                <input-text
                  id="ogrn"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.ogrn"
                />
              </div>
              <div class="col-4"></div>
              <div class="col-4 field">
                <label for="short-name" class="text-600">Сокращенное наименование</label>
                <input-text
                  id="short-name"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.name"
                />
              </div>
              <div class="col-4 field">
                <label for="kpp" class="text-600">КПП</label>
                <input-text
                  id="kpp"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.kpp"
                />
              </div>
              <div class="col-4"></div>
              <div class="col-4 field">
                <label for="status" class="text-600">Статус</label>
                <input-text
                  id="status"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.statusText"
                />
              </div>
              <div class="col-4 field">
                <label for="inn" class="text-600">ИНН</label>
                <input-text
                  id="inn"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.inn"
                />
              </div>
              <div class="col-4"></div>
            </div>
            <h3 class="mt-4">Отрасль</h3>
            <div class="w-full h-full grid">
              <div class="col-3 field">
                <label for="okved" class="text-600">ОКВЭД</label>
                <input-text
                  id="okved"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.okved"
                />
              </div>
              <div class="col-3 field">
                <label for="okved2" class="text-600">ОКВЭД 2</label>
                <input-text
                  id="okved2"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.okved2"
                />
              </div>
            </div>
          </div>
          <prime-divider class="mt-5 mb-5"></prime-divider>
          <div>
            <h2 class="mt-0">2. Адрес</h2>
            <div class="w-full h-full grid">
              <div class="col-8 field">
                <label for="legal-address" class="text-600">Юридический адрес</label>
                <input-text
                  id="legal-address"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.legalAddress"
                  disabled
                />
              </div>
              <div class="col-3"></div>
              <div class="col-8 field">
                <label for="actual-address" class="text-600">Фактический адрес</label>
                <input-text
                  id="actual-address"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.actualAddress"
                  disabled
                />
              </div>
              <div class="col-3"></div>
              <div class="col-8 field">
                <label for="postal-address" class="text-600">Почтовый адрес</label>
                <input-text
                  id="postal-address"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.postalAddress"
                  disabled
                />
              </div>
              <div class="col-3"></div>
            </div>
          </div>
          <prime-divider class="mt-5 mb-5"></prime-divider>
          <div>
            <h2 class="mt-0">3. Банковские реквизиты</h2>
            <div class="w-full h-full grid">
              <div class="col-4 field">
                <label for="bank" class="text-600">Банк</label>
                <input-text
                  id="bank"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.bank"
                  disabled
                />
              </div>
              <div class="col-4 field">
                <label for="cor-acc" class="text-600">Корреспондентский счет</label>
                <input-text
                  id="cor-acc"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.corAcc"
                  disabled
                />
              </div>
              <div class="col-4"></div>
              <div class="col-4 field">
                <label for="acc-number" class="text-600">Номер счета</label>
                <input-text
                  id="acc-number"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.accNumber"
                  disabled
                />
              </div>
              <div class="col-4 field">
                <label for="bik" class="text-600">БИК</label>
                <input-text
                  id="bik"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.bik"
                  disabled
                />
              </div>
              <div class="col-4"></div>
            </div>
          </div>
          <prime-divider class="mt-5 mb-5"></prime-divider>
          <div>
            <h2 class="mt-0">4. Контактные данные</h2>
            <div class="w-full h-full grid">
              <div class="col-4 field">
                <label for="phone" class="text-600">Контактный телефон</label>
                <input-text
                  id="phone"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.phone"
                  disabled
                />
              </div>
              <div class="col-8"></div>
              <div class="col-4 field">
                <label for="email" class="text-600">E-mail</label>
                <input-text
                  id="email"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.email"
                  disabled
                />
              </div>
              <div class="col-8"></div>
              <div class="col-4 field">
                <label for="site" class="text-600">Сайт</label>
                <input-text
                  id="site"
                  type="text"
                  class="w-full p-inputtext-sm"
                  v-model="formState.site"
                />
              </div>
              <div class="col-8"></div>
            </div>
          </div>
          <prime-divider class="mt-5 mb-5"></prime-divider>
          <div class="flex flex-row justify-content-end align-items-center">
            <prime-button label="Сохранить" />
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
    margin: 0;
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
