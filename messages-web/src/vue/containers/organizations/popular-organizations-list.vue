<template>
  <div class="popular-organization-list">
    <div class="grid w-full">
      <div v-for="o in items" :key="o.id" class="col-3">
        <card class="h-full p-2" @click="viewOrganization(o)" :style="{ cursor: 'pointer' }">
          <template #content>
            <div class="h-full w-full flex flex-row justify-space-between align-items-center">
              <div class="h-full w-full grid">
                <div class="col-3">
                  <div
                    class="h-full w-full flex flex-row justify-content-center align-items-center"
                  >
                    <avatar :label="o.name[0]" shape="circle"></avatar>
                  </div>
                </div>
                <div class="col-9">
                  <div class="flex flex-row">
                    <span class="font-semibold">{{ o.name }}</span>
                  </div>
                  <div class="flex flex-row">
                    <span>{{ o.region }}</span>
                  </div>
                </div>
              </div>
            </div>
          </template>
        </card>
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
      const data = [...(organizationsStore.currentPageItems.value ?? [])];
      return data.sort((a: OrganizationModel, b: OrganizationModel) =>
        a.name[0].localeCompare(b.name[0]),
      );
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
