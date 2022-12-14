<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page title="Профиль организации">
    <div>
      <toast position="top-right" group="tr" />
      <div class="flex flex-column">
        <card class="add-organization-inner-card">
          <template #content>
            <div>
              <div class="w-full h-full grid">
                <div class="col-3">
                  <div class="w-full flex flex-row justify-content-center align-items-center">
                    <img
                      :src="organizationImage"
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
                <div class="col-8 file-upload">
                  <div class="w-full flex flex-column">
                    <span class="mb-3 p-component text-xl font-semibold text-900">
                      {{ organizationName }}
                    </span>
                    <file-upload
                      mode="basic"
                      id="organization-img"
                      accept="image/*"
                      :maxFileSize="3000000"
                      @input="onFileInput"
                      :auto="true"
                      :customUpload="true"
                      chooseLabel="Загрузить изображение"
                      class="p-button-sm"
                    />
                  </div>
                </div>
              </div>
            </div>
            <prime-divider class="mt-3 mb-5"></prime-divider>
            <div>
              <h2 class="mt-0">1. Управление аккаунтом</h2>
              <div class="w-full h-full grid">
                <div class="col-6">
                  <div class="h-full w-full flex flex-column">
                    <span class="text-600">Тип профиля</span>
                    <div class="h-full w-full flex flex-row align-items-center">
                      <div class="field mr-5">
                        <checkbox
                          inputId="isProducer"
                          :binary="true"
                          v-model="formState.isProducer"
                          :disabled="isModeration"
                        />
                        <label for="isProducer" class="text-800 m-0 ml-2">Продавец</label>
                      </div>
                      <div class="field">
                        <checkbox
                          inputId="isBuyer"
                          :binary="true"
                          v-model="formState.isBuyer"
                          :disabled="isModeration"
                        />
                        <label for="isBuyer" class="text-800 m-0 ml-2">Покупатель</label>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-6">
                  <div class="h-full w-full flex flex-row align-items-center">
                    <div class="field">
                      <label for="status" class="text-600">Статус</label>
                      <dropdown
                        id="status"
                        :options="statusOptions"
                        class="w-full p-component rk-dropdown"
                        v-model="formState.statusText"
                        :disabled="!isModeration"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <prime-divider class="mt-3 mb-5"></prime-divider>
            <div>
              <h2 class="mt-0">2. Общие сведения об организации</h2>
              <div class="w-full h-full grid">
                <div class="col-4 field">
                  <label for="ogrn" class="text-600">ОГРН</label>
                  <input-text
                    id="ogrn"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    v-model="formState.ogrn"
                    :maxlength="13"
                    :disabled="isModeration"
                  />
                </div>
                <div class="col-4 field">
                  <label for="full-name" class="text-600">Полное наименование</label>
                  <input-text
                    id="full-name"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    v-model="formState.fullName"
                    :disabled="isModeration"
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
                    :maxlength="9"
                    :disabled="isModeration"
                  />
                </div>
                <div class="col-4 field">
                  <label for="short-name" class="text-600">Сокращенное наименование</label>
                  <input-text
                    id="short-name"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    v-model="formState.name"
                    :disabled="isModeration"
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
                    :maxlength="10"
                    :disabled="isModeration"
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
                    :disabled="isModeration"
                  />
                </div>
                <div class="col-3 field">
                  <label for="okved2" class="text-600">ОКВЭД 2</label>
                  <input-text
                    id="okved2"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    v-model="formState.okved2"
                    :disabled="isModeration"
                  />
                </div>
              </div>
            </div>
            <prime-divider class="mt-5 mb-5"></prime-divider>
            <div>
              <h2 class="mt-0">3. Адрес</h2>
              <div class="w-full h-full grid">
                <div class="col-4 field">
                  <label for="region" class="text-600">Регион</label>
                  <input-text
                    id="region"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    v-model="formState.region"
                    :disabled="isModeration"
                  />
                </div>
                <div class="col-4 field">
                  <label for="city" class="text-600">Город</label>
                  <input-text
                    id="city"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    v-model="formState.city"
                    :disabled="isModeration"
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
                    :disabled="isModeration"
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
                    :disabled="isModeration"
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
                    :disabled="isModeration"
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
                    :disabled="isModeration"
                  />
                </div>
              </div>
            </div>
            <prime-divider class="mt-5 mb-5"></prime-divider>
            <div>
              <h2 class="mt-0">4. Банковские реквизиты</h2>
              <div class="w-full h-full grid">
                <div class="col-4 field">
                  <label for="bank" class="text-600">Банк</label>
                  <input-text
                    id="bank"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    v-model="formState.bankName"
                    :disabled="isModeration"
                  />
                </div>
                <div class="col-4 field">
                  <label for="cor-acc" class="text-600">Корреспондентский счет</label>
                  <input-text
                    id="cor-acc"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    v-model="formState.corrAccount"
                    :disabled="isModeration"
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
                    :disabled="isModeration"
                  />
                </div>
                <div class="col-4 field">
                  <label for="bik" class="text-600">БИК</label>
                  <input-text
                    id="bik"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    v-model="formState.bik"
                    :disabled="isModeration"
                  />
                </div>
                <div class="col-4"></div>
              </div>
            </div>
            <prime-divider class="mt-5 mb-5"></prime-divider>
            <div>
              <h2 class="mt-0">5. Контактные данные</h2>
              <div class="w-full h-full grid">
                <div class="col-4 field">
                  <label for="phone" class="text-600">Контактный телефон</label>
                  <input-text
                    id="phone"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    v-model="formState.phone"
                    :disabled="isModeration"
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
                    :disabled="isModeration"
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
                    :disabled="isModeration"
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
    </div>
  </app-page>
