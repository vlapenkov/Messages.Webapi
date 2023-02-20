import { defineHttpService } from '@/app/core/services/http/define-http.service';

const { definePost } = defineHttpService({
  url: 'api/Account',
});

export interface IUserCredential {
  value: string;
  type: string;
  temporary: boolean;
}

export interface IUserAttributes {
  patronymic: string;
}

export interface ICreateUserRequest {
  firstName: string;
  lastName: string;
  email: string;
  username: string;
  credentials: IUserCredential[];
  groups: string[];
  attributes: IUserAttributes;
  enabled: boolean;
}

export interface ITokenResponse {
  accessToken: string;
  expiresIn: number;
  refreshExpiresIn: number;
  refreshToken: string;
  tokenType: string;
  notBeforePolicy: number;
  sessionState: string;
  scope: string;
}

const createUser = definePost<ITokenResponse, ICreateUserRequest>();

export const userHttpService = {
  createUser,
};
