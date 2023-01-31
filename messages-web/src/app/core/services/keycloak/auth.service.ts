import { parseJwt } from '@/services/user/user.service';
import { registerStore } from '@/store/register.store';
import { setToken } from '@/store/user.store';
import { KeycloakTokenParsed } from 'keycloak-js';
import { UserManager, UserManagerSettings, WebStorageStateStore } from 'oidc-client-ts';
import { RouteLocationNormalizedLoaded } from 'vue-router';
import { keycloakToken, keycloakTokenRefresh, userData } from './local-storage.service';

const url: string = process.env.VUE_APP_KEYCLOACK_URL ?? '';
const realm: string = process.env.VUE_APP_KEYCLOACK_REALM ?? '';
const clientId: string = process.env.VUE_APP_KEYCLOACK_CLIENT_ID ?? '';
// eslint-disable-next-line @typescript-eslint/no-unused-vars
const tokenRefreshInterval =
  process.env.VUE_APP_KEYCLOACK_TOKEN_REFRESH_INTERVAL != null
    ? +process.env.VUE_APP_KEYCLOACK_TOKEN_REFRESH_INTERVAL
    : 0;

const settings: UserManagerSettings = {
  authority: `${url}/realms/${realm}/protocol/openid-connect/auth`,
  client_id: clientId,
  response_type: 'code',
  response_mode: 'query',
  redirect_uri: window.location.origin,
  post_logout_redirect_uri: window.location.origin,
  filterProtocolClaims: true,
  loadUserInfo: false,
  // Tokens are stored only in memory, which is better from a security viewpoint
  userStore: new WebStorageStateStore({ store: window.localStorage }),
  // Store redirect state such as PKCE verifiers in session storage, for more reliable cleanup
  stateStore: new WebStorageStateStore({ store: sessionStorage }),
  metadata: {
    issuer: `${url}/realms/${realm}`,
    jwks_uri: `${url}/realms/${realm}/protocol/openid-connect/certs`,
    end_session_endpoint: `${url}/realms/${realm}/protocol/openid-connect/logout`,
    authorization_endpoint: `${url}/realms/${realm}/protocol/openid-connect/auth`,
    token_endpoint: `${url}/realms/${realm}/protocol/openid-connect/token`,
  },
};

const userManager = new UserManager(settings);

// eslint-disable-next-line @typescript-eslint/no-unused-vars
async function refreshTokens() {
  const user = await userManager.getUser();
  userData.value = user?.access_token == null ? null : JSON.stringify(parseJwt(user.access_token));
  keycloakToken.value = user?.access_token ?? null;
  keycloakTokenRefresh.value = user?.refresh_token ?? null;
}

function cleanTokens() {
  userData.value = null;
  keycloakToken.value = null;
  keycloakTokenRefresh.value = null;
}

async function loginCallback() {
  const user = await userManager.signinRedirectCallback();
  return user;
}

async function init() {
  const user = await userManager.getUser();
  console.log('user', user);

  if (user == null) {
    const userFromCallback = await loginCallback();
    console.log('user from callback', userFromCallback);
    console.log('state from callback', userFromCallback.state);

    setToken(userFromCallback.profile as KeycloakTokenParsed);
  } else {
    setToken(user?.profile as KeycloakTokenParsed);
  }
}

async function loginRedirect(route: RouteLocationNormalizedLoaded) {
  const { origin } = window.location;
  const { redirectToLocation: to } = registerStore;

  let redirectUri;
  if (route.name === 'register') {
    redirectUri = `${origin}/`;
  } else {
    redirectUri = `${origin}${to.value != null ? to.value.fullPath : route.fullPath}`;
  }
  await userManager.signinRedirect({ redirect_uri: redirectUri, state: { foo: 'bar' } });
}

async function loginResourceOwnerCredentials(username: string, password: string) {
  await userManager.signinResourceOwnerCredentials({ username, password });
}

async function logoutRedirect() {
  cleanTokens();
  await userManager.signoutRedirect();
}

export const authService = {
  init,
  loginRedirect,
  loginResourceOwnerCredentials,
  logoutRedirect,
  loginCallback,
};
