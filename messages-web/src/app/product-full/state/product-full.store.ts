import { defineSingleItemStore } from '@/app/core/services/harlem/custom-stores/single-Item/single-item.store';
import { watch } from 'vue';
import { productFullService } from '../infrastructure/product-full.http-service';
import { ProductFullModel } from '../models/product-full.model';
import { ProductFullState } from './product-full.state';

const [productFullStore, { computeState }] = defineSingleItemStore(
  'product-full',
  ProductFullModel,
  ProductFullState,
  productFullService,
);

const selectedId = computeState((state) => state.id);

watch(
  selectedId,
  () => {
    throw new Error('Not Implemented!');
  },
  {
    immediate: true,
  },
);

export { productFullStore };
