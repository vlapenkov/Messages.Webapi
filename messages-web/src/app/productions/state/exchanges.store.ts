import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { ExchangesState } from './exchanges.state';

const { computeState } = defineStore('product-exchanges', new ExchangesState());

const list = computeState((state) => state.list);

const status = computeState((state) => state.status);

export const exchangesStore = {
  list,
  status,
};
