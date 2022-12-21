<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page :title="pageTitle">
    <toast position="top-right" group="tr" />
    <prime-card no-padding shadow-hover="none">
      <div class="grid m-2">
        <div class="col-12 md:col-7">
          <prime-card shadow-hover="none">
            <template #title>
              <app-text mode="subheader">1. Основные данные</app-text>
            </template>
            <div class="p-fluid grid mt-4">
              <prime-input-text
                class="col-12 md:col-6"
                id="--prodct-short-name"
                v-model="models.name.value"
                label="Сокращённое наименование"
              />
              <prime-input-text
                id="--prodct-full-name"
                class="col-12 md:col-6"
                v-model="models.fullName.value"
                label="Полное наименование"
              />
              <prime-input-tree-select
                :options="sectionsTree"
                v-model="categoryId"
                class="col-12 md:col-6"
                label="Категория"
              />
              <prime-input-textarea
                class="col-12"
                label="Описание"
                v-model="models.description.value"
              />
            </div>
          </prime-card>
        </div>
        <div class="col-12 md:col-5">
          <product-documents-editor-container v-model="models.documents.value" />
        </div>
        <div v-if="productionType === 'product'" class="col-12">
          <div class="p-fluid grid mt-2">
            <prime-input-text
              id="--prodct-articule"
              class="col-12 md:col-4"
              v-model="models.article.value"
              label="Артикул"
            />
            <prime-input-text
              id="--prodct-code-okpd"
              class="col-12 md:col-4"
              v-model="models.codeOkpd2.value"
              label="Код ОКПД 2"
            />
            <prime-input-text
              id="--prodct-code"
              class="col-12 md:col-4"
              v-model="models.codeTnVed.value"
              label="Код товара"
            />
            <prime-input-text
              id="--prodct-adress"
              class="col-12 md:col-8"
              v-model="models.address.value"
              label="Адрес"
            />
          </div>
        </div>
        <template v-if="productionType === 'product'">
          <prime-divider></prime-divider>
          <div class="col-12">
            <prime-card shadow-hover="none">
              <template #title>
                <app-text mode="subheader">2. Характеристики</app-text>
              </template>
              <attributes-editor-container class="mt-4" v-model="models.attributes.value" />
            </prime-card>
          </div>
        </template>
        <prime-divider></prime-divider>
        <div class="col-12">
          <prime-card shadow-hover="none">
            <template #title>
              <app-text mode="subheader">3. Цена</app-text>
            </template>
            <div class="flex flex-row gap-3 mt-5">
              <prime-input-number
                :disabled="isPriceEmpty"
                label="Цена"
                v-model="models.price.value"
                id="--product-price"
              ></prime-input-number>
              <div class="field-checkbox">
                <checkbox inputId="binary" v-model="isPriceEmpty" :binary="true" />
                <label for="binary">Договорная</label>
              </div>
            </div>
          </prime-card>
        </div>
        <prime-divider></prime-divider>
        <div class="col-12 pb-5">
          <div class="flex flex-row justify-content-end">
            <prime-button label="Сохранить" @click="saveProduct"></prime-button>
          </div>
        </div>
      </div>
    </prime-card>
  </app-page>
</template>

<script lang="ts">
import { ProductFullModel } from '@/app/product-full/models/product-full.model';
import { productFullStore, ProductType } from '@/app/product-full/state/product-full.store';
import { useBase64 } from '@vueuse/core';
import { defineComponent, computed, watch, ref, Ref, onMounted, PropType } from 'vue';
import { v4 as uuidv4 } from 'uuid';
import { productFullHttpService } from '@/app/product-full/infrastructure/product-full.http-service';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { attributeStore } from '@/app/attributes/state/attribute.store';
import { http } from '@/app/core/services/http/axios/axios.service';
import { useToast } from 'primevue/usetoast';
import Toast from 'primevue/toast';
import { useSections } from '@/composables/sections.composable';
import { useRoute, useRouter } from 'vue-router';
import { DataMode } from '@/app/core/services/harlem/tools/not-valid-data';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { loadingStatusProvider } from '../base/presentational/state/collection/providers/loading-status.provider';

