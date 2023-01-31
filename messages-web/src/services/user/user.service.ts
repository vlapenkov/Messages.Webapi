import { HttpResult } from '@/app/core/handlers/http/results/base/http-result';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { authService } from '@/app/core/services/auth/auth.service';
import { status } from '@/store/user.store';
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
    const { username, credentials } = request;
    const password = credentials.find((x) => x.type === 'password')?.value;
    if (password != null) {
      await authService.loginResourceOwnerCredentials(username, password);
    }
  }
}

export const userService = {
  createUser,
};
