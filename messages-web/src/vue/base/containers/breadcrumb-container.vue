<template>
  <transition-fade>
    <breadcrumb v-if="isLoaded" :home="home" :model="items" class="rk-breadcrumb" />
    <skeleton v-else width="100%" height="40px" />
  </transition-fade>
</template>

<script lang="ts">
import { breadcrumbStore } from '@/store/breadcrumb.store';
import { computed, defineComponent } from 'vue';
import { RouteLocation, useRoute, useRouter } from 'vue-router';

interface IBreadcrumbModel {
  label: string;
  to: string;
}

export default defineComponent({
  setup() {
    const route = useRoute();
    const router = useRouter();
    const { list, breadcrumbItemsByPath } = breadcrumbStore;
    const breadcrumbs = computed<IBreadcrumbModel[]>(() =>
      breadcrumbItemsByPath(route).value.map(
        (x) =>
          ({
            label: x.label(),
            to: router.resolve(x.route() as RouteLocation).fullPath,
          } as IBreadcrumbModel),
      ),
    );
    const home = computed(() => breadcrumbs.value[0]);
    const items = computed(() => breadcrumbs.value.slice(1));
    const isLoaded = computed(() => home.value != null && items.value.length > 0);
    return {
      route,
      list,
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
