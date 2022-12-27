<template>
  <div>
    <div class="flex flex-row justify-content-between align-items-end mr-1">
      <app-text mode="header" class="p-component">
        <slot name="title">{{ title }}</slot>
      </app-text>
      <slot name="extra">
        <router-link v-if="!hideLink" class="no-underline" :to="to">
          <div class="flex flex-row gap-2 align-items-center">
            <div class="pb-1">
              <app-text class="p-component" mode="primary"> {{ linkText }} </app-text>
            </div>
            <div>
              <app-text tag="i" class="pi pi-arrow-right" mode="primary"> </app-text>
            </div>
          </div>
        </router-link>
      </slot>
    </div>
    <prime-divider class="mt-2"></prime-divider>
    <slot></slot>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import { RouteLocationRaw } from 'vue-router';

export default defineComponent({
  props: {
    title: {
      type: String,
      default: 'Секция без названия',
    },
    hideLink: {
      type: Boolean,
      default: false,
    },
    linkText: {
      type: String,
      default: 'Секция без названия',
    },
    to: {
      type: Object as PropType<RouteLocationRaw>,
      default: (): RouteLocationRaw => ({ path: '/' }),
    },
  },
});
</script>

<style scoped></style>
