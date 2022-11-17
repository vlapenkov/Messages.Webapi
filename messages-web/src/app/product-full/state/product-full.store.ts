import { defineSingleItemStore } from '@/app/core/services/harlem/custom-stores/single-Item/single-item.store';
import { productFullService } from '../infrastructure/product-full.http-service';
import { ProductFullModel } from '../models/product-full.model';
import { ProductFullState } from './product-full.state';

const [productFullStore, { computeState }] = defineSingleItemStore(
  'product-full',
  ProductFullModel,
  ProductFullState,
  productFullService,
);

export const selectedProductId = computeState((state) => state.id);

export { productFullStore };
