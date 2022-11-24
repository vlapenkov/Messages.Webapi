<template>
  <v-chart class="chart" :option="option" />
</template>

<script lang="ts">
import { use } from 'echarts/core';
import { CanvasRenderer } from 'echarts/renderers';
import { BarChart } from 'echarts/charts';
import {
  TitleComponent,
  TooltipComponent,
  LegendComponent,
  GridComponent,
} from 'echarts/components';
import VChart from 'vue-echarts';
import { ref, defineComponent } from 'vue';

use([CanvasRenderer, BarChart, TitleComponent, TooltipComponent, LegendComponent, GridComponent]);

export default defineComponent({
  name: 'bar-chart',
  components: {
    VChart,
  },
  props: {
    title: {
      type: String,
      default: 'Топ',
    },
  },
  setup(props) {
    const option = ref({
      grid: {
        containLabel: true,
        height: '85%',
      },
      color: ['#3275E1'],
      title: {
        show: true,
        text: props.title,
      },
      tooltip: {
        show: true,
        trigger: 'item',
        formatter: '{b} {c} тыс.руб',
      },
      legend: {
        show: false,
      },
      label: {
        show: true,
        position: 'outside',
        formatter: '{c} тыс. руб',
      },
      xAxis: {
        show: false,
        max: 110,
        interval: 110,
      },
      yAxis: {
        type: 'category',
        axisLabel: {
          show: true,
        },
        axisTick: {
          show: false,
        },
        data: [
          'ТЭК',
          'Автоматические системы\nуправления',
          'Легкорельсовый транспорт\nи машиностроение',
          'Продукция для ЖКХ\nи инфраструктуры',
          'Медицинская\nпродукцуия',
          'Геоинформационные\nсервисы',
        ].reverse(),
      },
      series: [
        {
          name: '',
          type: 'bar',
          data: [78.1, 38.2, 35.7, 26.3, 1.4, 1.2].reverse(),
        },
      ],
    });

    return { option };
  },
});
</script>

<style scoped>
.chart {
  height: 30vh;
  width: 29vw;
}
</style>
