import { definePageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/pageable-collection.store';
import { productsHttpService } from '../infrastructure/products.http-service';
import { ProductShortModel } from '../models/product.model';
import { ProductShortsState } from './product-shorts.state';

const [productShortsStore, { computeState }] = definePageableCollectionStore(
  'products',
  ProductShortModel,
  ProductShortsState,
  productsHttpService,
);

export const sectionId = computeState((state) => state.sectionId);

export { productShortsStore };
