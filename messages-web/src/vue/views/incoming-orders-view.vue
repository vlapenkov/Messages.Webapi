<template>
  <app-page title="Заказы">
    <div class="flex gap-2 flex-column">
      <card v-for="item in pageItems" :key="item.id">
        <template #title>
          <div class="flex flex-row justify-content-between">
            <div class="flex flex-row" style="align-items: baseline">
              <router-link :to="{ name: 'order', params: { id: item.id } }"
                class="p-component text-primary text-lg font-bold not-link">Заказ № {{ item.id }}</router-link>

              <tag class="ml-2" :severity="getSeverity(item.statusText)" style="height: 24px">
                {{ item.statusText }}</tag>
              <dropdown class="ml-3" v-model="item.statusText" :options="getStatus(item.statusText)" optionLabel="value"
                v-if="getStatus(item.statusText) != null" @change="changeStatus($event, item.id)" optionValue="value" />
            </div>
            <prime-button style="transform: scale(0.7)" class="p-button-rounded p-button-text p-button-secondary"
              @click="item.expanded.value = !item.expanded.value"
              :icon="item.expanded.value ? 'pi pi-chevron-up' : 'pi pi-chevron-down'"></prime-button>
          </div>
        </template>
        <template #content>
          <div class="grid">
            <div class="col-6 flex flex-column gap-3">
              <div class="p-component text-sm">
                Дата оформления:
                <span> {{ item.created?.toLocaleDateString() }}</span>
              </div>
              <div class="p-component text-md">
                Заказчик:
                <span class="text-primary"> {{ item.userName || 'неизвестный пользователь' }}</span>
              </div>
              <div class="p-component text-md">
                Организация:
                <span class="text-primary">
                  {{ item.organisationName || 'неизвестный пользователь' }}</span>
              </div>
            </div>
            <template v-if="!item.expanded.value">
              <div class="col-3">
                <div class="p-component text-md">Количество: {{ item.quantity }}</div>
              </div>
              <div class="col-3">
                <div class="p-component text-md">Сумма заказа: {{ item.sum }} ₽</div>
              </div>
            </template>
            <template v-else>
              <div class="col-6"></div>
            </template>
            <div class="col-3"></div>
            <div class="w-full mt-2 px-2" v-if="item.expanded.value">
              <template v-if="item.fullOrder.value != null">
                <div class="flex mt-3 mx-1 flex-row justify-content-start">
                  <div class="p-component text-lg font-bold">
                    Количество товаров: {{ item.quantity }}
                  </div>
                </div>
                <prime-divider class="mt-1 mb-2"></prime-divider>
                <div v-for="product in item.fullOrder.value.orderItems" :key="product.productId"
                  class="grid px-1 mt-1 border-round shadow-1">
                  <div class="col-2">
                    <file-store-image fit-width :id="product.documentId"></file-store-image>
                  </div>
                  <div class="col-4 flex flex-column gap-3">
                    <router-link :to="{ name: 'product', params: { id: product.productId } }"
                      class="flex gap-3 align-items-center not-link text-primary">
                      {{ product.productName }}</router-link>
                  </div>
                  <div class="col-2 flex flex-row justify-content-end">
                    <div class="p-component text-md">Цена: {{ product.price }} ₽</div>
                  </div>
                  <div class="col-4 flex flex-row justify-content-end">
                    <div class="p-component text-md">Количество: {{ product.quantity }}</div>
                  </div>
                </div>
                <div class="flex mt-3 mx-1 flex-row justify-content-end">
                  <div class="p-component text-lg font-bold">Итого: {{ item.sum }} ₽</div>
                </div>
              </template>
            </div>
          </div>
        </template>
      </card>
    </div>
    <prime-paginator class="mt-2" v-if="pageNumber && pageSize && (currentPage?.totalItemCount ?? 0) > 0"
      @page="changePage" :rows="pageSize" :first="pageSize * (pageNumber - 1)"
      :totalRecords="currentPage?.totalItemCount ?? 0"></prime-paginator>
  </app-page>
</template>

