<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page :title="pageTitle">
    <toast position="top-right" group="tr" />
    <card class="" id="main-card">
      <template #content>
        <tab-view ref="tabview1">
          <tab-panel header="Продукт">
            <loading-status-handler>
              <div class="grid p-fluid mt-2">
                <!-- строка 1 -->
                <div class="field col-12 md:col-3">
                  <span class="p-float-label">
                    <input-text id="productName" type="text" v-model="models.name.value" />
                    <label for="productName">Короткое наименование товара</label>
                  </span>
                </div>
                <div class="field col-12 md:col-9">
                  <span class="p-float-label">
                    <input-text id="fullName" type="text" v-model="models.fullName.value" />
                    <label for="fullName">Полное наименование товара</label>
                  </span>
                </div>
                <!-- строка 2 -->
                <!-- строка 3 -->
                <div class="field col-12 md:col-4">
                  <span class="p-float-label">
                    <dropdown
                      id="categoryId"
                      v-model="models.categoryId.value"
                      :options="sectionsCollection"
                      optionLabel="name"
                      optionValue="id"
                    />
                    <label for="categoryId">Категория</label>
                  </span>
                </div>
                <div class="field col-12 md:col-4">
                  <span class="p-float-label">
                    <input-text id="codeTnVed" type="text" v-model="models.codeTnVed.value" />
                    <label for="codeTnVed">Код товара</label>
                  </span>
                </div>
                <div class="field col-12 md:col-4">
                  <span class="p-float-label p-input-icon-right">
                    <i class="pi pi-money-bill" />
                    <input-number
                      id="price"
                      mode="decimal"
                      :minFractionDigits="2"
                      :maxFractionDigits="10"
                      v-model="models.price.value"
                    />
                    <label for="price">Цена, руб</label>
                  </span>
                </div>
                <!-- строка 4 -->
                <div class="field col-12 md:col-4">
                  <span class="p-float-label">
                    <input-text id="measuring" type="text" v-model="models.measuringUnit.value" />
                    <label for="measuring">Единицы измерения</label>
                  </span>
                </div>
                <div class="field col-12 md:col-4">
                  <span class="p-float-label">
                    <input-text id="country" type="text" v-model="models.country.value" />
                    <label for="country">Страна производства</label>
                  </span>
                </div>
                <div class="field col-12 md:col-4">
                  <label class="font mb-1">Изображение продукта</label>
                  <div>
                    <file-upload
                      mode="basic"
                      id="product-img"
                      accept="image/*"
                      :maxFileSize="1000000"
                      @input="onFileInput"
                      :auto="true"
                      :customUpload="true"
                      chooseLabel="Выбрать"
                    />
                  </div>
                  <div class="mt-2 grid">
                    <div
                      class="col-12 md:col-6"
                      v-for="doc in models.documents.value"
                      :key="doc.fileId"
                    >
                      <product-document-display :document="doc"></product-document-display>
                    </div>
                  </div>
                </div>
                <!-- строка 5 -->
                <div class="field col-12">
                  <span class="p-float-label">
                    <prime-textarea
                      id="productDescr"
                      auto-resize
                      type="text"
                      v-model="models.description.value"
                    />
                    <label for="productDescr">Описание товара</label>
                  </span>
                </div>
                <!-- строка 5 -->
                <div class="field col-12">
                  <prime-button
                    label="Сохранить"
                    @click="createProduct"
                    v-if="isNew"
                  ></prime-button>
                  <!-- <prime-button label="Сохранить" @click="saveChanges" v-if="!isNew"></prime-button> -->
                </div>
              </div>
            </loading-status-handler>
          </tab-panel>
          <tab-panel header="Технические характеристики" v-if="!isNew">
            <ul>
              <li v-for="attr in attributesCollection" :key="attr.attributeId">
                <div class="grid p-fluid mt-2">
                  <!-- строка 1 -->
                  <div class="field col-12 md:col-3">
                    <span class="p-float-label">
                      <dropdown
                        id="attributeId"
                        v-model="attr.attributeId"
                        :options="attributeDefs"
                        optionLabel="name"
                        optionValue="id"
                      />
                      <label for="attributeId">Название характеристики</label>
                    </span>
                  </div>
                  <div class="field col-12 md:col-9">
                    <span class="p-float-label">
                      <input-text id="attr-value" type="text" v-model="attr.value" />
                      <label for="attr-value">Значение характеристики</label>
                    </span>
                  </div>
                </div>
              </li>
            </ul>
            <div class="flex card-container blue-container overflow-hidden">
              <div class="flex-none flex">
                <span class="text-xl"></span>
              </div>
              <div class="flex-grow-1 flex"></div>
              <div class="flex-none flex">
                <prime-button
                  icon="pi pi-plus"
                  class="p-button-sm py-1 px-1 ml-1"
                  @click="addAttribute()"
                >
                </prime-button>
              </div>
            </div>
            <div class="field col-12">
              <prime-button label="Сохранить" @click="saveAttributes"></prime-button>
            </div>
          </tab-panel>
        </tab-view>
      </template>
    </card>
  </app-page>
