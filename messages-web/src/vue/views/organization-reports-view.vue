<template>
  <app-page title="Отчеты">
    <div class="reports">
      <org-chart></org-chart>
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

.full-chart-size {
  min-width: 38vw;
  min-height: 56vh;
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
