import { HttpResult } from '@/app/core/handlers/http/results/base/http-result';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { Creation, Edititng } from '@/app/core/services/harlem/tools/not-valid-data';
import { productServiceHttpService } from '@/app/productions/infrastructure/product-service.http-service';
import { productWorkHttpService } from '@/app/productions/infrastructure/product-work.http-service';
import { productFullHttpService } from '../infrastructure/product-full.http-service';
import { ProductFullModel } from '../models/product-full.model';

import { ProductFullState } from './product-full.state';

export type ProductType = 'product' | 'service' | 'work';

const { computeState, action, mutation } = defineStore('product-full', new ProductFullState());

const product = computeState((state) => state.item);

const status = computeState((state) => state.status);

const selected = computeState((state) => state.itemSelected);

const getAsync = action<{ id: number; type: ProductType }>('get-async', async ({ id, type }) => {
  status.value = new DataStatus('loading');
  try {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    let response!: HttpResult<any>;
    switch (type) {
      case 'product':
        response = await productFullHttpService.get(id);
        break;
      case 'service':
        response = await productServiceHttpService.get(id);
        break;
      case 'work':
        response = await productWorkHttpService.get(id);
        break;
      default:
        break;
    }
    if (response.status === HttpStatus.Success && response.data != null) {
      const newModel = new ProductFullModel();
      const succesParsed = newModel.fromResponse(response.data);
      if (succesParsed) {
        product.value = newModel;
        status.value = new DataStatus('loaded');
      } else {
        status.value = new DataStatus('error', 'Произошла ошибка в сопоставлении данных продукта');
      }
    }
  } catch (error) {
    status.value = new DataStatus('error', 'Что-то пошло не так при загрузке продукта');
  }
});

const startCreation = mutation('begin-product-creation', (state) => {
  state.itemSelected = new Creation(new ProductFullModel());
});

const startEditing = mutation('begin-product-editing', (state) => {
  state.itemSelected = new Edititng(state.item.clone());
});

const saveChanges = action<ProductType>('save-changes', async (type) => {
  if (selected.value == null) {
    return;
  }
  switch (type) {
    case 'product':
      switch (selected.value.mode) {
        case 'create':
          await productFullHttpService.post(selected.value.data);
          break;
        case 'edit':
          await productFullHttpService.put(selected.value.data);
          break;
        default:
          break;
      }
      break;
    case 'service':
      switch (selected.value.mode) {
        case 'create':
          await productServiceHttpService.post(selected.value.data);
          break;
        default:
          break;
      }
      break;
    case 'work':
      switch (selected.value.mode) {
        case 'create':
          await productWorkHttpService.post(selected.value.data);
          break;
        default:
          break;
      }
      break;
    default:
      break;
  }
});

export const productFullStore = {
  product,
  status,
  selected,
  getAsync,
  startCreation,
  startEditing,
  saveChanges,
};
