<template>
  <v-chart class="chart" :option="option" />
</template>

<script lang="ts">
// import { use, graphic } from 'echarts/core';
import { use, graphic } from 'echarts/core';
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
      default: 'Время на сайте',
    },
  },
  setup(props) {
    const option = ref({
      grid: {
        containLabel: true,
        height: '50%',
        left: '30%',
      },
      color: ['#3275e1'],
      title: {
        show: true,
        text: props.title,
      },
      tooltip: {
        show: false,
      },
      legend: {
        show: true,
        orient: 'vertical',
        selectedMode: false,
        left: 'left',
        top: 'bottom',
        padding: [
          0, // up
          0, // right
          50, // down
          0, // left
        ],
        data: [
          {
            name: '3:10',
            icon: 'none',
            textStyle: {
              fontSize: 42,
              fontWeight: 'bold',
            },
          },
          {
            name: '+64,8 %',
            icon: 'none',
            textStyle: {
              fontSize: 16,
              color: '#3bdd85',
            },
          },
        ],
      },
      label: {
        show: false,
      },
      xAxis: {
        show: false,
        type: 'time',
        axisLabel: {
          show: false,
        },
        axisTick: {
          show: false,
        },
      },
      yAxis: {
        show: false,
        type: 'value',
        min: 55,
        max: 85,
        interval: 25,
      },
      series: [
        { name: '3:10', data: [], type: 'line' },
        {
          name: '+64,8 %',
          showSymbol: false,
          type: 'line',
          data: [
            ['2022-10-1', 65],
            ['2022-10-15', 60],
            ['2022-10-28', 85],
            ['2022-11-7', 65],
            ['2022-11-15', 70],
            ['2022-11-23', 65],
          ],
          areaStyle: {
            opacity: 0.5,
            color: new graphic.LinearGradient(1, 0, 1, 1, [
              {
                offset: 0,
                color: 'rgb(0, 0, 255)',
              },
              {
                offset: 0.9,
                color: 'rgb(0, 0, 255, 0)',
              },
            ]),
          },
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
