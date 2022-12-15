import { organizationsService } from '@/app/organizations/services/organization.service';
import { organizationsStore } from '@/app/organizations/state/organizations.store';
import { computed, onMounted } from 'vue';

export function useOrganizations() {
  onMounted(() => {
    if (organizationsStore.status.value.status === 'initial') {
      organizationsService.loadPage({
        pageNumber: 1,
        pageSize: 8,
      });
    }
  });

  const organizations = computed(() => organizationsStore.currentPageItems.value ?? []);

  const regionOptions = computed(() =>
    (organizationsStore.currentPageItems.value ?? [])
      .map((x) => x.region)
      .filter((val, ind, arr) => arr.indexOf(val) === ind),
  );
  const organizationOptions = computed(() =>
    (organizationsStore.currentPageItems.value ?? []).map((x) => x.name),
  );

  return {
    organizations,
    regionOptions,
    organizationOptions,
  };
}