export default defineComponent({
  components: { Toast },
  props: {
    productionType: {
      type: String as PropType<ProductType>,
      required: true,
    },
    dataMode: {
      type: String as PropType<DataMode>,
      required: true,
    },
  },
  setup(props) {
    const toast = useToast();
    const route = useRoute();
    const router = useRouter();

    watch(
      () => route.params.id as string | undefined,
      (id) => {
        if (props.dataMode !== 'edit' || id == null) {
          return;
        }
        productFullStore.getAsync({ id: +id, type: props.productionType }).then(() => {
          productFullStore.startEditing();
        });
      },
      {
        immediate: true,
      },
    );

    watch(
      () => props.dataMode,
      (mst) => {
        if (mst === 'create') {
          productFullStore.startCreation();
          productFullStore.status.value = new DataStatus('loaded');
        }
      },
      {
        immediate: true,
      },
    );

    const attributes = attributeStore;

    const attributeDefs = attributes.items;
    loadingStatusProvider.provideFrom(() => productFullStore.status);
    const { list: sectionsCollection, tree: sectionsTree } = useSections();

    const mode = computed({
      get: () => productFullStore.selected?.value?.mode,
      set: (val) => {
        if (productFullStore.selected?.value == null || val == null) {
          return;
        }
        productFullStore.selected.value = {
          data: productFullStore.selected.value.data,
          mode: val,
        };
      },
    });

    const attributesCollection = ref<{ attributeId: number | undefined; value: string }[]>();

    onMounted(() => {
      attributesCollection.value =
        productFullStore.selected?.value?.data?.attributeValues.map((x) => ({
          attributeId: x.attributeId,
          value: x.value,
        })) ?? [];
    });

    const selectedData = computed({
      get: () => productFullStore.selected?.value?.data ?? null,
      set: (val) => {
        if (val == null || productFullStore.selected?.value == null) {
          return;
        }
        productFullStore.selected.value = {
          data: val,
          mode: productFullStore.selected.value.mode,
        };
      },
    });

    const pageTitle = computed(() => {
      let whatToDo = '';
      let withWhat = '';
      switch (props.dataMode) {
        case 'create':
          whatToDo = 'Создание';
          break;
        case 'edit':
          whatToDo = 'Редактирование';
          break;
        case 'moderate':
          whatToDo = 'Модерирование';
          break;
        default:
          throw new Error(`Неизвестный тип действий - ${props.dataMode}`);
      }
      switch (props.productionType) {
        case 'product':
          withWhat = 'товара';
          break;
        case 'service':
          withWhat = 'услуги';
          break;
        case 'work':
          withWhat = 'работы';
          break;
        default:
          throw new Error('Неизвестный тип товара');
      }
      return `${whatToDo} ${withWhat}`;
    });

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
      if (productFullStore.selected?.value == null) return;
      const request = productFullStore.selected?.value.data.toRequest();
      const result = await productFullHttpService.post(request);
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
      if (productFullStore.selected?.value == null) return;
      const request = productFullStore.selected?.value.data.toRequest();
      await productFullHttpService.put(request);
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
      article: getModelFor('article', ''),
      address: getModelFor('address', ''),
      codeOkpd2: getModelFor('codeOkpd2', ''),
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

    const categoryId = computed({
      get: () =>
        models.categoryId.value == null || models.categoryId.value <= 0
          ? undefined
          : { [models.categoryId.value]: true },
      set: (val) => {
        if (models.categoryId.value == null || val == null) {
          return;
        }
        const key = Object.keys(val).find((k) => val[+k] === true);
        if (key != null) {
          models.categoryId.value = +key;
        }
      },
    });

    const isPriceEmpty = computed({
      get: () => (selectedData.value?.price ?? null) == null,
      set: (empty) => {
        if (empty && selectedData.value != null) {
          const cloned = selectedData.value.clone();
          cloned.price = null;
          selectedData.value = cloned;
        }
      },
    });

    // watch(isPriceEmpty, (empty) => {
    //   if (empty && selectedData.value != null) {
    //     const cloned = selectedData.value.clone();
    //     cloned.price = null;
    //     selectedData.value = cloned;
    //   }
    // });

    const saveProduct = () => {
      productFullStore.saveChanges(props.productionType).then(() => {
        router.push({
          name: 'org-products',
        });
      });
    };

    return {
      status: productFullStore.status,
      mode,
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
      sectionsTree,
      isNew,
      categoryId,
      attributeDefs,
      isPriceEmpty,
      saveProduct,
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
