<template>
  <card>
    <template #content>
      <div class="card-container">
        <div class="flex">
          <div class="flex-grow-1 flex align-items-center justify-content-center border-round">
            <div class="text-3xl font-semibold">956 тыс. руб</div>
          </div>
        </div>
        <div class="flex">
          <div class="flex-grow-1 flex align-items-center justify-content-center border-round">
            <div>Сумма всех заключенных сделок</div>
          </div>
        </div>
      </div>
    </template>
  </card>

  <div class="flex mt-3">
    <card class="flex-1 flex align-items-center justify-content-center border-round mr-2">
      <template #content>
        <div class="card-container">
          <div class="flex">
            <div class="flex-grow-1 flex align-items-center justify-content-center border-round">
              <div class="text-3xl font-semibold">7</div>
            </div>
          </div>
          <div class="flex">
            <div class="flex-grow-1 flex align-items-center justify-content-center border-round">
              <div>Клиентов</div>
            </div>
          </div>
        </div>
      </template>
    </card>
    <card class="flex-1 flex align-items-center justify-content-center border-round mr-2">
      <template #content>
        <div class="card-container">
          <div class="flex">
            <div class="flex-grow-1 flex align-items-center justify-content-center border-round">
              <div class="text-3xl font-semibold">21%</div>
            </div>
          </div>
          <div class="flex">
            <div class="flex-grow-1 flex align-items-center justify-content-center border-round">
              <div>Конверсия сделок</div>
            </div>
          </div>
        </div>
      </template>
    </card>
    <card class="flex-1 flex align-items-center justify-content-center border-round">
      <template #content>
        <div class="card-container">
          <div class="flex">
            <div class="flex-grow-1 flex align-items-center justify-content-center border-round">
              <div class="text-3xl font-semibold">24</div>
            </div>
          </div>
          <div class="flex">
            <div class="flex-grow-1 flex align-items-center justify-content-center border-round">
              <div>Средний срок оплаты дней</div>
            </div>
          </div>
        </div>
      </template>
    </card>
  </div>

  <div class="flex mt-5">
    <card class="flex-1 flex align-items-center justify-content-center border-round mr-3">
      <template #content>
        <div class="card-container">
          <v-chart class="chart" :option="funnelOptions" />
        </div>
      </template>
    </card>
    <card class="flex-1 flex align-items-center justify-content-center border-round mr-3">
      <template #content>
        <div class="card-container">
          <v-chart class="chart" :option="trendOptions" />
        </div>
      </template>
    </card>
  </div>

  <h3 class="mt-5">Товарные сегменты</h3>
  <card>
    <template #content>
      <div class="flex flex-wrap card-container">
        <div class="flex-1 flex align-items-center justify-content-center m-2 border-round">
          <v-chart class="chart" :option="barOptions" />
        </div>
        <div class="flex-1 flex align-items-center justify-content-center m-2 border-round">
          <v-chart class="chart" :option="pieOptions" />
        </div>
      </div>
    </template>
  </card>
</template>

<script lang="ts">
import { use, graphic } from 'echarts/core';
import { CanvasRenderer } from 'echarts/renderers';
import { BarChart, FunnelChart, PieChart } from 'echarts/charts';
import { TitleComponent, TooltipComponent, LegendComponent, GridComponent } from 'echarts/components';
import VChart from 'vue-echarts';
import { ref, defineComponent } from 'vue';

use([CanvasRenderer, PieChart, FunnelChart, BarChart, TitleComponent, TooltipComponent, LegendComponent, GridComponent]);

export default defineComponent({
  name: 'org-chart',
  components: {
    VChart,
  },
  setup() {
    const pieOptions = ref({
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
            { value: 6, name: 'ТЭК \n6%' },
            { value: 9, name: 'Продукция для ЖКХ \nи инфраструктуры\n9%' },
            { value: 18, name: 'Легкорельсовый\nтранспорт\n и машиностроение\n18%' },
            { value: 12, name: 'Медицинская \nпродукцуия\n12%' },
            { value: 30, name: 'Геоинформацион\nные сервисы\n30%' },
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

    const barOptions = ref({
      grid: {
        containLabel: true,
        height: '85%',
      },
      color: ['#3275E1'],
      title: {
        show: true,
        text: "Маржа по сегментам",
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
        max: 70,
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
          'Геоинформационные\nсервисы',
          'Автоматические системы\nуправления',
          'Легкорельсовый транспорт\nи машиностроение',
          'Продукция для ЖКХ\nи инфраструктуры',
          'Медицинская\nпродукцуия',
          'ТЭК',
        ].reverse(),
      },
      series: [
        {
          name: '',
          type: 'bar',
          data: [56.1, 43.2, 31.7, 20.3, 14.4, 7.2].reverse(),
        },
      ],
    });

    const funnelOptions = ref({
      color: ['#FF6971', '#FFB169', '#FFB169', '#FFB169', '#A5E763'],
      title: {
        show: true,
        text: "Воронка продаж",
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
          max: 6,
          minSize: '00%',
          maxSize: '100%',
          sort: 'none',
          gap: 2,
          data: [
            { value: 4, name: 'В корзине: 4' },
            { value: 3, name: 'В обработке: 3' },
            { value: 2, name: 'Офрмление договора: 2' },
            { value: 1, name: 'Договор подписан: 1' },
            { value: 1, name: 'Успешно реализованно: 1' },
          ],
          emphasis: {
            label: {
              fontSize: 20,
            },
          },
        },
      ],
    });

    const trendOptions = ref({
      grid: {
        containLabel: true,
        height: '70%',
        left: '20%',
      },
      color: ['#3275e1'],
      title: {
        show: true,
        text: "Количество просмотров",
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
          40, // down
          0, // left
        ],
        data: [
          {
            name: '45',
            icon: 'none',
            textStyle: {
              fontSize: 42,
              fontWeight: 'bold',
            },
          },
          {
            name: '+25,2 %',
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
        { name: '45', data: [], type: 'line' },
        {
          name: '+25,2 %',
          showSymbol: false,
          type: 'line',
          data: [
            ['2022-10-1', 65],
            ['2022-10-15', 60],
            ['2022-10-28', 75],
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

    return {pieOptions, barOptions, funnelOptions, trendOptions };
  },
});
</script>

<style scoped>
.chart {
  height: 30vh;
  width: 28vw;
}
</style>
