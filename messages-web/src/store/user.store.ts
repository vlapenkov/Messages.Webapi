import { createStore } from '@harlem/core';
import { KeycloakTokenParsed } from 'keycloak-js';
import { IKeycloakToken } from './@types/IKeycloakToken';

export interface IUserStore {
  token: IKeycloakToken | null;
}

const defaultState: IUserStore = {
  token: null,
};

const { state: userState, getter, mutation } = createStore('user', defaultState);

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
      },
);

export const setToken = mutation('set-token', (state, token: KeycloakTokenParsed | null) => {
  state.token = token as IKeycloakToken | null;
});

export { userState };
