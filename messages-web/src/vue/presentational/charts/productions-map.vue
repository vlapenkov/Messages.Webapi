<template>
  <v-chart class="chart-map" :option="option" :loading="loading" />
</template>

<script lang="ts">
import { CanvasRenderer } from 'echarts/renderers';
import { registerMap, use } from 'echarts/core';
import { defineComponent, onBeforeMount, PropType, ref, watch } from 'vue';
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
import { OrganizationModel } from '@/app/organizations/model/organization.model';

use([CanvasRenderer, GeoComponent, TitleComponent, TooltipComponent, LegendComponent, CustomChart]);

export default defineComponent({
  components: {
    VChart,
  },
  props: {
    organizations: {
      type: Object as PropType<OrganizationModel[]>,
      default: null,
    },
  },
  setup(props) {
    const loading = ref<boolean | undefined>();
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const option = ref<any>(undefined);
    const tooltipHtmlTemplate = (org: OrganizationModel) => `
      <div class="w-full">
        <div><span style="font-weight: 600">${org.name}</span></div>
        <div class="mt-1"><span style="font-weight: 500">${org.region}</span></div>
        <div class="flex flex-row w-full justify-content-center align-items-center mt-1 p-2 bg-primary border-round-sm cursor-pointer">
          <a href="/organization/${org.id}" class="text-white" style="text-decoration: none">
            <span>Перейти к организации</span>
          </a>
        </div>
      </div>`;
    const setOption = (organizations: OrganizationModel[]) => {
      const projection = geoTransverseMercator().rotate([-105, -90]);
      option.value = {
        tooltip: {
          enterable: true,
          borderColor: '#fff',
          triggerOn: 'click',
          padding: 15,
        },
        geo: {
          map: 'russia',
          roam: false,
          itemStyle: {
            areaColor: '#d2d2db',
            borderColor: '#d2d2db',
          },
          emphasis: {
            disabled: true,
          },
          tooltip: {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            formatter: (params: any) => {
              if (params.componentType === 'series') {
                // выбранная организация по клику на метку
                const org: OrganizationModel = params.data[2];
                // организации с тем же регионом
                const orgs = organizations.filter((x) => x.city === org.city);
                const rows = orgs.map(
                  (x, i, arr) =>
                    `<div class="flex flex-row ${
                      i + 1 !== arr.length ? 'mb-4' : undefined
                    }">${tooltipHtmlTemplate(x)}</div>`,
                );
                return rows.reduce((acc, val) => acc + val, '');
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
          data: organizations.map((x) => [x.longitude, x.latitude, x]),
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
                    fill: '#ff6b5d',
                  },
                },
              ],
            };
          },
        },
      };
    };
    const updateMap = async (orgs: OrganizationModel[]) => {
      if (option.value != null) {
        setOption([]);
      }
      loading.value = true;
      await axios
        .get('/map/russia.json')
        .then((resp) => resp.data)
        .then((geoJson) => {
          registerMap('russia', geoJson);
          setOption(orgs);
          loading.value = false;
        });
    };
    onBeforeMount(async () => {
      await updateMap(props.organizations);
    });
    watch(
      () => props.organizations,
      async (orgs) => {
        await updateMap(orgs);
      },
    );
    return {
      option,
      loading,
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
