<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page title="Управление категориями">
    <template v-if="sections != null">
      <div v-if="sections.length === 0">Категории отсутствуют</div>
      <tree v-else :value="sections"></tree>
    </template>

    <card class="shadow-none mt-3">
      <template #content>
        <div class="grid p-fluid">
          <div class="col-12 md:col-6">
            <span class="p-float-label">
              <input-text v-model="name" id="new-cat-name"></input-text>
              <label for="new-cat-name">Название категории</label>
            </span>
          </div>
          <div class="col-12 md:col-6">
            <span class="p-float-label">
              <tree-select
                :disabled="sections == null || sections.length === 0"
                :options="sections"
                v-model="selectedKeys"
                id="new-cat-name"
              ></tree-select>
              <label for="new-cat-name">Родительская категория</label>
            </span>
          </div>
        </div>
      </template>
      <template #footer>
        <div class="flex flex-row justify-content-end">
          <prime-button @click="save" icon="pi pi-plus" label="Добавить категорию"></prime-button>
        </div>
      </template>
    </card>
  </app-page>
</template>

<script lang="ts">
import { Creation } from '@/app/core/services/harlem/tools/not-valid-data';
import { SectionModel } from '@/app/sections/models/section.model';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { useSections } from '@/composables/sections.composable';
import { computed, defineComponent, onMounted } from 'vue';

export default defineComponent({
  setup() {
    const { tree: sections } = useSections();
    const { status, startCreation, sectionSelected, saveChanges } = sectionsStore;
    onMounted(() => {
      startCreation();
    });

    const selectedData = computed({
      get: () => sectionSelected.value?.data ?? null,
      set: (val) => {
        if (sectionSelected.value == null || val == null) {
          return;
        }
        sectionSelected.value = new Creation(val);
      },
    });

    const name = computed({
      get: () => selectedData.value?.name,
      set: (val) => {
        if (selectedData.value == null) {
          return;
        }
        const model = new SectionModel();
        Object.assign(model, { ...selectedData.value, name: val ?? '' });
        selectedData.value = model;
      },
    });
    const parentSectionId = computed({
      get: () => selectedData.value?.parentSectionId,
      set: (val) => {
        if (selectedData.value == null) {
          return;
        }
        const model = new SectionModel();
        Object.assign(model, { ...selectedData.value, parentSectionId: val ?? '' });
        selectedData.value = model;
      },
    });

    const selectedKeys = computed<Record<string, boolean> | undefined>({
      get: () =>
        parentSectionId.value == null ? undefined : { [`${parentSectionId.value}`]: true },
      set: (val) => {
        if (val == null) {
          parentSectionId.value = null;
          return;
        }
        Object.keys(val).forEach((key) => {
          if (val[key] === true) {
            parentSectionId.value = +key;
          }
        });
      },
    });

    const save = () => {
      saveChanges().then(() => {
        startCreation();
      });
    };

    return { sections, status, name, selectedKeys, save };
  },
});
</script>

<style scoped></style>
