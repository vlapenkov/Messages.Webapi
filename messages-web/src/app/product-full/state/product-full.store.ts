import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { Creation, Edititng } from '@/app/core/services/harlem/tools/not-valid-data';
import { productFullService } from '../infrastructure/product-full.http-service';
import { ProductFullModel } from '../models/product-full.model';

import { ProductFullState } from './product-full.state';

const { computeState, action, mutation } = defineStore('product-full', new ProductFullState());

const product = computeState((state) => state.item);

const status = computeState((state) => state.status);

const selected = computeState((state) => state.itemSelected);

const getAsync = action<number>('get-async', async (id) => {
  status.value = new DataStatus('loading');
  try {
    const response = await productFullService.get(id);
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

export const productFullStore = {
  product,
  status,
  selected,
  getAsync,
  startCreation,
  startEditing,
};
