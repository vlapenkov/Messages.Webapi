import { HttpResult } from '@/app/core/handlers/http/results/base/http-result';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { status, setToken } from '@/store/user.store';
import { AxiosError } from 'axios';
import { ICreateUserRequest, ITokenResponse, userHttpService } from './user.http-service';

async function createUser(request: ICreateUserRequest) {
  status.value = new DataStatus('loading');

  let response: HttpResult<ITokenResponse> | undefined;
  try {
    response = await userHttpService.createUser(request);
  } catch (e: unknown) {
    if (e instanceof AxiosError) {
      if (e.response == null) return;
      const { errors } = e.response.data;
      const errorList: string[][] = Object.values(errors);
      status.value = new DataStatus(
        'error',
        `Что-то пошло не так при добавлении пользователя`,
        errorList,
      );
    }
    return;
  }

  if (response?.status === HttpStatus.Success && response?.data != null) {
    status.value = new DataStatus('loaded');
    setToken({
      scope: response.data.scope,
      sid: response.data.sessionState,
      email_verified: false,
      name: `${request.firstName} ${request.lastName}`,
      preferred_username: request.email,
      given_name: request.firstName,
      family_name: request.lastName,
      email: request.email,
      role: request.groups,
    });
  }
}

export const userService = {
  createUser,
};
