import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { ProductShortsState } from './productions.state';

const { getter, computeState } = defineStore('products', new ProductShortsState());

const pages = computeState((state) => state.pages);

const selectedItem = computeState((state) => state.selectedItem);

const selectedItemMode = getter('selected-item--mode', (state) => state.selectedItem?.mode ?? null);

export const productionsStore = {
  selectedItem,
  selectedItemMode,
  pages,
};
