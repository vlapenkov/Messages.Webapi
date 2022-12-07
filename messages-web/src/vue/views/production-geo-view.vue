<template>
  <h1 class="p-component text-xl sm:text-2xl mb-1">География производства</h1>
  <prime-divider class="mt-0"></prime-divider>
  <div class="flex flex-row justify-content-end">
    <prime-button
      class="text-sm font-normal p-bbutton-sm p-button-secondary p-1"
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
      <div v-for="(r, i) in regions" :key="i" class="flex flex-row mb-3">
        <a :href="'/catalog'">
          <span>{{ r[2] }}</span>
        </a>
      </div>
    </div>
  </transition-fade>
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
      [86.250023, 59.390129, 'Новосибирск'],
      [61.908876, 61.311001, 'Екатеринбург'],
      [110.889468, 69.217334, 'Норильск'],
      [110.160774, 60.076527, 'Якутск'],
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