</template>

<script lang="ts">
import { IOrganizationFullModel } from '@/app/organization-full/@types/IOrganizationFullModel';
import { organizationFullStore } from '@/app/organization-full/state/organization-full.store';
import { OrganizationFullModel } from '@/app/organization-full/models/organozation-full.model';
import { computed, defineComponent, reactive, Ref, ref, watch } from 'vue';
import { useBase64 } from '@vueuse/core';
import { v4 as uuidv4 } from 'uuid';
import { useToast } from 'primevue/usetoast';
import Toast from 'primevue/toast';
import { useRouter } from 'vue-router';

export default defineComponent({
  components: { Toast },
  setup() {
    const toast = useToast();
    const router = useRouter();
    const { createItem, updateSelectedItem, saveChanges, organizationSelected, status } =
      organizationFullStore;
    const isModeration = computed(() => organizationSelected.value?.mode === 'moderate');
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

      if (status.value.status === 'error') {
        const errors = status.value.payload;
        const firstErr = errors != null ? errors[0] : null;
        const title = firstErr != null ? firstErr[0] : null;
        const description = firstErr != null ? firstErr[1] : null;
        const detail = `${title != null ? title : 'Что-то случилось при добавлении организации'}${
          description != null ? `: ${description[0].toLowerCase() + description.slice(1)}` : ''
        }`;
        toast.add({
          severity: 'error',
          group: 'tr',
          summary: 'Ошибка',
          detail,
          life: 4000,
        });
      }

      if (status.value.status === 'loaded') {
        const id = organizationSelected.value?.data.id;
        if (id !== 0) router.push({ name: 'organization', params: { id } });
      }
    };

    const file = ref() as Ref<File>;
    const { base64: fileB64 } = useBase64(file);
    watch(fileB64, (b64) => {
      if (file.value == null || b64 == null) {
        return;
      }
      const doc = {
        data: b64.replace(/(^data:image\/[a-z]+;base64,)/gi, ''),
        fileId: uuidv4(),
        fileName: file.value.name,
      };
      formState.document = doc;
    });
    const onFileInput = (e: Event) => {
      const target = e.target as HTMLInputElement;
      const { files } = target;
      if (files == null) {
        return;
      }
      [file.value] = files;
    };

    const organizationName = computed(() =>
      formState.name != null && formState.name !== '' ? formState.name : 'Название',
    );
    const organizationImage = computed(() =>
      file.value != null ? fileB64.value : '/images/upload_avatar.png',
    );

    return {
      organizationImage,
      organizationName,
      statusOptions,
      isModeration,
      formState,
      fileB64,
      status,
      file,
      save,
      onFileInput,
    };
  },
});
</script>

<style lang="scss" scoped>
.file-upload {
  :deep(.p-fileupload-choose) {
    background-color: #f4f7fb;
    border-color: #f4f7fb;
    color: var(--gray-600);

    &:hover {
      background-color: #f4f7fb;
      border-color: #f4f7fb;
      color: var(--gray-600);
    }
  }
}

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
