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
  name: 'top-chart3',
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
        formatter: '{b} {c} %',
      },
      legend: {
        show: false,
      },
      label: {
        show: true,
        position: 'outside',
        formatter: '{c} %',
      },
      xAxis: {
        show: false,
        max: 30,
        interval: 30,
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
          'Прибор БИБ-4',
          'Датчик ДХС 523',
          'ММКК247',
          'Прибор МИПЭ',
          'МЧЭ033',
          'Ротор-3',
        ].reverse(),
      },
      series: [
        {
          name: props.title,
          type: 'bar',
          data: [26.79, 22.1, 16.45, 14.44, 5.2, 2.28].reverse(),
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
