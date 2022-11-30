import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { IPagedRequest } from '@/app/core/services/http/@types/IPagedRequest';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { organizationsHttpService } from '../infrastructure/organization.http-service';
import { IOrganizationModel } from '../model/IOrganizationModel';
import { OrganizationModel } from '../model/organization.model';
import { organizationsStore } from '../state/organizations.store';

/**
 * В контроллере отсутствуют аргументы для пэйджинга
 */
async function loadPage(request: IPagedRequest) {
  organizationsStore.status.value = new DataStatus('loading');
  const response = await organizationsHttpService.getPage(request);
  if (response.status === HttpStatus.Success && response.data != null) {
    const model = response.data.rows.map((i) => {
      const parsedData = new OrganizationModel();
      const parseSuccess = parsedData.fromResponse(i as IOrganizationModel);
      if (parseSuccess) {
        return parsedData;
      }
      throw new Error('Не удалось преобразовать данные');
    });
    const newItem: IPagedResponse<OrganizationModel> = { ...response.data, rows: model };
    organizationsStore.status.value = new DataStatus('loaded');
    organizationsStore.insertPage(newItem);
  }
}

export const organizationsService = {
  loadPage,
};
