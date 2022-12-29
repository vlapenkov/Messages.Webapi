<template>
  <breadcrumb :home="home" :model="items" class="rk-breadcrumb" />
</template>

<script lang="ts">
import { breadcrumbStore } from '@/store/breadcrumb.store';
import { defineComponent } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  setup() {
    const route = useRoute();
    const { list } = breadcrumbStore;
    return {
      route,
      list,
      home: { label: 'Главная', to: '/' },
      items: [
        { label: 'Каталог', to: '/catalog' },
        { label: 'Авиация', to: '/catalog?sectionId=4' },
      ],
    };
  },
});
</script>

<style lang="scss" scoped>
.rk-breadcrumb {
  border: none;
  background: none;
  padding: 0;
}
</style>
