import { HttpResult } from '@/app/core/handlers/http/results/base/http-result';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import {
  keycloakToken,
  keycloakTokenRefresh,
  userData,
} from '@/app/core/services/keycloak/local-storage.service';
import { IKeycloakToken } from '@/store/@types/IKeycloakToken';
import { status } from '@/store/user.store';
import { AxiosError } from 'axios';
import { ICreateUserRequest, ITokenResponse, userHttpService } from './user.http-service';

function parseJwt(token: string): IKeycloakToken {
  const base64Url = token.split('.')[1];
  const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  const jsonPayload = decodeURIComponent(
    window
      .atob(base64)
      .split('')
      .map((c) => `%${`00${c.charCodeAt(0).toString(16)}`.slice(-2)}`)
      .join(''),
  );
  return JSON.parse(jsonPayload);
}

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
    const token = parseJwt(response.data.accessToken);

    keycloakToken.value = response.data.accessToken;
    keycloakTokenRefresh.value = response.data.refreshToken;
    userData.value = JSON.stringify(token);
  }
}

export const userService = {
  createUser,
};
