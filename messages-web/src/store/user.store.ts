import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { IOrganizationFullModel } from '@/app/organization-full/@types/IOrganizationFullModel';
import { organizationHttpService } from '@/app/organization-full/infrastructure/organozation-full.http-service';
import { KeycloakTokenParsed } from 'keycloak-js';
import { IKeycloakToken } from './@types/IKeycloakToken';

export interface IUserStore {
  token: IKeycloakToken | null;
  org: IOrganizationFullModel | null;
  status: DataStatus;
}

const setTokenKey = 'set-token';

const defaultState: IUserStore = {
  token: null,
  org: null,
  status: new DataStatus(),
};

const {
  state: userState,
  getter,
  mutation,
  onMutationSuccess,
  action,
  computeState,
} = defineStore('user', defaultState);

export const isAuthenticated = getter('is-auth', (state) => state.token != null);

export const userInfo = getter('get-user-info', (state) =>
  state.token == null
    ? null
    : {
        name: state.token.name,
        givenName: state.token.given_name,
        familyName: state.token.family_name,
        email: state.token.email,
        accountName: state.token.preferred_username,
        inn: state.token.inn,
        role: state.token.role,
        org: state.org,
      },
);

export const setToken = mutation(setTokenKey, (state, token: KeycloakTokenParsed | null) => {
  state.token = token as IKeycloakToken | null;
});

export const organization = computeState((state) => state.org);
export const status = computeState((state) => state.status);

const getOrganization = action<string>('get-user-organization', async (inn) => {
  status.value = new DataStatus('loading');
  const org = await organizationHttpService.getByInn(inn);
  if (org.status === HttpStatus.Success && org.data != null) {
    organization.value = org.data;
  }
  status.value = new DataStatus('loaded');
});

onMutationSuccess(setTokenKey, (p) => {
  if (p?.payload?.inn != null && status.value.status !== 'loading') {
    getOrganization(p.payload.inn);
  }
});

export { userState };

