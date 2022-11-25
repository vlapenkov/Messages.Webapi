<template>
  <app-page title="Отчеты">
    <div class="reports">
      <tab-view ref="tabview1">
        <tab-panel header="Аналитические отчеты по продажам">
          <card>
            <template #content>
              <div class="card-container">
                <div class="flex">
                  <div
                    class="flex-grow-1 flex align-items-center justify-content-center border-round"
                  >
                    <div class="text-3xl font-semibold">756 млрд. руб</div>
                  </div>
                </div>
                <div class="flex">
                  <div
                    class="flex-grow-1 flex align-items-center justify-content-center border-round"
                  >
                    <div>Сумма всех заключенных сделок</div>
                  </div>
                </div>
              </div>
            </template>
          </card>

          <div class="flex mt-3">
            <card class="flex-1 flex align-items-center justify-content-center border-round mr-2">
              <template #content>
                <div class="card-container">
                  <div class="flex">
                    <div
                      class="flex-grow-1 flex align-items-center justify-content-center border-round"
                    >
                      <div class="text-3xl font-semibold">36</div>
                    </div>
                  </div>
                  <div class="flex">
                    <div
                      class="flex-grow-1 flex align-items-center justify-content-center border-round"
                    >
                      <div>Клиентов</div>
                    </div>
                  </div>
                </div>
              </template>
            </card>
            <card class="flex-1 flex align-items-center justify-content-center border-round mr-2">
              <template #content>
                <div class="card-container">
                  <div class="flex">
                    <div
                      class="flex-grow-1 flex align-items-center justify-content-center border-round"
                    >
                      <div class="text-3xl font-semibold">58%</div>
                    </div>
                  </div>
                  <div class="flex">
                    <div
                      class="flex-grow-1 flex align-items-center justify-content-center border-round"
                    >
                      <div>Конверсия сделок</div>
                    </div>
                  </div>
                </div>
              </template>
            </card>
            <card class="flex-1 flex align-items-center justify-content-center border-round">
              <template #content>
                <div class="card-container">
                  <div class="flex">
                    <div
                      class="flex-grow-1 flex align-items-center justify-content-center border-round"
                    >
                      <div class="text-3xl font-semibold">6</div>
                    </div>
                  </div>
                  <div class="flex">
                    <div
                      class="flex-grow-1 flex align-items-center justify-content-center border-round"
                    >
                      <div>Средний срок оплаты дней</div>
                    </div>
                  </div>
                </div>
              </template>
            </card>
          </div>

          <h3 class="mt-5">Товарные сегменты</h3>
          <card>
            <template #content>
              <div class="flex flex-wrap card-container">
                <div class="flex-1 flex align-items-center justify-content-center m-2 border-round">
                  <top-chart chartId="id2" cssClasses="chart-size" title="Маржа по сегментам" />
                </div>
                <div class="flex-1 flex align-items-center justify-content-center m-2 border-round">
                  <doughnut-chart chartId="id1" cssClasses="chart-size" />
                </div>
              </div>
            </template>
          </card>

          <div class="flex mt-3">
            <card class="flex-1 flex align-items-center justify-content-center border-round mr-3">
              <template #content>
                <div class="card-container">
                  <funnel-chart title="Воронка продаж: Геоинформационные сервисы" />
                </div>
              </template>
            </card>
            <card class="flex-1 flex align-items-center justify-content-center border-round">
              <template #content>
                <div class="card-container">
                  <funnel-chart title="Воронка продаж: ТЭК" />
                </div>
              </template>
            </card>
          </div>
        </tab-panel>
        <tab-panel header="Аналитические отчеты по посещаемости">
          <div class="flex mt-3">
            <card class="flex-1 flex align-items-center justify-content-center border-round mr-2">
              <template #content>
                <trend1-chart></trend1-chart>
              </template>
            </card>
            <card class="flex-1 flex align-items-center justify-content-center border-round mr-2">
              <template #content>
                <trend2-chart></trend2-chart>
              </template>
            </card>
            <card class="flex-1 flex align-items-center justify-content-center border-round">
              <template #content>
                <trend3-chart></trend3-chart>
              </template>
            </card>
          </div>
        </tab-panel>
      </tab-view>
    </div>
  </app-page>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return {};
  },
});
</script>

<style lang="scss" scoped>
.chart-size {
  min-width: 28vw;
  min-height: 28vh;
}

::v-deep(.p-tabview-nav) {
  background: none;
}

::v-deep(.p-tabview-header) {
  background: none;
  margin-right: 20px;
}

::v-deep(.p-tabview .p-tabview-nav li a.p-tabview-nav-link) {
  background: none;
}

::v-deep(.p-tabview .p-tabview-nav li .p-tabview-nav-link span) {
  font-weight: 400;
}

::v-deep(.p-tabview-panels) {
  background: none;
}

::v-deep(div.p-card .p-card-content) {
  padding: 0;
}

::v-deep(div.p-card .p-card-body) {
  width: 100%;
}
</style>
