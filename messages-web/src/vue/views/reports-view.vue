<template>
  <div class="reports">
    <tab-view ref="tabview1">
      <tab-panel header="Аналитические отчеты по продажам">
        <div class="flex flex-wrap card-container">
          <div class="flex-1 flex align-items-center justify-content-center m-2 border-round">
            <card>
              <template #content>
                <doughnut-chart chartId="id1" cssClasses="chart-size" title="sadfasfsdf" />
              </template>
            </card>
          </div>
          <div class="flex-1 flex align-items-center justify-content-center m-2 border-round"></div>
        </div>
      </tab-panel>
      <tab-panel header="Аналитические отчеты по посещаемости"> </tab-panel>
    </tab-view>
  </div>
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
  min-width: 25vw;
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
  padding: none;
}
</style>
