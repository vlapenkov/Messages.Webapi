<template>
  <app-page hide-title :title="item.name">
    <toast position="top-right" group="tr" />
    <div class="flex mt-5 flex-column gap-3">
      <prime-card transparent shadow-hover="none">
        <div class="grid">
          <div class="col-3">
            <file-store-image
              :id="item?.documents[0]?.fileId"
              :maxHeight="300"
              :fitWidth="true"
            ></file-store-image>
          </div>
          <div class="col-9 flex flex-column gap-2">
            <app-text mode="header"> {{ item.name }}</app-text>
            <app-text mode="weak"> {{ item.article || '123456' }}</app-text>
            <div
              class="flex flex-row gap-1 align-content-center text-md"
              :style="{ opacity: (item.rating ?? 0) > 0 ? 1 : 0 }"
            >
              <i class="star-filled star-yellow"></i>
              <span>
                {{ item.rating ?? 0 }}
              </span>
            </div>
            <div class="flex flex-row mt-2">
              <div>
                <prime-button
                  label="Добавить в избранное"
                  icon="pi pi-heart"
                  class="p-button-text py-1 button-light"
                />
              </div>
              <div>
                <prime-button
                  label="Добавить к сравнению"
                  icon="pi pi-chart-bar"
                  class="p-button-text py-1 button-light"
                />
              </div>
              <div>
                <prime-button
                  label="Искать аналоги"
                  icon="pi pi-arrows-h"
                  class="p-button-text py-1 button-light"
                />
              </div>
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
                <app-text mode="header-strong"> {{ item.price }} ₽ </app-text>
              </div>
              <prime-button
                @click="addToCart(item.id, item.name)"
                class="p-button-sm mt-1"
                style="width: 221px; height: 44px"
                label="В корзину"
              >
              </prime-button>
            </div>
            <div></div>
          </div>
        </div>
      </prime-card>
      <prime-card transparent shadow-hover="none">
        <tab-view ref="tabview1">
          <tab-panel header="Описание">
            <data-table class="no-head p-datatable-sm" :value="tableRows">
              <column field="name"></column>
              <column field="value"></column>
            </data-table>
          </tab-panel>
          <tab-panel header="Технические характеристики">
            <product-attributes :product="item"> </product-attributes>
          </tab-panel>
        </tab-view>
      </prime-card>
    </div>
  </app-page>
</template>

<script lang="ts">
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { useSections } from '@/composables/sections.composable';
import { isNullOrEmpty } from '@/tools/string-tools';
import Toast from 'primevue/toast';
import { useToast } from 'primevue/usetoast';
import { computed, defineComponent, watch } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  components: { Toast },
  setup() {
    const route = useRoute();
    const toast = useToast();
    const item = productFullStore.product;
    watch(
      () => route.params.id as string | undefined,
      (id) => {
        if (id == null || id === '' || id.trim() === '') {
          return;
        }
        productFullStore.getAsync(+id);
      },
      {
        immediate: true,
      },
    );

    const addProductToShopingCart = async (id: number, name: string) => {
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

    return { item, addToCart: addProductToShopingCart, actualizationDate, tableRows };
  },
});
</script>

<style lang="scss" scoped>
.button-light :deep(.p-button-label) {
  font-weight: 400;
  width: 170px;
  text-align: left;
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
