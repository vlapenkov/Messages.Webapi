import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { KeycloakTokenParsed } from 'keycloak-js';
import { IKeycloakToken } from './@types/IKeycloakToken';

export interface IUserStore {
  token: IKeycloakToken | null;
}

const defaultState: IUserStore = {
  token: null,
};

const { state: userState, getter, mutation } = defineStore('user', defaultState);

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
      },
);

export const setToken = mutation('set-token', (state, token: KeycloakTokenParsed | null) => {
  state.token = token as IKeycloakToken | null;
});

export { userState };
