<template>
  <v-chart class="chart" :option="option" />
</template>

<script lang="ts">
// import { use, graphic } from 'echarts/core';
import { use } from 'echarts/core';
import { CanvasRenderer } from 'echarts/renderers';
import { LineChart } from 'echarts/charts';
import {
  TitleComponent,
  TooltipComponent,
  LegendComponent,
  GridComponent,
} from 'echarts/components';
import VChart from 'vue-echarts';
import { ref, defineComponent } from 'vue';

use([CanvasRenderer, LineChart, TitleComponent, TooltipComponent, LegendComponent, GridComponent]);

export default defineComponent({
  name: 'trend1-chart',
  components: {
    VChart,
  },
  props: {
    title: {
      type: String,
      default: 'Новые посетители',
    },
  },
  setup(props) {
    const option = ref({
      grid: {
        containLabel: true,
        height: '50%',
      },
      color: ['#ffe92c'],
      title: {
        show: true,
        text: props.title,
      },
      tooltip: {
        show: false,
      },
      legend: {
        show: true,
        selectedMode: false,
        bottom: '0%',
        left: '10%',
        data: [
          {
            name: 'Новые посетители: 265',
            icon: 'circle',
            lineStyle: {
              color: '#ffe92c',
            },
          },
        ],
      },
      label: {
        show: false,
      },
      xAxis: {
        show: true,
        splitNumber: 1,
        type: 'time',
        axisLabel: {
          show: true,
          margin: 15,
          showMinLabel: true,
          showMaxLabel: true,
          formatter: '{dd}.{MM}.{yyyy}',
        },
        axisTick: {
          show: false,
        },
      },
      yAxis: {
        type: 'value',
        min: 55,
        max: 100,
        interval: 25,
      },
      series: [
        {
          name: 'Новые посетители: 265',
          showSymbol: false,
          type: 'line',
          data: [
            ['2022-10-23', 100],
            ['2022-10-26', 74],
            ['2022-11-7', 60],
            ['2022-11-15', 80],
            ['2022-11-23', 65],
          ],
        },
      ],
    });

    return { option };
  },
});
</script>

<style scoped>
.chart {
  height: 20vh;
  width: 350px;
}
</style>
