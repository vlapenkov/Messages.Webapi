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
      <productions-map v-if="selectedMode === Modes.MAP" :organizations="filteredOrgs" />
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
</style>
