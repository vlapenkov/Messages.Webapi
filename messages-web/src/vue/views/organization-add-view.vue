<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page title="Профиль организации">
    <div class="flex flex-column">
      <card class="add-organization-inner-card">
        <template #content>
          <div>
            <h2 class="mt-0">Тип профиля</h2>
            <div class="w-full h-full grid">
              <div class="col-4">
                <div class="h-full w-full flex flex-row align-items-center">
                  <div class="field">
                    <checkbox inputId="isProducer" :binary="true" v-model="formState.isProducer" />
                    <label for="isProducer" class="text-600 m-0 ml-2">Продавец</label>
                  </div>
                </div>
              </div>
              <div class="col-4">
                <div class="h-full w-full flex flex-row align-items-center">
                  <div class="field">
                    <checkbox inputId="isBuyer" :binary="true" v-model="formState.isBuyer" />
                    <label for="isBuyer" class="text-600 m-0 ml-2">Покупатель</label>
                  </div>
                </div>
              </div>
              <div class="col-4">
                <div class="h-full w-full flex flex-row align-items-center">
                  <div class="field">
                    <label for="status" class="text-600">Статус</label>
                    <dropdown
                      id="status"
                      :options="statusOptions"
                      class="w-full p-component rk-dropdown"
                      v-model="formState.statusText"
                      :disabled="!isEditable"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
          <prime-divider class="mt-5 mb-5"></prime-divider>
          <div>
            <h2 class="mt-0">1. Общие сведения об организации</h2>
            <div class="w-full h-full grid">
              <div class="col-4 field">
                <label for="ogrn" class="text-600">ОГРН</label>
                <input-text
                  id="ogrn"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.ogrn"
                />
              </div>
              <div class="col-4 field">
                <label for="full-name" class="text-600">Полное наименование</label>
                <input-text
                  id="full-name"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.fullName"
                />
              </div>
              <div class="col-4"></div>
              <div class="col-4 field">
                <label for="kpp" class="text-600">КПП</label>
                <input-text
                  id="kpp"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.kpp"
                />
              </div>
              <div class="col-4 field">
                <label for="short-name" class="text-600">Сокращенное наименование</label>
                <input-text
                  id="short-name"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.name"
                />
              </div>
              <div class="col-4"></div>
              <div class="col-4 field">
                <label for="inn" class="text-600">ИНН</label>
                <input-text
                  id="inn"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.inn"
                />
              </div>
              <div class="col-8"></div>
            </div>
            <h3 class="mt-4">Отрасль</h3>
            <div class="w-full h-full grid">
              <div class="col-3 field">
                <label for="okved" class="text-600">ОКВЭД</label>
                <input-text
                  id="okved"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.okved"
                />
              </div>
              <div class="col-3 field">
                <label for="okved2" class="text-600">ОКВЭД 2</label>
                <input-text
                  id="okved2"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.okved2"
                />
              </div>
            </div>
          </div>
          <prime-divider class="mt-5 mb-5"></prime-divider>
          <div>
            <h2 class="mt-0">2. Адрес</h2>
            <div class="w-full h-full grid">
              <div class="col-4 field">
                <label for="region" class="text-600">Регион</label>
                <input-text
                  id="region"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.region"
                />
              </div>
              <div class="col-4 field">
                <label for="city" class="text-600">Город</label>
                <input-text
                  id="city"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.city"
                />
              </div>
              <div class="col-4"></div>
              <div class="col-8 field">
                <label for="legal-address" class="text-600">Юридический адрес</label>
                <input-text
                  id="legal-address"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.address"
                />
              </div>
              <div class="col-3"></div>
              <div class="col-8 field">
                <label for="actual-address" class="text-600">Фактический адрес</label>
                <input-text
                  id="actual-address"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.factAddress"
                />
              </div>
              <div class="col-3"></div>
            </div>
          </div>
          <div>
            <h3 class="mt-4">Географические координаты</h3>
            <div class="w-full h-full grid">
              <div class="col-3 field">
                <label for="latitude" class="text-600">Широта</label>
                <input-number
                  id="latitude"
                  mode="decimal"
                  locale="ru-RU"
                  class="w-full p-inputtext-sm rk-input"
                  :minFractionDigits="1"
                  :maxFractionDigits="12"
                  v-model="formState.latitude"
                />
              </div>
              <div class="col-3 field">
                <label for="longitude" class="text-600">Долгота</label>
                <input-number
                  id="longitude"
                  mode="decimal"
                  locale="ru-RU"
                  class="w-full p-inputtext-sm rk-input"
                  :minFractionDigits="1"
                  :maxFractionDigits="12"
                  v-model="formState.longitude"
                />
              </div>
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
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.bankName"
                />
              </div>
              <div class="col-4 field">
                <label for="cor-acc" class="text-600">Корреспондентский счет</label>
                <input-text
                  id="cor-acc"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.corrAccount"
                />
              </div>
              <div class="col-4"></div>
              <div class="col-4 field">
                <label for="acc-number" class="text-600">Номер счета</label>
                <input-text
                  id="acc-number"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.account"
                />
              </div>
              <div class="col-4 field">
                <label for="bik" class="text-600">БИК</label>
                <input-text
                  id="bik"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.bik"
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
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.phone"
                />
              </div>
              <div class="col-8"></div>
              <div class="col-4 field">
                <label for="email" class="text-600">E-mail</label>
                <input-text
                  id="email"
                  type="text"
                  class="w-full p-inputtext-sm rk-input"
                  v-model="formState.email"
                />
              </div>
              <div class="col-8"></div>
              <div class="col-4 field">
                <label for="site" class="text-600">Сайт</label>
                <input-text
                  id="site"
                  type="text"
                  class="w-full rk-input p-inputtext-sm rk-input"
                  v-model="formState.site"
                />
              </div>
              <div class="col-8"></div>
            </div>
          </div>
          <prime-divider class="mt-5 mb-5"></prime-divider>
          <div class="flex flex-row justify-content-end align-items-center">
            <prime-button label="Сохранить" @click="save" />
          </div>
        </template>
      </card>
    </div>
  </app-page>
