<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page title="Регистрация">
    <toast position="top-right" group="tr" />
    <card class="re-padding shadow-none">
      <template #content>
        <tab-view lazy class="w-full">
          <tab-panel header="Профиль пользователя">
            <div>
              <h2 class="mt-0">Контактные данные</h2>
              <div class="w-full h-full grid">
                <div class="col-4 field">
                  <label
                    for="lastName"
                    :class="{
                      'p-error': uv$.lastName.$invalid && submitted,
                      'text-600': !uv$.lastName.$invalid,
                    }"
                  >
                    Фамилия
                  </label>
                  <input-text
                    id="lastName"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    :class="{ 'p-invalid': uv$.lastName.$invalid && submitted }"
                    v-model="uv$.lastName.$model"
                  />
                  <template v-if="uv$.lastName.$invalid && submitted">
                    <small class="p-error"> Не должно быть пустым </small>
                  </template>
                </div>
                <div class="col-4 field">
                  <label
                    for="firstName"
                    :class="{
                      'p-error': uv$.firstName.$invalid && submitted,
                      'text-600': !uv$.firstName.$invalid,
                    }"
                  >
                    Имя
                  </label>
                  <input-text
                    id="firstName"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    :class="{ 'p-invalid': uv$.firstName.$invalid && submitted }"
                    v-model="uv$.firstName.$model"
                  />
                  <template v-if="uv$.firstName.$invalid && submitted">
                    <small class="p-error"> Не должно быть пустым </small>
                  </template>
                </div>
                <div class="col-4 field">
                  <label
                    for="patronymic"
                    :class="{
                      'p-error': uv$.patronymic.$invalid && submitted,
                      'text-600': !uv$.patronymic.$invalid,
                    }"
                  >
                    Отчество
                  </label>
                  <input-text
                    id="patronymic"
                    type="text"
                    class="w-full p-inputtext-sm rk-input"
                    :class="{ 'p-invalid': uv$.patronymic.$invalid && submitted }"
                    v-model="uv$.patronymic.$model"
                  />
                  <template v-if="uv$.patronymic.$invalid && submitted">
                    <small class="p-error"> Не должно быть пустым </small>
                  </template>
                </div>
                <div class="col-4 field">
                  <label for="phone" class="text-600">Контактный телефон</label>
                  <input-mask
                    id="phone"
                    mask="9 (999) 999-99-99"
                    class="w-full p-inputtext-sm rk-input"
                    v-model="uv$.phone.$model"
                  />
                </div>
                <div class="col-8"></div>
                <div class="col-4 field">
                  <label
                    for="email"
                    :class="{
                      'p-error': uv$.email.$invalid && submitted,
                      'text-600': !uv$.email.$invalid,
                    }"
                  >
                    E-mail
                  </label>
                  <input-text
                    id="email"
                    type="email"
                    class="w-full p-inputtext-sm rk-input"
                    :class="{ 'p-invalid': uv$.email.$invalid && submitted }"
                    v-model="uv$.email.$model"
                  />
                  <template v-if="uv$.email.$invalid && submitted">
                    <small class="p-error"> Невалидный E-mail</small>
                  </template>
                </div>
                <div class="col-8"></div>
                <div class="col-4 field">
                  <label
                    for="password"
                    :class="{
                      'p-error': uv$.password.$invalid && submitted,
                      'text-600': !uv$.password.$invalid,
                    }"
                  >
                    Пароль
                  </label>
                  <input-text
                    id="password"
                    type="password"
                    class="w-full p-inputtext-sm rk-input"
                    :class="{ 'p-invalid': uv$.password.$invalid && submitted }"
                    v-model="uv$.password.$model"
                  />
                  <template v-if="uv$.password.$invalid && submitted">
                    <small class="p-error"> Не должно быть пустым </small>
                  </template>
                </div>
                <div class="col-8"></div>
                <div class="col-4 field">
                  <label
                    for="role"
                    :class="{
                      'p-error': uv$.role.$invalid && submitted,
                      'text-600': !uv$.role.$invalid,
                    }"
                  >
                    Роль
                  </label>
                  <dropdown
                    id="role"
                    :options="roleOptions"
                    class="w-full p-component rk-dropdown"
                    :class="{ 'p-invalid': uv$.role.$invalid && submitted }"
                    v-model="uv$.role.$model"
                  />
                  <template v-if="uv$.role.$invalid && submitted">
                    <small class="p-error"> Не должно быть пустым </small>
                  </template>
                </div>
                <div class="col-8"></div>
              </div>
            </div>
          </tab-panel>
          <tab-panel header="Профиль организации">
            <div>
              <div>
                <div class="w-full h-full grid">
                  <div class="col-3">
                    <div class="w-full flex flex-row justify-content-center align-items-center">
                      <img
                        :src="
                          document != null ? documentBase64 : require('@/assets/images/profile.svg')
                        "
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
                    <div class="w-full h-full flex flex-column justify-content-center">
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
                      <span class="text-600 mb-2">Тип профиля</span>
                      <div class="h-full w-full flex flex-row align-items-center">
                        <div class="field mr-5">
                          <checkbox
                            inputId="isProducer"
                            :binary="true"
                            v-model="ov$.isProducer.$model"
                            :disabled="isModeration"
                            :class="{
                              'p-invalid': ov$.isProducer.$invalid && submitted,
                            }"
                          />
                          <label
                            for="isProducer"
                            class="m-0 ml-2"
                            :class="{
                              'p-error': ov$.isProducer.$invalid && submitted,
                              'text-800': !ov$.isProducer.$invalid,
                            }"
                          >
                            Продавец
                          </label>
                        </div>
                        <div class="field">
                          <checkbox
                            inputId="isBuyer"
                            :binary="true"
                            v-model="ov$.isBuyer.$model"
                            :disabled="isModeration"
                            :class="{
                              'p-invalid': ov$.isBuyer.$invalid && submitted,
                            }"
                          />
                          <label
                            for="isBuyer"
                            class="m-0 ml-2"
                            :class="{
                              'p-error': ov$.isBuyer.$invalid && submitted,
                              'text-800': !ov$.isBuyer.$invalid,
                            }"
                          >
                            Покупатель
                          </label>
                        </div>
                      </div>
                      <template v-if="!hasSelectedOrgType && submitted">
                        <small class="p-error"> Должен быть выбран хотя бы один тип профиля </small>
                      </template>
                    </div>
                  </div>
                  <div class="col-6" v-if="isModeration">
                    <div class="h-full w-full flex flex-row align-items-center">
                      <div class="field">
                        <label for="status" class="text-600">Статус</label>
                        <dropdown
                          id="status"
                          :options="statusOptions"
                          class="w-full p-component rk-dropdown"
                          v-model="orgFormState.statusText"
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
                    <label
                      for="ogrn"
                      :class="{
                        'p-error': ov$.ogrn.$invalid && submitted,
                        'text-600': !ov$.ogrn.$invalid,
                      }"
                      >ОГРН</label
                    >
                    <input-text
                      id="ogrn"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      :class="{ 'p-invalid': ov$.ogrn.$invalid && submitted }"
                      v-model="ov$.ogrn.$model"
                      :maxlength="13"
                      :disabled="isModeration"
                    />
                    <template v-if="ov$.ogrn.$invalid && submitted">
                      <small class="p-error"> Состоит из 13 цифр </small>
                    </template>
                  </div>
                  <div class="col-4 field">
                    <label
                      for="full-name"
                      :class="{
                        'p-error': ov$.fullName.$invalid && submitted,
                        'text-600': !ov$.fullName.$invalid,
                      }"
                      >Полное наименование</label
                    >
                    <input-text
                      id="full-name"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      :class="{ 'p-invalid': ov$.fullName.$invalid && submitted }"
                      v-model="ov$.fullName.$model"
                      :disabled="isModeration"
                    />
                    <template v-if="ov$.fullName.$invalid && submitted">
                      <small class="p-error"> Не менее 5 символов </small>
                    </template>
                  </div>
                  <div class="col-4"></div>
                  <div class="col-4 field">
                    <label
                      for="kpp"
                      :class="{
                        'p-error': ov$.kpp.$invalid && submitted,
                        'text-600': !ov$.kpp.$invalid,
                      }"
                      >КПП</label
                    >
                    <input-text
                      id="kpp"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      :class="{ 'p-invalid': ov$.kpp.$invalid && submitted }"
                      v-model="ov$.kpp.$model"
                      :maxlength="9"
                      :disabled="isModeration"
                    />
                    <template v-if="ov$.kpp.$invalid && submitted">
                      <small class="p-error"> Состоит из 9 цифр </small>
                    </template>
                  </div>
                  <div class="col-4 field">
                    <label
                      for="short-name"
                      :class="{
                        'p-error': ov$.name.$invalid && submitted,
                        'text-600': !ov$.name.$invalid,
                      }"
                      >Сокращенное наименование</label
                    >
                    <input-text
                      id="short-name"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      :class="{ 'p-invalid': ov$.name.$invalid && submitted }"
                      v-model="ov$.name.$model"
                      :disabled="isModeration"
                    />
                    <template v-if="ov$.name.$invalid && submitted">
                      <small class="p-error"> Не менее 5 символов </small>
                    </template>
                  </div>
                  <div class="col-4"></div>
                  <div class="col-4 field">
                    <label
                      for="inn"
                      :class="{
                        'p-error': ov$.inn.$invalid && submitted,
                        'text-600': !ov$.inn.$invalid,
                      }"
                      >ИНН</label
                    >
                    <input-text
                      id="inn"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      :class="{ 'p-invalid': ov$.inn.$invalid && submitted }"
                      v-model="ov$.inn.$model"
                      :maxlength="10"
                      :disabled="isModeration"
                    />
                    <template v-if="ov$.inn.$invalid && submitted">
                      <small class="p-error"> Состоит из 10 цифр </small>
                    </template>
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
                      v-model="ov$.okved.$model"
                      :disabled="isModeration"
                    />
                  </div>
                  <div class="col-3 field">
                    <label for="okved2" class="text-600">ОКВЭД 2</label>
                    <input-text
                      id="okved2"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      v-model="ov$.okved2.$model"
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
                    <label
                      for="region"
                      :class="{
                        'p-error': ov$.region.$invalid && submitted,
                        'text-600': !ov$.region.$invalid,
                      }"
                      >Регион</label
                    >
                    <input-text
                      id="region"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      :class="{ 'p-invalid': ov$.region.$invalid && submitted }"
                      v-model="ov$.region.$model"
                      :disabled="isModeration"
                    />
                    <template v-if="ov$.region.$invalid && submitted">
                      <small class="p-error"> Не менее 5 символов </small>
                    </template>
                  </div>
                  <div class="col-4 field">
                    <label
                      for="city"
                      :class="{
                        'p-error': ov$.city.$invalid && submitted,
                        'text-600': !ov$.city.$invalid,
                      }"
                      >Город</label
                    >
                    <input-text
                      id="city"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      :class="{ 'p-invalid': ov$.city.$invalid && submitted }"
                      v-model="ov$.city.$model"
                      :disabled="isModeration"
                    />
                    <template v-if="ov$.city.$invalid && submitted">
                      <small class="p-error"> Не должно быть пустым </small>
                    </template>
                  </div>
                  <div class="col-4"></div>
                  <div class="col-8 field">
                    <label
                      for="legal-address"
                      :class="{
                        'p-error': ov$.address.$invalid && submitted,
                        'text-600': !ov$.address.$invalid,
                      }"
                      >Юридический адрес</label
                    >
                    <input-text
                      id="legal-address"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      :class="{ 'p-invalid': ov$.address.$invalid && submitted }"
                      v-model="ov$.address.$model"
                      :disabled="isModeration"
                    />
                    <template v-if="ov$.address.$invalid && submitted">
                      <small class="p-error"> Не должно быть пустым </small>
                    </template>
                  </div>
                  <div class="col-3"></div>
                  <div class="col-8 field">
                    <label for="actual-address" class="text-600">Фактический адрес</label>
                    <input-text
                      id="actual-address"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      v-model="ov$.factAddress.$model"
                      :disabled="isModeration"
                    />
                  </div>
                  <div class="col-3"></div>
                </div>
              </div>
              <div>
                <accordion>
                  <accordion-tab header="Географические координаты">
                    <div class="w-full h-full grid">
                      <div class="col-3 field">
                        <label for="latitude" class="text-600">Широта</label>
                        <input-number
                          id="latitude"
                          mode="decimal"
                          locale="ru-RU"
                          class="w-full p-inputtext-sm rk-input"
                          placeholder="Не указана"
                          :minFractionDigits="1"
                          :maxFractionDigits="12"
                          v-model="orgFormState.latitude"
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
                          placeholder="Не указана"
                          :minFractionDigits="1"
                          :maxFractionDigits="12"
                          v-model="orgFormState.longitude"
                          :disabled="isModeration"
                        />
                      </div>
                    </div>
                    <div class="w-full h-full map-container">
                      <ymap-map
                        :coords="[orgFormState.latitude ?? 65, orgFormState.longitude ?? 90]"
                        :zoom="3"
                        class="map"
                        @click="updateCoords"
                      >
                        <ymap-placemark
                          v-if="orgFormState.latitude != null && orgFormState.longitude != null"
                          :marker-id="'marker'"
                          :coords="[orgFormState.latitude, orgFormState.longitude]"
                        />
                        <div v-else></div>
                      </ymap-map>
                    </div>
                  </accordion-tab>
                </accordion>
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
                      v-model="ov$.bankName.$model"
                      :disabled="isModeration"
                    />
                  </div>
                  <div class="col-4 field">
                    <label for="cor-acc" class="text-600">Корреспондентский счет</label>
                    <input-text
                      id="cor-acc"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      v-model="ov$.corrAccount.$model"
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
                      v-model="ov$.account.$model"
                      :disabled="isModeration"
                    />
                  </div>
                  <div class="col-4 field">
                    <label for="bik" class="text-600">БИК</label>
                    <input-text
                      id="bik"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      v-model="ov$.bik.$model"
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
                    <label for="org-phone" class="text-600">Контактный телефон</label>
                    <input-mask
                      id="org-phone"
                      mask="9 (999) 999-99-99"
                      class="w-full p-inputtext-sm rk-input"
                      v-model="ov$.phone.$model"
                      :disabled="isModeration"
                    />
                  </div>
                  <div class="col-8"></div>
                  <div class="col-4 field">
                    <label
                      for="org-email"
                      :class="{
                        'p-error': ov$.email.$invalid && submitted,
                        'text-600': !ov$.email.$invalid,
                      }"
                      >E-mail</label
                    >
                    <input-text
                      id="org-email"
                      type="text"
                      class="w-full p-inputtext-sm rk-input"
                      :class="{ 'p-invalid': ov$.email.$invalid && submitted }"
                      v-model="ov$.email.$model"
                      :disabled="isModeration"
                    />
                    <template v-if="ov$.email.$invalid && submitted">
                      <small class="p-error"> Невалидный E-mail</small>
                    </template>
                  </div>
                  <div class="col-8"></div>
                  <div class="col-4 field">
                    <label for="site" class="text-600">Сайт</label>
                    <input-text
                      id="site"
                      type="text"
                      class="w-full rk-input p-inputtext-sm rk-input"
                      v-model="ov$.site.$model"
                      :disabled="isModeration"
                    />
                  </div>
                  <div class="col-8"></div>
                </div>
              </div>
              <prime-divider class="mt-5 mb-5"></prime-divider>
              <div>
                <h2 class="mt-0">6. Вложения</h2>
                <div class="w-full h-full grid">
                  <file-upload
                    @select="applicationSelected"
                    :multiple="true"
                    accept=".doc,.docx,.xml,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    :maxFileSize="30000000"
                    :showUploadButton="false"
                    chooseLabel="Выбрать"
                    cancelLabel="Отмена"
                  >
                    <template #empty>
                      <p>Перетащите сюда файлы для загрузки.</p>
                    </template>
                  </file-upload>
                </div>
              </div>
            </div>
          </tab-panel>
        </tab-view>
        <prime-divider></prime-divider>
        <div class="w-full flex flex-row justify-content-end">
          <prime-button label="Сохранить" class="p-button-sm" @click="save" />
        </div>
      </template>
    </card>
  </app-page>
