<template>
  <div>
    <h1 class="p-component text-xl sm:text-2xl mb-1">География производства</h1>
    <prime-divider class="mt-0 mb-0"></prime-divider>
    <div class="flex flex-row justify-content-end mt-2 mb-2">
      <prime-button
        class="text-sm font-normal p-bbutton-sm p-button-secondary p-1 mr-3"
        :class="selected !== Modes.LIST ? 'p-button-text' : undefined"
        icon="pi pi-list"
        @click="selected = Modes.LIST"
      />
      <prime-button
        class="text-sm font-normal p-bbutton-sm p-button-secondary p-1"
        :class="selected !== Modes.MAP ? 'p-button-text' : undefined"
        icon="pi pi-map-marker"
        @click="selected = Modes.MAP"
      />
    </div>
    <transition-fade>
      <productions-map v-if="selected === Modes.MAP" :regions="regions" />
      <div v-if="selected === Modes.LIST">
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
import { defineComponent, ref } from 'vue';

export default defineComponent({
  setup() {
    enum Modes {
      MAP,
      LIST,
    }
    const selected = ref(Modes.MAP);
    const regions = [
      [37.905868, 55.268184, 'Москва'],
      [82.250023, 55.390129, 'Новосибирск'],
      [60.908876, 56.311001, 'Екатеринбург'],
      [88.210393, 69.217334, 'Норильск'],
      [129.732178, 62.076527, 'Якутск'],
      [50.100199, 55.159897, 'Челябинская область'],
      [56.229441, 58.010454, 'Пермский край'],
      [30.315644, 59.938955, 'Санкт-Петербург'],
    ];
    return {
      Modes,
      selected,
      regions,
    };
  },
});
</script>

<style scoped></style>
