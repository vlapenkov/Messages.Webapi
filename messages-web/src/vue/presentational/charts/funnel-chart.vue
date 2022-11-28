<template>
  <v-chart class="chart" :option="option" />
</template>

<script lang="ts">
import { use } from 'echarts/core';
import { CanvasRenderer } from 'echarts/renderers';
import { FunnelChart } from 'echarts/charts';
import { TitleComponent, TooltipComponent, LegendComponent } from 'echarts/components';
import VChart from 'vue-echarts';
import { ref, defineComponent } from 'vue';

use([CanvasRenderer, FunnelChart, TitleComponent, TooltipComponent, LegendComponent]);

export default defineComponent({
  name: 'funnel-chart',
  components: {
    VChart,
  },
  props: {
    title: {
      type: String,
      default: 'Воронка',
    },
  },
  setup(props) {
    const option = ref({
      color: ['#FF6971', '#FFB169', '#FFB169', '#FFB169', '#A5E763'],
      title: {
        show: true,
        text: props.title,
      },
      tooltip: {
        show: true,
        trigger: 'item',
        formatter: '{b}',
      },
      label: {
        show: true,
        position: 'outside',
      },
      legend: {
        show: false,
      },
      series: [
        {
          name: '',
          type: 'funnel',
          funnelAlign: 'center',
          top: 30,
          bottom: 10,
          min: 0,
          max: 16,
          minSize: '0%',
          maxSize: '100%',
          sort: 'none',
          gap: 2,
          data: [
            { value: 10, name: 'В обработке: 10' },
            { value: 8, name: 'В корзине: 8' },
            { value: 6, name: 'Офрмление договора: 6' },
            { value: 4, name: 'Договор подписан: 4' },
            { value: 2, name: 'Успешно реализованно: 2' },
          ],
          emphasis: {
            label: {
              fontSize: 20,
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
  width: 27vw;
}
</style>
