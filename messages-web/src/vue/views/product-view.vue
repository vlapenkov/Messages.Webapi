<template>
  <app-page hide-title :title="item.name">
    <toast position="top-right" group="tr" />
    <div class="flex mt-5 flex-column gap-3">
      <prime-card transparent shadow-hover="none">
        <div class="grid">
          <div class="col-4">
            <file-store-image
              :id="item?.documents[0]?.fileId"
              :maxHeight="300"
              :fitWidth="true"
            ></file-store-image>
          </div>
          <div class="col-8 flex flex-column gap-2">
            <app-text mode="header"> {{ item.name }}</app-text>
            <app-text
              :class="{ 'opacity-0': item.article == null || item.article === '' }"
              mode="weak"
            >
              {{ item.article || '123456' }}</app-text
            >
            <div
              v-if="(item.rating ?? 0) > 0"
              class="flex flex-row gap-1 align-content-center text-md"
            >
              <i class="star-filled star-yellow"></i>
              <span>
                {{ item.rating ?? 0 }}
              </span>
            </div>
            <div class="flex flex-row gap-1 mt-2">
              <div>
                <prime-button-weak disabled small>
                  <div class="flex flex-row gap-2 align-items-end">
                    <i class="pi pi-heart text-primary"> </i>
                    <span>Добавить в избранное</span>
                  </div>
                </prime-button-weak>
              </div>
              <div>
                <prime-button-weak disabled small>
                  <div class="flex flex-row align-items-end gap-2">
                    <i class="pi pi-arrows-h text-primary"> </i>
                    <span>Добавить к сравнению</span>
                  </div>
                </prime-button-weak>
              </div>
              <div>
                <prime-button-weak disabled small>
                  <div class="flex flex-row align-items-end gap-2">
                    <i class="pi pi-chart-bar text-primary"> </i>
                    <span>Искать аналоги</span>
                  </div>
                </prime-button-weak>
              </div>
            </div>
            <div v-if="productTypeText">
              <tag class="tag-secondary lowercase p-2" rounded>
                <div class="text-sm font-normal">{{ productTypeText }}</div>
              </tag>
            </div>
            <div class="flex flex-row gap-2">
              <app-text style="line-height: 19px">Производитель:</app-text>
              <router-link
                style="line-height: 19px"
                class="no-underline"
                :to="{ name: 'organization', params: { id: item.organization.id } }"
              >
                <app-text mode="primary">{{ item.organization.name }}</app-text>
              </router-link>
            </div>
            <div class="flex flex-row gap-2">
              <app-text style="line-height: 19px">Регион:</app-text>
              <app-text style="line-height: 19px">{{ item.organization.region }}</app-text>
            </div>
            <div class="flex flex-row gap-2">
              <app-text style="line-height: 19px; color: #00ba88">{{
                item.availableStatusText
              }}</app-text>
            </div>
            <div class="flex flex-row gap-2">
              <app-text style="line-height: 19px" mode="weak">
                Дата актуализации: {{ actualizationDate }}
              </app-text>
            </div>

            <div class="mt-4">
              <div>
                <app-text v-if="item.price && item.price > 0" mode="header-strong">
                  {{ item.price }} ₽
                </app-text>
                <app-text v-else mode="header-strong"> Цена договорная </app-text>
              </div>
              <prime-button
                :disabled="isInCart"
                @click="addToCart(item.id, item.name)"
                class="p-button-sm mt-3"
                style="width: 221px; height: 44px"
                :label="isInCart ? 'В корзине' : 'В корзину'"
              >
              </prime-button>
            </div>
            <div></div>
          </div>
        </div>
      </prime-card>
      <prime-card transparent shadow-hover="none">
        <tab-view class="no-background-table" ref="tabview1">
          <tab-panel header="Описание">
            <data-table class="no-head p-datatable-sm" :value="tableRows">
              <column field="name"></column>
              <column field="value"></column>
            </data-table>
          </tab-panel>
          <tab-panel v-if="productionType === 'product'" header="Технические характеристики">
            <product-attributes :product="item"> </product-attributes>
          </tab-panel>
          <tab-panel v-if="productionType === 'product'" disabled header="Аналоги">
            <div class="grid">
              <div class="col-4" v-for="i in 10" :key="i">
                <skeleton height="150px"></skeleton>
              </div>
            </div>
          </tab-panel>
        </tab-view>
      </prime-card>
    </div>
  </app-page>
