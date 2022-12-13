import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { TreeNode } from 'primevue/tree';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { Creation } from '@/app/core/services/harlem/tools/not-valid-data';
import { SectionState } from './sections.state';
import { SectionModel } from '../models/section.model';
import { sectionsHttpService } from '../infrastructure/sections.http-service';

const { computeState, getter, mutation, action, onActionSuccess } = defineStore(
  'product-sections',
  new SectionState(),
);

const sections = computeState((state) => state.items);

const sectionSelected = computeState((state) => state.selectedItem);

const status = computeState((state) => state.status);

const treeView = getter('get-sections-tree', () => {
  const models = sections.value;
  if (models == null) {
    return null;
  }
  const modelToTree = (section: SectionModel): TreeNode => ({
    label: section.name as string,
    key: `${section.key}`,
    data: section,
    children: models.filter((m) => m.parentSectionId === section.id).map(modelToTree),
  });
  return models.filter((s) => s.parentSectionId == null || s.parentSectionId < 0).map(modelToTree);
});

const loadSections = action('get-sections', async () => {
  status.value = new DataStatus('loading');
  try {
    const { data, status: httpStatus, message } = await sectionsHttpService.get();
    if (httpStatus === HttpStatus.Success && data != null) {
      const models = data.map((m) => {
        const model = new SectionModel();
        const parsedSuccessful = model.fromResponse(m);
        if (parsedSuccessful) {
          return model;
        }
        const errorMessage = 'Ошибка преобразования данных к модели';
        status.value = new DataStatus('error', errorMessage);
        throw new Error(errorMessage);
      });
      status.value = new DataStatus('loaded');
      sections.value = models;
    } else {
      status.value = new DataStatus('error', message);
    }
  } catch (_) {
    status.value = new DataStatus('error', 'Что-то пошло не так при загрузке категорий');
  }
});

const startCreation = mutation('create-item', (state) => {
  state.selectedItem = new Creation(new SectionModel());
});

const saveChangesKey = 'save-changes';

const saveChanges = action(saveChangesKey, async () => {
  if (sectionSelected.value == null) {
    return;
  }
  const { data, mode } = sectionSelected.value;
  status.value = new DataStatus('updating');
  switch (mode) {
    case 'create':
      try {
        await sectionsHttpService.post({
          name: data.name,
          parentSectionId: data.parentSectionId,
        });
        status.value = new DataStatus('loaded');
      } catch (error) {
        console.error(error);
        status.value = new DataStatus(
          'error',
          'Что-то пошло не так в процессе добавления категории',
        );
      }
      break;
    default:
      throw new Error('Что-то пошло не так');
  }
});

onActionSuccess(saveChangesKey, () => {
  if (sectionSelected.value == null) {
    return;
  }
  const { mode } = sectionSelected.value;
  if (mode === 'create') {
    loadSections();
  }
  sectionSelected.value = null;
});

export const sectionsStore = {
  sections,
  treeView,
  status,
  loadSections,
  startCreation,
  saveChanges,
  sectionSelected,
};
