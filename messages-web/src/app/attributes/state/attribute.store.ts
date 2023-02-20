import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { attributeHttpService } from '../infrastructure/attribute.http-service';
import { AttributeModel } from '../models/AttributeModel';
import { AttributeState } from './attribute.state';

const { computeState, action } = defineStore('attributes', new AttributeState());

const items = computeState((state) => state.items);
const status = computeState((state) => state.status);
const selectedItem = computeState((state) => state.selectedItem);

const getAsync = action('get-async', async () => {
  status.value = new DataStatus('loading');
  try {
    const response = await attributeHttpService.get();
    if (response.status === HttpStatus.Success && response.data != null) {
      const parsedAttrs = response.data.map((attr) => {
        const newModel = new AttributeModel();
        const succesParsed = newModel.fromResponse(attr);
        if (succesParsed) {
          return newModel;
        }
        const message = 'Произошла ошибка в сопоставлении данных продукта';
        status.value = new DataStatus('error', message);
        throw new Error(message);
      });
      items.value = parsedAttrs;
      status.value = new DataStatus('loaded');
    }
  } catch (error) {
    status.value = new DataStatus('error', 'Что-то пошло не так при загрузке продукта');
  }
});

export const attributeStore = { items, status, selectedItem, getAsync };