</template>

<script lang="ts">
import { productFullStore, ProductType } from '@/app/product-full/state/product-full.store';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { useIsInCart } from '@/composables/is-in-cart.composable';
import { useSections } from '@/composables/sections.composable';
import { showRegisterDialog } from '@/store/register.store';
import { isAuthenticated } from '@/store/user.store';
import { isNullOrEmpty } from '@/tools/string-tools';
import Toast from 'primevue/toast';
import { useToast } from 'primevue/usetoast';
import { computed, defineComponent, PropType, watch } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  components: { Toast },
  props: {
    productionType: {
      type: String as PropType<ProductType>,
      required: true,
    },
  },
  setup(props) {
    const route = useRoute();
    const toast = useToast();
    const item = productFullStore.product;
    watch(
      () => route.params.id as string | undefined,
      (id) => {
        if (id == null || id === '' || id.trim() === '') {
          return;
        }
        productFullStore.getAsync({ id: +id, type: props.productionType });
      },
      {
        immediate: true,
      },
    );

    const isInCart = useIsInCart(item.value.id);

    const addProductToShopingCart = async (id: number, name: string) => {
      if (!isAuthenticated.value) {
        showRegisterDialog.value = true;
        return;
      }
      await shoppingCartStore.addToCart({
        productId: id,
        quantity: 1,
      });

      toast.add({
        severity: 'success',
        group: 'tr',
        detail: `${name} был успешно добавлен в корзину`,
        life: 3000,
      });
    };

    const actualizationDate = computed(() => {
      let result = '';
      if (item.value.lastModified != null) {
        const d = new Date(item.value.lastModified);
        result = d.toLocaleDateString();
      }
      return result;
    });

    const { list: sectionsList } = useSections();

    const tableRows = computed<{ name: string; value: string }[]>(() => [
      {
        name: 'Код по ОКПД 2',
        value: item.value.codeOkpd2,
      },
      {
        name: 'Код ТН ВЭД',
        value: item.value.codeTnVed,
      },
      {
        name: 'Полное наименование',
        value: item.value.fullName,
      },
      {
        name: 'Сокращённое наименование',
        value: item.value.name,
      },
      {
        name: 'Единицы измерения',
        value: item.value.measuringUnit,
      },
      {
        name: 'Отрасли применения',
        value:
          sectionsList.value?.find((x) => x.id === item.value?.catalogSectionId)?.name ??
          'Не указаны',
      },
      {
        name: 'Страна происхождения',
        value: item.value.country,
      },
      {
        name: 'Организация производства',
        value: item.value.organization.name,
      },
      {
        name: 'Адрес производства',
        value: isNullOrEmpty(item.value.address) ? 'Не указан' : item.value.address,
      },
      {
        name: 'Описание',
        value: item.value.description,
      },
    ]);

    const productTypeText = computed(() => {
      switch (props.productionType) {
        case 'product':
          return 'Товар';
        case 'work':
          return 'Работа';
        case 'service':
          return 'Услуга';
        default:
          return null;
      }
    });

    return {
      item,
      addToCart: addProductToShopingCart,
      actualizationDate,
      tableRows,
      productTypeText,
      isAuthenticated,
      isInCart,
    };
  },
});
</script>

<style lang="scss" scoped>
.button-light :deep(.p-button-label) {
  font-weight: 400;
  width: 170px;
  text-align: left;
}

.tag-secondary {
  background-color: rgba(180, 187, 186, 0.39);
}

.tag-height {
  min-height: 24px;
}

.no-head {
  :deep(.p-datatable-thead) {
    display: none;
  }
}

a,
a:visited,
a:hover,
a:active {
  color: inherit;
}
</style>
