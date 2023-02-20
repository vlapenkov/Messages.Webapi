import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { IExchangeModel } from '../models/exchange.model';

const { defineGet } = defineHttpService({
  url: 'api/productions/exchanges',
});

const get = defineGet<IExchangeModel[], void>();

export const exchangeHttpService = {
  get,
};
