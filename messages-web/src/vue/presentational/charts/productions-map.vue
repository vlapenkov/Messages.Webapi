<template>
  <v-chart class="chart-map" :option="option" />
</template>

<script lang="ts">
import { CanvasRenderer } from 'echarts/renderers';
import { registerMap, use } from 'echarts/core';
import { defineComponent, onBeforeMount, PropType, ref } from 'vue';
import { geoTransverseMercator } from 'd3';
import VChart from 'vue-echarts';
import {
  GeoComponent,
  TitleComponent,
  TooltipComponent,
  LegendComponent,
} from 'echarts/components';
import { CustomChart } from 'echarts/charts';
import axios from 'axios';

use([CanvasRenderer, GeoComponent, TitleComponent, TooltipComponent, LegendComponent, CustomChart]);

export default defineComponent({
  components: {
    VChart,
  },
  props: {
    regions: {
      type: Object as PropType<(string | number)[][]>,
      default: null,
    },
  },
  setup(props) {
    const option = ref();
    onBeforeMount(async () => {
      await axios
        .get('/map/russia.json')
        .then((resp) => resp.data)
        .then((geoJson) => {
          registerMap('russia', geoJson);
          const projection = geoTransverseMercator().rotate([-90, -90]);
          option.value = {
            tooltip: {
              enterable: true,
              borderColor: '#fff',
              triggerOn: 'click',
            },
            geo: {
              map: 'russia',
              roam: false,
              itemStyle: {
                areaColor: '#e7e8ea',
              },
              emphasis: {
                disabled: true,
              },
              tooltip: {
                show: true,
                // eslint-disable-next-line @typescript-eslint/no-explicit-any
                formatter: (params: any) => {
                  if (params.componentType === 'series') {
                    return `<span style="font-weight: 600">${params.data[2]}</span><br/><a href="/catalog"><span>Список производимой продукции</span></a>`;
                  }
                  return undefined;
                },
              },
              projection: {
                project: (point: [number, number]) => projection(point),
                unproject: (point: [number, number]) => {
                  if (projection.invert == null) return;
                  projection.invert(point);
                },
              },
            },
            series: {
              type: 'custom',
              coordinateSystem: 'geo',
              geoIndex: 0,
              zlevel: 1,
              data: props.regions,
              // eslint-disable-next-line @typescript-eslint/no-explicit-any
              renderItem(params: any, api: any) {
                const coord = api.coord([
                  api.value(0, params.dataIndex),
                  api.value(1, params.dataIndex),
                ]);
                return {
                  type: 'group',
                  x: coord[0],
                  y: coord[1],
                  children: [
                    {
                      type: 'path',
                      shape: {
                        d: 'M16 0c-5.523 0-10 4.477-10 10 0 10 10 22 10 22s10-12 10-22c0-5.523-4.477-10-10-10zM16 16c-3.314 0-6-2.686-6-6s2.686-6 6-6 6 2.686 6 6-2.686 6-6 6z',
                        x: -10,
                        y: -35,
                        width: 20,
                        height: 40,
                      },
                      style: {
                        fill: 'red',
                      },
                    },
                  ],
                };
              },
            },
          };
        });
    });

    return {
      option,
    };
  },
});
</script>

<style lang="scss">
.chart-map {
  width: 100%;
  height: 650px;
}
</style>
