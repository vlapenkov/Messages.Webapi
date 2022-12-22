import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { exchangeHttpService } from '../infrastructure/exchange.http-service';
import { ExcahngeModel, IExchangeModel } from '../models/exchange.model';
import { exchangesStore } from '../state/exchanges.store';

async function load() {
  const response = await exchangeHttpService.get();
  exchangesStore.status.value = new DataStatus('loading');
  if (response.status === HttpStatus.Success && response.data != null) {
    const model = response.data.map((i) => {
      const parsedData = new ExcahngeModel();
      const parseSuccess = parsedData.fromResponse(i as IExchangeModel);
      if (parseSuccess) {
        return parsedData;
      }
      exchangesStore.status.value = new DataStatus('error');
      throw new Error('Не удалось преобразовать данные');
    });
    exchangesStore.list.value = model;
    exchangesStore.status.value = new DataStatus('loaded');
  }
}

export const exchangeService = {
  load,
};
