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
  setup() {
    const option = ref({
      color: ['#3275E1', '#7FE0FF', '#BB87FC', '#FFB169', '#A5E763', '#FF6971'],
      title: {
        show: false,
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
          name: '',
          type: 'pie',
          radius: ['40%', '70%'],
          data: [
            { value: 25, name: 'Автоматические системы\nуправления\n25%' },
            { value: 6, name: 'Геоинформацион\nные сервисы\n6%' },
            { value: 9, name: 'Продукция для ЖКХ \nи инфраструктуры\n9%' },
            { value: 18, name: 'Легкорельсовый\nтранспорт\n и машиностроение\n18%' },
            { value: 12, name: 'Медицинская \nпродукцуия\n12%' },
            { value: 30, name: 'ТЭК\n30%' },
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
  height: 30vh;
  width: 30vw;
}
</style>