<script lang="ts">
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { ordersHttpService } from '@/app/orders/infrastructure/order.http-service';
import { IOrderModelFull } from '@/app/orders/model/IOrderModel';
import { ordersService } from '@/app/orders/services/orders.service';
import { ordersStore } from '@/app/orders/state/orders.store';
import { userInfo } from '@/store/user.store';

import { PrimePaginator } from '@/tools/prime-vue-components';
import { defineComponent, ref, watch, computed } from 'vue'; // , toRaw

export default defineComponent({
  components: { PrimePaginator },
  setup() {
    const userOrg = computed(() => userInfo.value?.org)

    watch(
      [ordersStore.pageNumber, ordersStore.pageSize],
      ([pageNumber, pageSize]) => {
        ordersService.loadPage({
          pageNumber,
          pageSize,
          producerId: userOrg.value?.id,
          organisationId: undefined
        });
      },
      {
        immediate: true,
      },
    );

    const {
      status: ordersStatus,
      pageNumber,
      pageSize,
      currentPage,
      currentPageItems,
    } = ordersStore;
    const changePage = ({ page }: { page: number }) => {
      pageNumber.value = page + 1;
    };

    const expandedOrderIds = ref<number[]>([]);

    const getxpandControllerFor = (id: number) =>
      computed({
        get: () => expandedOrderIds.value.find((x) => x === id) != null,
        set: (val) => {
          // console.log({ val, id });

          if (val) {
            expandedOrderIds.value = [...expandedOrderIds.value, id];
          } else {
            expandedOrderIds.value = expandedOrderIds.value.filter((x) => x !== id);
          }
        },
      });

    const expandedOrders = ref<Record<number, IOrderModelFull | undefined>>({});
    const pageItems = computed(() =>
      (currentPageItems.value ?? []).map((i) => ({
        ...i,
        expanded: getxpandControllerFor(i.id),
        fullOrder: computed(() => expandedOrders.value[i.id] ?? null),
      })),
    );

    watch(
      expandedOrderIds,
      (values) => {
        // ('values', toRaw(values));

        values
          .filter((id) => expandedOrders.value[id] == null)
          .forEach((val) => {
            ordersHttpService.getOrder(val).then((result) => {
              // console.log({ result });

              if (result.status === HttpStatus.Success && result.data != null) {
                expandedOrders.value[result.data.id] = result.data;
              }
            });
          });
      },
      {
        immediate: true,
      },
    );

    const status = [
      { key: 0, value: 'Новый' },
      { key: 1, value: 'В обработке' },
      { key: 10, value: 'Завершен' },
      { key: 11, value: 'Отменен' }
    ]

    const getStatus = (current: string) => {
      if (current === 'Новый') {
        return status;
      }

      if (current === 'В обработке') {
        return status.slice(1);
      }

      return undefined;
    }

    const getSeverity = (current: string) => {
      if (current === 'Новый') {
        return "info";
      }

      if (current === 'В обработке') {
        return "warning";
      }

      if (current === 'Завершен') {
        return "success";
      }

      if (current === 'Отменен') {
        return "danger";
      }

      return "info";
    }

    const changeStatus = async (event: { value: string }, id: number,) => {
      const s = status.filter(x => x.value === event.value)[0].key;
      const res = await ordersService.updateStatus(id, s);
      if (res) {
        ordersService.loadPage({
          pageNumber: ordersStore.pageNumber.value,
          pageSize: ordersStore.pageSize.value,
          producerId: userOrg.value?.id,
          organisationId: undefined
        });
      }
    }

    return {
      ordersStore,
      ordersStatus,
      pageNumber,
      pageSize,
      currentPage,
      changePage,
      pageItems,
      getxpandControllerFor,
      expandedOrders,
      userOrg,
      status,
      changeStatus,
      getStatus,
      getSeverity
    };
  },
});
</script>

<style lang="scss" scoped>
:deep(.p-card-title) {
  height: 28px;
}

:deep(div.p-dropdown.p-component.p-inputwrapper.p-inputwrapper-filled span.p-dropdown-label.p-inputtext) {
  padding: 6px;
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
