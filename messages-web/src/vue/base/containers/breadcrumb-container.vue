<template>
  <breadcrumb :home="home" :model="items" class="rk-breadcrumb" />
</template>

<script lang="ts">
import { breadcrumbStore } from '@/store/breadcrumb.store';
import { computed, defineComponent } from 'vue';
import { useRoute, useRouter } from 'vue-router';

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
      breadcrumbItemsByPath({ name: route.name?.toString() }).value.map(
        (x) =>
          ({
            label: x.label(),
            to: router.resolve({ name: x.route.name }).fullPath,
          } as IBreadcrumbModel),
      ),
    );
    const home = computed(() => breadcrumbs.value[0]);
    const items = computed(() => breadcrumbs.value.slice(1));
    return {
      route,
      list,
      home,
      items,
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
