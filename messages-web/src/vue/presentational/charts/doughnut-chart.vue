<template>
  <doughnut
    :chart-options="chartOptions"
    :chart-data="chartData"
    :chart-id="chartId"
    :plugins="plugins"
    :css-classes="cssClasses"
    :styles="styles"
  />
</template>

<script lang="ts">
import { defineComponent, h } from 'vue';
import { Doughnut } from 'vue-chartjs';
import * as DataLables from 'chartjs-plugin-datalabels';
import { Chart as ChartJS, Title, Tooltip, Legend, ArcElement, CategoryScale } from 'chart.js';

ChartJS.register(Title, Tooltip, Legend, ArcElement, CategoryScale, DataLables.default);

export default defineComponent({
  components: {
    Doughnut,
  },
  props: {
    chartId: {
      type: String,
      default: 'doughnut-chart',
    },
    cssClasses: {
      default: '',
      type: String,
    },
    title: {
      default: '',
      type: String,
    },
  },
  setup(props) {
    const chartData = {
      labels: [
        'Автоматические системы\nуправления',
        'Геоинформацион\nные сервисы',
        'Продукция для ЖКХ \nи инфраструктуры',
        'Легкорельсовый транспорт\n и машиностроение',
        'Медицинская \nпродукцуия',
        'ТЭК',
      ],
      datasets: [
        {
          backgroundColor: ['#3275E1', '#7FE0FF', '#BB87FC', '#FFB169', '#A5E763', '#FF6971'],
          data: [25, 6, 9, 18, 12, 30],
          // eslint-disable-next-line
        } as any,
      ],
    };
    const chartOptions = {
      responsive: true,
      maintainAspectRatio: true,
      aspectRatio: 12 / 6,
      cutout: '65%',
      layout: {
        padding: {
          left: 150,
          right: 150,
          top: 20,
          bottom: 20,
        },
      },
      plugins: {
        legend: {
          display: false,
        },
        tooltip: {
          enabled: false,
        },
        title: {
          display: false,
          text: props.title,
        },
        datalabels: {
          formatter: (value: string, context: DataLables.Context): unknown =>
            // eslint-disable-next-line
            `${context.chart.data.labels![context.dataIndex]}\n${value}%`,
          anchor: 'end',
          align: 'end',
          // eslint-disable-next-line
        } as any,
      },
    };

    return () =>
      h(Doughnut, {
        chartData,
        chartOptions,
        chartId: props.chartId,
        cssClasses: props.cssClasses,
        styles: {},
        plugins: [],
      });
  },
});
</script>

<style lang="scss" scoped></style>
