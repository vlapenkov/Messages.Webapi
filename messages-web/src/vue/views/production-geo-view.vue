<template>
  <div>
    <h1 class="p-component text-xl sm:text-2xl mb-1">География производства</h1>
    <prime-divider class="mt-0 mb-0"></prime-divider>
    <div class="flex flex-row justify-space-between align-items-center">
      <div class="col-4 pl-0">
        <dropdown
          v-model="regionModel"
          :options="regionOptions"
          placeholder="Регион"
          show-clear
          :style="{ width: '100%' }"
        />
      </div>
      <div class="col-4">
        <dropdown
          v-model="organizationModel"
          :options="organizationOptions"
          placeholder="Производитель"
          show-clear
          :style="{ width: '100%' }"
        />
      </div>
    </div>
    <div class="flex flex-row justify-content-end mb-2">
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
    <transition-fade>
      <productions-map v-if="selectedMode === Modes.MAP" :regions="regions" />
      <div v-if="selectedMode === Modes.LIST">
        <div v-for="(region, i) in regions" :key="i" class="flex flex-row mb-3">
          <a
            :href="`/catalog?region=${region[2]}`"
            :style="{ textDecoration: 'none', color: '#000' }"
          >
            <span>{{ region[2] }}</span>
          </a>
        </div>
      </div>
    </transition-fade>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, ref } from 'vue';
import { useOrganizations } from './composables/organizations.composable';

export default defineComponent({
  setup() {
    enum Modes {
      MAP,
      LIST,
    }
    const selectedMode = ref(Modes.MAP);

    const regionModel = ref();
    const organizationModel = ref();

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const { organizations, organizationOptions, regionOptions } = useOrganizations();

    const r = [
      [37.905868, 55.268184, 'Москва'],
      [82.250023, 55.390129, 'Новосибирск'],
      [60.908876, 56.311001, 'Екатеринбург'],
      [88.210393, 69.217334, 'Норильск'],
      [129.732178, 62.076527, 'Якутск'],
      [50.100199, 55.159897, 'Челябинская область'],
      [56.229441, 58.010454, 'Пермский край'],
      [30.315644, 59.938955, 'Санкт-Петербург'],
    ];
    const regions = computed(() =>
      r.filter((x) => {
        if (regionModel.value != null) {
          return x[2] === regionModel.value;
        }
        return true;
      }),
    );
    return {
      Modes,
      selectedMode,
      regions,
      regionModel,
      organizationModel,
      organizationOptions,
      regionOptions,
    };
  },
});
</script>

<style scoped></style>
