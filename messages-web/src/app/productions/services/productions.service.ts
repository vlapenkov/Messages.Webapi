import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { computed } from 'vue';
import { IproductionsPageRequest } from '../@types/IproductionsPageRequest';
import { productionsHttpService } from '../infrastructure/productions.http-service';
import { IProductionModel, ProductionModel } from '../models/production.model';
import { productionsStore } from '../state/productions.store';

async function loadPage(request: IproductionsPageRequest) {
  productionsStore.status.value = new DataStatus('loading');
  const response = await productionsHttpService.getPage(request);
  if (response.status === HttpStatus.Success && response.data != null) {
    const model = response.data.rows.map((i) => {
      const parsedData = new ProductionModel();
      const parseSuccess = parsedData.fromResponse(i as IProductionModel);
      if (parseSuccess) {
        return parsedData;
      }
      productionsStore.status.value = new DataStatus('error');
      throw new Error('Не удалось преобразовать данные');
    });
    const newItem: IPagedResponse<ProductionModel> = { ...response.data, rows: model };
    productionsStore.setPage(newItem);
    productionsStore.status.value = new DataStatus('loaded');
  }
}

const isProductInShoppingCart = (product: ProductionModel) =>
  computed(
    () => shoppingCartStore.items.value?.find((i) => i.productId === product.id) !== undefined,
  );

async function updateStatus(id: number, status: number) {
  const response = await productionsHttpService.updateStatus(id, status);
  if (response.statusText === 'OK') return true;
  return false;
}
