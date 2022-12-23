<template>
  <div>
    <h1 class="p-component text-xl sm:text-2xl mb-1">География производства</h1>
    <prime-divider class="mt-0 mb-0"></prime-divider>
    <div class="flex flex-row justify-space-between align-items-center">
      <div class="col-3 pl-0">
        <dropdown
          v-model="regionModel"
          :options="regionOptions"
          placeholder="Регион"
          show-clear
          :style="{ width: '100%' }"
        />
      </div>
      <div class="col-3">
        <dropdown
          v-model="organizationModel"
          :options="organizationOptions"
          placeholder="Производитель"
          show-clear
          :style="{ width: '100%' }"
          class="si"
        />
      </div>
      <div class="col-6 flex flex-row justify-content-end align-items-center">
        <prime-button
          class="text-sm font-normal p-bbutton-sm p-button-secondary p-1 mr-3"
          :class="selectedMode !== Modes.LIST ? 'p-button-text' : undefined"
          icon="pi pi-list"
          @click="selectedMode = Modes.LIST"
        />
        <prime-button
          class="text-sm font-normal p-bbutton-sm p-button-secondary p-1"
          :class="selectedMode !== Modes.MAP ? 'p-button-text' : undefined"
          icon="pi pi-map-marker"
          @click="selectedMode = Modes.MAP"
        />
      </div>
    </div>

    <transition-fade>
      <div v-if="selectedMode === Modes.MAP" class="map-container">
        <yandex-map
          :settings="settings"
          :cluster-options="clusterOptions"
          :coords="[65, 90]"
          :zoom="3"
          class="map"
        >
          <ymap-marker
            v-for="o in filteredOrgs"
            :key="o.id"
            :marker-id="o.id"
            :coords="[o.latitude, o.longitude]"
            :icon="markerIcon"
            :balloon-template="balloonTemplate(o)"
            cluster-name="cluster"
          >
          </ymap-marker>
        </yandex-map>
      </div>
      <div v-if="selectedMode === Modes.LIST">
        <div v-if="filteredOrgs.length > 0" class="w-full h-full grid mt-1">
          <div v-for="(org, i) in filteredOrgs" :key="i" class="col-4">
            <card class="w-full h-full production-geo-card">
              <template #content>
                <router-link :to="`/organization/${org.id}`" class="no-underline">
                  <div class="flex flex-column">
                    <div class="flex flex-row">
                      <span
                        class="w-full font-semibold text-900"
                        :style="{ overflowWrap: 'break-word' }"
                      >
                        {{ org.name }}
                      </span>
                    </div>
                    <div class="flex flex-row mt-1">
                      <span class="w-full text-700" :style="{ overflowWrap: 'break-word' }">
                        {{ org.region }}
                      </span>
                    </div>
                  </div>
                </router-link>
              </template>
            </card>
          </div>
        </div>
        <div v-else class="flex flex-row justify-content-center mt-5">
          <span class="p-component">Организации не найдены</span>
        </div>
      </div>
    </transition-fade>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, ref } from 'vue';
import { useOrganizations } from '@/composables/organizations.composable';
import { yandexMap, ymapMarker } from 'vue-yandex-maps';
import { OrganizationModel } from '@/app/organizations/model/organization.model';

export default defineComponent({
  components: {
    yandexMap,
    ymapMarker,
  },
  setup() {
    enum Modes {
      MAP,
      LIST,
    }
    const settings = {
      apiKey: process.env.VUE_APP_YANDEX_MAP_API_KEY,
      lang: process.env.VUE_APP_YANDEX_MAP_LANG,
      coordorder: process.env.VUE_APP_YANDEX_MAP_COORDORDER,
      version: process.env.VUE_APP_YANDEX_MAP_VERSION,
    };
    const markerIcon = {
      layout: 'default#image',
      // eslint-disable-next-line global-require
      imageHref: require('@/assets/icons/marker.png'),
      imageSize: [22, 35],
      imageOffset: [0, 0],
    };
    const clusterOptions = {
      cluster: {
        gridSize: 128,
      },
    };
    const balloonTemplate = (org: OrganizationModel) => `
      <div class="w-full" style="max-height: 200px">
        <div><span style="font-weight: 600">${org.name}</span></div>
        <div class="mt-1"><span style="font-weight: 500">${org.region}</span></div>
        <div class="flex flex-row w-full justify-content-center align-items-center mt-1 p-2 bg-primary border-round-sm cursor-pointer">
          <a href="/organization/${org.id}" class="text-white" style="text-decoration: none">
            <span>Перейти к организации</span>
          </a>
        </div>
      </div>`;
    const selectedMode = ref(Modes.MAP);
    const regionModel = ref();
    const organizationModel = ref();
    const { organizations, organizationOptions, regionOptions } = useOrganizations();
    const filteredOrgs = computed(() =>
      (organizations.value ?? [])
        .filter((x) => (regionModel.value != null ? x.region === regionModel.value : true))
        .filter((x) =>
          organizationModel.value != null ? x.name === organizationModel.value : true,
        ),
    );
    return {
      Modes,
      selectedMode,
      filteredOrgs,
      regionModel,
      organizationModel,
      organizationOptions,
      regionOptions,
      settings,
      markerIcon,
      clusterOptions,
      balloonTemplate,
    };
  },
});
</script>

<style lang="scss" scoped>
.production-geo-card {
  :deep(.p-card-content) {
    padding: 0;
  }
}
.map-container {
  :deep(.ymap-container) {
    height: 100%;
  }

  .map {
    height: 50vh;
  }
}
</style>