</template>

<script lang="ts">
import { IOrganizationFullModel } from '@/app/organization-full/@types/IOrganizationFullModel';
import { organizationFullStore } from '@/app/organization-full/state/organization-full.store';
import { OrganizationFullModel } from '@/app/organization-full/models/organozation-full.model';
import { computed, defineComponent, reactive } from 'vue';

export default defineComponent({
  setup() {
    const { createItem, updateSelectedItem, saveChanges, organizationSelected } =
      organizationFullStore;
    const isEditable = computed(() => organizationSelected.value?.mode === 'edit');
    const statusOptions = ['Новая', 'Активна', 'Закрыта'];
    const formState = reactive<IOrganizationFullModel>({
      ogrn: '',
      inn: '',
      kpp: '',
      fullName: '',
      name: '',
      statusText: 'Новая',
      okved: '',
      okved2: '',
      region: '',
      city: '',
      address: '',
      factAddress: '',
      site: '',
      latitude: 0,
      longitude: 0,
      phone: '',
      email: '',
      isProducer: false,
      isBuyer: false,
      bankName: '',
      account: '',
      corrAccount: '',
      bik: '',
    });

    const save = async () => {
      createItem();
      const item = new OrganizationFullModel();
      item.fromResponse(formState);
      updateSelectedItem(item);
      await saveChanges();
    };

    return {
      statusOptions,
      isEditable,
      formState,
      save,
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

  .rk-dropdown {
    margin: 0 !important;
  }

  .rk-input {
    &:deep(.p-inputtext) {
      display: block;
      margin-bottom: 0.5rem;

      &:last-child {
        margin-bottom: 0;
      }
    }
  }

  :deep(.p-card-content) {
    padding-top: 0;
    padding-bottom: 0;
  }
}
</style>
