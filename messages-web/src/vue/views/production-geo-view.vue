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
      <productions-map v-if="selectedMode === Modes.MAP" :organizations="filteredOrgs" />
      <div v-if="selectedMode === Modes.LIST">
        <div v-for="(region, i) in filteredOrgs" :key="i" class="flex flex-row mb-3">
          <a
            :href="`/catalog?region=${region.region}`"
            :style="{ textDecoration: 'none', color: '#000' }"
          >
            <span>{{ region.region }}</span>
          </a>
        </div>
      </div>
    </transition-fade>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, ref } from 'vue';
import { useOrganizations } from '@/composables/organizations.composable';

export default defineComponent({
  setup() {
    enum Modes {
      MAP,
      LIST,
    }
    const selectedMode = ref(Modes.MAP);

    const regionModel = ref();
    const organizationModel = ref();

    const { organizations, organizationOptions, regionOptions } = useOrganizations();

    const filteredOrgs = computed(() =>
      organizations.value
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
    };
  },
});
</script>

<style scoped></style>
