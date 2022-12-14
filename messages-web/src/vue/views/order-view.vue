<template>
  <app-page title="Карточка заказа">
    <div class="flex gap-2 flex-column">
      <card>
        <template #title>
          <div class="flex flex-row justify-content-between">
            <div class="flex flex-row" style="align-items: baseline">
              <span class="p-component text-lg font-bold">Заказ № {{ order?.id }}</span>
              <tag class="ml-2" value="Primary" rounded style="height: 24px">завершен</tag>
              <prime-button
                style="transform: scale(0.7)"
                class="p-button-raised p-button-secondary p-button-text"
                label="История заказа"
              ></prime-button>
            </div>
            <prime-button
              style="transform: scale(0.7)"
              class="p-button-rounded p-button-text p-button-secondary"
              @click="expanded = !expanded"
              :icon="expanded ? 'pi pi-chevron-up' : 'pi pi-chevron-down'"
            ></prime-button>
          </div>
        </template>
        <template #content>
          <div class="grid">
            <div class="col-6 flex flex-column gap-3">
              <div class="p-component text-sm">
                Дата оформления:
                <span> {{ order?.created }}</span>
              </div>
              <div class="p-component text-md">
                Заказчик:
                <span class="text-primary">
                  {{ order?.userName || 'Неизвестный пользователь' }}</span
                >
              </div>
              <div class="p-component text-md">
                Производитель:
                <span class="text-primary">
                  {{ order?.organisationName || 'Неизвестный пользователь' }}</span
                >
              </div>
            </div>
            <template v-if="!expanded">
              <div class="col-3">
                <div class="p-component text-md">Количество: {{ order?.quantity }}</div>
              </div>
              <div class="col-3">
                <div class="p-component text-md">Сумма заказа: {{ order?.sum }} ₽</div>
              </div>
            </template>
            <template v-else>
              <div class="col-6"></div>
            </template>
            <div class="col-3"></div>
            <div class="w-full mt-2 px-2" v-if="expanded">
              <div class="flex mt-3 mx-1 flex-row justify-content-start">
                <div class="p-component text-lg font-bold">
                  Количество товаров: {{ order?.quantity }}
                </div>
              </div>
              <prime-divider class="mt-1 mb-2"></prime-divider>
              <div
                v-for="product in order?.orderItems"
                :key="product.productId"
                class="grid px-1 mt-1 border-round surface-200"
              >
                <div class="col-2">
                  <file-store-image fit-width :id="product.documentId"></file-store-image>
                </div>
                <div class="col-4 flex flex-column gap-3">
                  <router-link
                    :to="{ name: 'product', params: { id: product.productId } }"
                    class="flex gap-3 align-items-center not-link text-primary"
                  >
                    {{ product.productName }}</router-link
                  >
                </div>
                <div class="col-2 flex flex-row justify-content-end">
                  <div class="p-component text-md">Цена: {{ product.price }} ₽</div>
                </div>
                <div class="col-4 flex flex-row justify-content-end">
                  <div class="p-component text-md">Количество: {{ product.quantity }}</div>
                </div>
              </div>
              <div class="flex mt-3 mx-1 flex-row justify-content-end">
                <div class="p-component text-lg font-bold">Итого: {{ order?.sum }} ₽</div>
              </div>
            </div>
          </div>
        </template>
      </card>
    </div>
  </app-page>
</template>

<script lang="ts">
import { getOrder } from '@/app/orders/infrastructure/order.http-service';
import { defineComponent, ref, watch } from 'vue'; // , toRaw
import { useRoute } from 'vue-router';

interface IOrder {
  createdBy: string;
  lastModifiedBy: string;
  created: string;
  lastModified: string;
  id: number;
  organisationName: string;
  userName: string;
  comments: string;
  quantity: number;
  orderItems: [
    {
      productId: number;
      productName: string;
      documentId: string;
      price: number;
      quantity: number;
      sum: number;
    },
  ];
  sum: number;
}

export default defineComponent({
  setup() {
    const route = useRoute();
    const expanded = ref<boolean>(true);
    const order = ref<IOrder>();

    const getTotalQuantity = (
      data: {
        productId: number;
        productName: string;
        documentId: string;
        price: number;
        quantity: number;
        sum: number;
      }[],
    ) => {
      let sum = 0;
      data.forEach((x) => {
        sum += x.quantity;
      });
      return sum;
    };

    watch(
      () => route.params.id as string | undefined,
      (id) => {
        if (id == null || id === '' || id.trim() === '') {
          return;
        }
        const idNum = Number.parseInt(id, 10);
        if (Number.isNaN(idNum)) return;
        getOrder(idNum).then((o) => {
          if (o == null || o.data == null) return;
          const model = {
            id: o.data.id,
            createdBy: o.data.createdBy,
            lastModifiedBy: o.data?.lastModifiedBy,
            created: new Date(o.data.created).toLocaleDateString(),
            lastModified: new Date(o.data.lastModified).toLocaleDateString(),
            organisationName: o.data?.organisationName,
            userName: o.data?.userName,
            comments: o.data?.comments,
            quantity: getTotalQuantity(o.data?.orderItems),
            orderItems: o.data?.orderItems,
            sum: o.data.sum,
          } as IOrder;
          order.value = model;
        });
      },
      {
        immediate: true,
      },
    );

    return { order, expanded };
  },
});
</script>

<style lang="scss" scoped>
:deep(.p-card-title) {
  height: 28px;
}

.not-link {
  text-decoration: none;
  /* color: black; */
}

a,
a:visited,
a:hover,
a:active {
  color: inherit;
}
</style>
