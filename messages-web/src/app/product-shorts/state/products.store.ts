import { definePageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/pageable-collection.store';
import { productsHttpService } from '../infrastructure/products.http-service';
import { ProductShortModel } from '../models/product.model';
import { ProductsState } from './products.state';

const [productShortsStore, { computeState }] = definePageableCollectionStore(
  'products',
  ProductShortModel,
  ProductsState,
  productsHttpService,
);

export const sectionId = computeState((state) => state.sectionId);

export { productShortsStore };