</template>

<script lang="ts">
import Toast from 'primevue/toast';
import { IOrganizationFullModel } from '@/app/organization-full/@types/IOrganizationFullModel';
import { organizationFullStore } from '@/app/organization-full/state/organization-full.store';
import { OrganizationFullModel } from '@/app/organization-full/models/organozation-full.model';
import { computed, defineComponent, reactive, Ref, ref, watch } from 'vue';
import { useBase64 } from '@vueuse/core';
import { v4 as uuidv4 } from 'uuid';
import { useToast } from 'primevue/usetoast';
import { useOrganizationStatuses } from '@/composables/organization-statuses.composable';
import { UserRoles, status as userStatus, isAuthenticated } from '@/store/user.store';
import { userService } from '@/services/user/user.service';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import useVuelidate from '@vuelidate/core';
import { email, minLength, numeric, required } from '@vuelidate/validators';
import { useRouter } from 'vue-router';
import { IOrganizationDocument } from '@/app/organization-full/@types/IOrganizationDocument';

interface ICreateUser {
  firstName: '';
  lastName: '';
  patronymic: '';
  phone: '';
  email: '';
  password: '';
  role: '';
}

export default defineComponent({
  components: { Toast },
  setup() {
    const router = useRouter();
    const userFormState = reactive<ICreateUser>({
      firstName: '',
      lastName: '',
      patronymic: '',
      phone: '',
      email: '',
      password: '',
      role: '',
    });
    const userFormRules = {
      firstName: { required },
      lastName: { required },
      patronymic: { required },
      email: { required, email },
      password: { required },
      role: { required },
      phone: {},
    };
    const uv$ = useVuelidate(userFormRules, userFormState);
    const roleOptions = computed(() => UserRoles.map((x) => x.name));
    const toast = useToast();
    const {
      createItem,
      updateSelectedItem,
      saveChanges,
      organizationSelected,
      status: orgStatus,
    } = organizationFullStore;
    const isModeration = computed(() => organizationSelected.value?.mode === 'moderate');
    const { statuses } = useOrganizationStatuses();
    const statusOptions = computed(() => statuses.value.map((x) => x.name));
    const orgFormState = reactive<IOrganizationFullModel>({
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
      phone: '',
      email: '',
      isProducer: false,
      isBuyer: false,
      bankName: '',
      account: '',
      corrAccount: '',
      bik: '',
    });
    const hasSelectedOrgType = computed(() => orgFormState.isProducer || orgFormState.isBuyer);
    const orgFormRules = {
      isProducer: { required: () => hasSelectedOrgType.value },
      isBuyer: { required: () => hasSelectedOrgType.value },
      ogrn: { required, numeric, minLength: minLength(13) },
      inn: { required, numeric, minLength: minLength(10) },
      kpp: { required, numeric, minLength: minLength(9) },
      fullName: { required, minLength: minLength(5) },
      name: { required, minLength: minLength(5) },
      region: { required, minLength: minLength(5) },
      city: { required },
      address: { required },
      email: { required, email },
      okved: {},
      okved2: {},
      factAddress: {},
      site: {},
      phone: {},
      bankName: {},
      account: {},
      corrAccount: {},
      bik: {},
    };
    const ov$ = useVuelidate(orgFormRules, orgFormState);
    const showErrorToast = (detail: string) => {
      toast.add({
        severity: 'error',
        group: 'tr',
        summary: 'Ошибка',
        detail,
        life: 4000,
      });
    };
    const showErrorFromStatusToast = (status: DataStatus, defaultTitle: string) => {
      const errors = status.payload;
      const firstErr = errors != null ? errors[0] : null;
      const title = firstErr != null ? firstErr[0] : null;
      const description = firstErr != null ? firstErr[1] : null;
      const detail = `${title != null ? title : defaultTitle}${
        description != null ? `: ${description[0].toLowerCase() + description.slice(1)}` : ''
      }`;
      showErrorToast(detail);
    };
    const saveUser = async () => {
      const userRole = UserRoles.find((x) => x.name === userFormState.role)?.value;
      if (userRole == null) {
        showErrorToast('Не выбрана роль');
        return false;
      }
      await userService.createUser({
        firstName: userFormState.firstName,
        lastName: userFormState.lastName,
        email: userFormState.email,
        username: userFormState.email,
        credentials: [
          {
            value: userFormState.password,
            type: 'password',
            temporary: false,
          },
        ],
        groups: [userRole],
        attributes: {
          patronymic: userFormState.patronymic,
        },
        enabled: true,
      });

      if (userStatus.value.status === 'loaded') {
        return true;
      }

      if (userStatus.value.status === 'error') {
        showErrorFromStatusToast(userStatus.value, 'Что-то случилось при добавлении пользователя');
      }

      return false;
    };
    const saveOrganization = async () => {
      createItem();
      const item = new OrganizationFullModel();
      item.fromResponse(orgFormState);
      updateSelectedItem(item);
      await saveChanges();

      if (orgStatus.value.status === 'loaded') {
        return true;
      }

      if (orgStatus.value.status === 'error') {
        showErrorFromStatusToast(orgStatus.value, 'Что-то случилось при добавлении организации');
      }

      return false;
    };
    const submitted = ref(false);
    const save = async () => {
      submitted.value = true;
      if (uv$.value.$invalid || ov$.value.$invalid) return;

      /**
       * нужно, если при сохранении организации произошла
       * непредвиденная ошибка на сервере, чтобы не создавать
       * второго пользователя
       */
      let userIsSaved = isAuthenticated.value;
      if (!isAuthenticated.value) {
        userIsSaved = await saveUser();
      }
      if (!userIsSaved) return;
      const orgIsSaved = await saveOrganization();
      if (!orgIsSaved) return;
      if (userIsSaved && orgIsSaved) {
        const id = organizationSelected.value?.data.id;
        if (id !== 0) router.push({ name: 'organization', params: { id } });
      }
    };

    const document = ref() as Ref<File>;
    const { base64: documentBase64 } = useBase64(document);
    watch(documentBase64, (b64) => {
      if (document.value == null || b64 == null) {
        return;
      }
      const doc = {
        data: b64.replace(/(^data:image\/[a-z]+;base64,)/gi, ''),
        fileId: uuidv4(),
        fileName: document.value.name,
      };
      orgFormState.document = doc;
    });
    const documents = ref() as Ref<File[]>;
    watch(documents, (docs) => {
      if (docs == null) return;
      const encodedDocs: IOrganizationDocument[] = [];
      docs.forEach((doc) => {
        const { base64 } = useBase64(doc);
        encodedDocs.push({
          data: base64.value.replace(/(^data:image\/[a-z]+;base64,)/gi, ''),
          fileId: uuidv4(),
          fileName: doc.name,
        });
      });
      orgFormState.documents = encodedDocs;
    });
    const applicationSelected = (e: { files: File[]; originalEvent: Event }) => {
      documents.value = e.files;
    };
    const onFileInput = (e: Event) => {
      const target = e.target as HTMLInputElement;
      const { files } = target;
      if (files == null) {
        return;
      }
      [document.value] = files;
    };
    const updateCoords = (e: { get?: (_: string) => [number, number] }) => {
      if (e.get == null) return;
      const [lat, long] = e.get('coords');
      orgFormState.latitude = lat;
      orgFormState.longitude = long;
    };
    return {
      uv$,
      ov$,
      hasSelectedOrgType,
      submitted,
      userFormState,
      roleOptions,
      statusOptions,
      isModeration,
      orgFormState,
      documentBase64,
      document,
      documents,
      save,
      onFileInput,
      updateCoords,
      applicationSelected,
    };
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

.map-container {
  :deep(.ymap-container) {
    height: 100%;
  }

  .map {
    height: 50vh;
  }
}
</style>