</template>

<script lang="ts">
import { ProductFullModel } from '@/app/product-full/models/product-full.model';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { PrimeTextarea } from '@/tools/prime-vue-components';
import { useBase64 } from '@vueuse/core';
import { defineComponent, computed, watch, ref, Ref, WritableComputedRef, onMounted } from 'vue';
import { v4 as uuidv4 } from 'uuid';
import { productFullService } from '@/app/product-full/infrastructure/product-full.http-service';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { attributeStore } from '@/app/attributes/state/attribute.store';
import { AttributeModel } from '@/app/attributes/models/AttributeModel';
import { http } from '@/app/core/services/http/axios/axios.service';
import { useToast } from 'primevue/usetoast';
import Toast from 'primevue/toast';
import { useSections } from '@/composables/sections.composable';
import { CollectionStoreMixed } from '../base/presentational/state/collection/collection-state.vue';
import { loadingStatusProvider } from '../base/presentational/state/collection/providers/loading-status.provider';

export default defineComponent({
  components: { PrimeTextarea, Toast },
  setup() {
    const toast = useToast();
    if (productFullStore.itemSelected == null) {
      throw new Error('Что-то не так с состоянием редактируемого товара');
    }

    const attributes = attributeStore as CollectionStoreMixed;
    if (attributes.items == null) {
      throw new Error('Что-то пошло не так');
    }
    const attributeDefs = attributes.items({ force: true }) as WritableComputedRef<
      AttributeModel[]
    >;
    loadingStatusProvider.provideFrom(() => productFullStore.status);
    const { list: sectionsCollection } = useSections();

    const mode = computed({
      get: () => productFullStore.itemSelected?.value?.mode,
      set: (val) => {
        if (productFullStore.itemSelected?.value == null || val == null) {
          return;
        }
        productFullStore.itemSelected.value = {
          data: productFullStore.itemSelected.value.data,
          mode: val,
        };
      },
    });

    const attributesCollection = ref<{ attributeId: number | undefined; value: string }[]>();

    onMounted(() => {
      attributesCollection.value =
        productFullStore.itemSelected?.value?.data?.attributeValues.map((x) => ({
          attributeId: x.attributeId,
          value: x.value,
        })) ?? [];
    });

    const selectedData = computed({
      get: () => productFullStore.itemSelected?.value?.data ?? null,
      set: (val) => {
        if (val == null || productFullStore.itemSelected?.value == null) {
          return;
        }
        productFullStore.itemSelected.value = {
          data: val,
          mode: productFullStore.itemSelected.value.mode,
        };
      },
    });

    const pageTitle = computed(() =>
      mode.value === 'create' ? 'Добавление товара' : 'Редактирование товара',
    );

    const isNew = computed(() => mode.value === 'create');

    const getModelFor = <TKey extends keyof ProductFullModel & string>(
      key: TKey,
      fallback: ProductFullModel[TKey],
    ) =>
      computed({
        get: () => (selectedData.value == null ? fallback : selectedData.value[key]),
        set: (val) => {
          if (selectedData.value == null || val == null) {
            return;
          }
          const cloned = selectedData.value.clone();
          cloned.setKey(key, val);
          selectedData.value = cloned;
        },
      });

    const file = ref() as Ref<File>;

    const { base64: fileB64 } = useBase64(file);

    watch(fileB64, (b64) => {
      if (file.value == null || b64 == null) {
        return;
      }
      const docs = [
        {
          data: b64,
          fileId: uuidv4(),
          fileName: file.value.name,
        },
      ];
      if (selectedData.value == null) return;
      const cloned = selectedData.value.clone();
      cloned.setKey('documents', docs);
      selectedData.value = cloned;
    });

    const onFileInput = (e: Event) => {
      const target = e.target as HTMLInputElement;
      const { files } = target;
      if (files == null) {
        return;
      }
      [file.value] = files;
    };

    const createProduct = async () => {
      if (productFullStore.itemSelected?.value == null) return; // || productFullStore.saveChanges == null
      // await productFullStore.saveChanges();
      // mode.value = 'edit';
      const request = productFullStore.itemSelected?.value.data.toRequest();
      const result = await productFullService.post(request);
      if (result.status === HttpStatus.Success) {
        mode.value = 'edit';
        if (selectedData.value == null) {
          return;
        }
        const cloned = selectedData.value.clone();
        cloned.setKey('id', result.data);
        selectedData.value = cloned;
      }
    };

    const saveChanges = async () => {
      if (productFullStore.itemSelected?.value == null) return;
      const request = productFullStore.itemSelected?.value.data.toRequest();
      await productFullService.put(request);
    };

    const models = {
      name: getModelFor('name', ''),
      fullName: getModelFor('fullName', ''),
      description: getModelFor('description', ''),
      codeTnVed: getModelFor('codeTnVed', ''),
      price: getModelFor('price', 0),
      categoryId: getModelFor('catalogSectionId', 0),
      measuringUnit: getModelFor('measuringUnit', ''),
      country: getModelFor('country', ''),
      documents: getModelFor('documents', []),
      attributes: getModelFor('attributeValues', []),
    };

    const addAttribute = () => {
      if (attributesCollection.value == null) return;
      attributesCollection.value.push({
        attributeId: undefined,
        value: '',
      });
    };

    const saveAttributes = () => {
      const valid = attributesCollection.value?.every(
        (x) => x.attributeId != null && x.value != null && x.value.trim() !== '',
      );
      if (!valid) {
        toast.add({
          severity: 'error',
          group: 'tr',
          detail: `Заполнены не все данные`,
          life: 3000,
        });
        return;
      }
      const productId = selectedData.value?.id;
      if (productId == null && selectedData.value == null) return;
      http
        .put(
          `api/products/${productId}/attributes`,
          attributesCollection.value?.map((c) => ({
            attributeId: c.attributeId,
            value: c.value,
          })),
        )
        .then((response) => {
          if (response.status === 200) {
            toast.add({
              severity: 'success',
              group: 'tr',
              detail: `Технические характеристики сохранены`,
              life: 2000,
            });
            if (selectedData.value == null) return;
            const cloned = selectedData.value.clone();
            cloned?.setKey(
              'attributeValues',
              attributesCollection.value?.map((x) => ({
                baseProductId: productId,
                attributeId: x.attributeId,
                value: x.value,
              })),
            );
            selectedData.value = cloned;
          }
        });
    };

    return {
      getModelFor,
      createProduct,
      onFileInput,
      saveChanges,
      addAttribute,
      saveAttributes,
      attributesCollection,
      pageTitle,
      models,
      sectionsCollection,
      isNew,
      attributeDefs,
    };
  },
});
</script>

<style scoped>
.p-float-label {
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif,
    'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';
}

.font {
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif,
    'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';
}

#main-card :deep(.p-card-content) {
  padding-top: 0;
}

ul {
  list-style-type: none;
}
</style>
