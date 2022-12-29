<template>
  <transition-fade>
    <breadcrumb v-if="isLoaded" :home="home" :model="items" class="rk-breadcrumb" />
    <skeleton v-else width="100%" height="40px" />
  </transition-fade>
</template>

<script lang="ts">
import { breadcrumbStore } from '@/store/breadcrumb.store';
import { computed, defineComponent } from 'vue';
import { RouteLocation, useRoute } from 'vue-router';

interface IBreadcrumbModel {
  label: string;
  to: RouteLocation;
}

export default defineComponent({
  setup() {
    const route = useRoute();
    const { breadcrumbItemsByPath } = breadcrumbStore;
    const breadcrumbItems = breadcrumbItemsByPath(route);
    const breadcrumbs = computed<IBreadcrumbModel[]>(() =>
      breadcrumbItems.value.map(
        (x): IBreadcrumbModel => ({
          label: x.label() ?? '',
          to: x.route(),
        }),
      ),
    );
    const home = computed(() => breadcrumbs.value[0]);
    const items = computed(() => breadcrumbs.value.slice(1));
    const isLoaded = computed(() => home.value != null && items.value.length > 0);
    return {
      home,
      items,
      isLoaded,
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
