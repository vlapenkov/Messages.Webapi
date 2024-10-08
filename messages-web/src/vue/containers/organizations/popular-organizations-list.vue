<template>
  <div class="popular-organization-list">
    <div class="grid w-full">
      <template v-if="items == null">
        <div v-for="i in 8" :key="i" class="col-3">
          <skeleton height="100px"></skeleton>
        </div>
      </template>
      <div v-else v-for="o in items" :key="o.id" class="col-3">
        <prime-card transparent class="h-full" @click="viewOrganization(o)">
          <div class="h-full w-full flex flex-row align-items-start pl-3 py-2 gap-4">
            <div>
              <avatar class="flex-shrink-0" :label="o.name[0]" shape="circle"></avatar>
            </div>
            <div>
              <div class="flex flex-row">
                <span class="font-semibold">{{ o.name }}</span>
              </div>
              <div class="flex flex-row">
                <span>{{ o.region }}</span>
              </div>
            </div>
          </div>
        </prime-card>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { OrganizationModel } from '@/app/organizations/model/organization.model';
import { organizationsStore } from '@/app/organizations/state/organizations.store';
import { computed, defineComponent } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  setup() {
    const router = useRouter();
    const items = computed(() => {
      if (organizationsStore.currentPageItems.value == null) {
        return null;
      }
      const data = [...organizationsStore.currentPageItems.value];
      return data
        .sort(
          (a: OrganizationModel, b: OrganizationModel) =>
            (a.lastModified?.getTime() ?? 0) - (b.lastModified?.getTime() ?? 0),
        )
        .sort((a: OrganizationModel, b: OrganizationModel) => a.name[0].localeCompare(b.name[0]))
        .slice(0, 8);
    });
    const viewOrganization = (item: OrganizationModel) => {
      router.push({ name: 'organization', params: { id: item.id } });
    };
    return {
      items,
      viewOrganization,
    };
  },
});
</script>

<style lang="scss">
.popular-organization-list {
  :deep(.p-card-header) {
    line-height: 0;
    text-align: center;
  }

  .p-card-header {
    line-height: 0 !important;
    text-align: center !important;
  }

  :deep(.p-card-body) {
    padding: 0;
  }

  .p-card-body {
    padding: 0 !important;
    width: 100% !important;
    height: 100% !important;
  }

  :deep(.p-card-content) {
    padding-bottom: 0;
    padding-top: 0;
  }

  .p-card-content {
    padding-bottom: 0 !important;
    padding-top: 0 !important;
    width: 100% !important;
    height: 100% !important;
  }

  .custom-button-text {
    padding: 0;
  }
}
</style>
