import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { Creation, Edititng, NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { organizationHttpService } from '../infrastructure/organozation-full.http-service';
import { OrganizationFullModel } from '../models/organozation-full.model';
import { OrganizationFullState } from './organization-full.state';

const { computeState, action, mutation } = defineStore(
  'organization-full',
  new OrganizationFullState(),
);

const status = computeState((state) => state.status);

const organization = computeState((state) => state.item);

const organizationSelected = computeState((state) => state.itemSelected);

const getDataAsync = action<number>('get-data-async', async (id) => {
  try {
    status.value = new DataStatus('loading');
    const response = await organizationHttpService.get(id);
    if (response.status === HttpStatus.Success && response.data != null) {
      const model = new OrganizationFullModel();
      const parsedSuccess = model.fromResponse(response.data);
      if (parsedSuccess) {
        organization.value = model;
        status.value = new DataStatus('loaded');
      } else {
        status.value = new DataStatus('error', 'Не удалось преобразовать данные из ответа');
      }
    } else {
      status.value = new DataStatus('error', response.message);
    }
  } catch (error) {
    status.value = new DataStatus('error', 'Что-то пошло не так при получении данных организации');
  }
});

const createItem = mutation('create-item', (state) => {
  state.itemSelected = new Creation(new OrganizationFullModel());
});

const selectItem = mutation('select-item', (state) => {
  state.itemSelected = new Edititng(new OrganizationFullModel());
});

const updateSelectedItem = mutation<OrganizationFullModel>(
  'update-selected-item',
  (state, data) => {
    if (state.itemSelected == null) return;
    state.itemSelected = new NotValidData(data, state.itemSelected.mode);
  },
);

const saveChanges = action('save-changes', async () => {
  if (organizationSelected.value == null) {
    return;
  }
  const { mode, data } = organizationSelected.value;

  switch (mode) {
    case 'create':
      status.value = new DataStatus('updating');
      try {
        const resp = await organizationHttpService.post(data);
        const copy = data.clone();
        copy.id = resp.data ?? 0;
        updateSelectedItem(copy);
        status.value = new DataStatus('loaded');
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
      } catch (e: any) {
        const { errors } = e.response.data;
        const errorList: string[][] = Object.values(errors);
        status.value = new DataStatus(
          'error',
          `Что-то пошло не так при добавлении организации`,
          errorList,
        );
      }
      break;
    default:
      throw new Error('Что-то пошло не так при сохранении изменеий');
  }
});

export const organizationFullStore = {
  status,
  organization,
  organizationSelected,
  getDataAsync,
  createItem,
  selectItem,
  updateSelectedItem,
  saveChanges,
};
