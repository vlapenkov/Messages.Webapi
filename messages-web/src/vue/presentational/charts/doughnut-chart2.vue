<template>
  <v-chart class="chart" :option="option" />
</template>

<script lang="ts">
import { use } from 'echarts/core';
import { CanvasRenderer } from 'echarts/renderers';
import { PieChart } from 'echarts/charts';
import { TitleComponent, TooltipComponent, LegendComponent } from 'echarts/components';
import VChart from 'vue-echarts';
import { ref, defineComponent } from 'vue';

use([CanvasRenderer, PieChart, TitleComponent, TooltipComponent, LegendComponent]);

export default defineComponent({
  name: 'doughnut-chart',
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
      color: ['#3275E1', '#7FE0FF', '#BB87FC', '#FFB169', '#A5E763', '#FF6971'],
      title: {
        show: true,
        text: props.title,
        left: 'center',
      },
      tooltip: {
        show: true,
        trigger: 'item',
        formatter: '{b}',
      },
      legend: {
        show: false,
      },
      series: [
        {
          name: props.title,
          type: 'pie',
          radius: ['40%', '70%'],
          data: [
            { value: 25, name: 'АО "НПО им. С.А. Лавочкина"\n25%' },
            { value: 6, name: 'АО "Конструкторское бюро химавтоматики"\n6%' },
            { value: 9, name: 'АО "Научно-исследовательский институт\nточных приборов"\n9%' },
            { value: 18, name: 'АО "КБ "Арсенал"\n18%' },
            { value: 12, name: 'АО "Красноярский машиностроительный завод"\n12%' },
            {
              value: 38,
              name: 'АО "106 эксперементальны оптико-\nмеханический завод" (АО"106 ЭОМЗ")\n30%',
            },
          ],
          emphasis: {
            itemStyle: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)',
            },
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
  height: 40vh;
  width: 60vw;
}
</style>
