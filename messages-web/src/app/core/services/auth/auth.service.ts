import { registerStore } from '@/store/register.store';
import { setUser } from '@/store/user.store';
import { UserManager, UserManagerSettings, WebStorageStateStore } from 'oidc-client-ts';
import { RouteLocationNormalizedLoaded } from 'vue-router';
import type { UserExtended } from '@/types/user';
import { token } from './local-storage.service';

const url: string = process.env.VUE_APP_KEYCLOACK_URL ?? '';
const realm: string = process.env.VUE_APP_KEYCLOACK_REALM ?? '';
const clientId: string = process.env.VUE_APP_KEYCLOACK_CLIENT_ID ?? '';

const settings: UserManagerSettings = {
  authority: `${url}/realms/${realm}/protocol/openid-connect/auth`,
  client_id: clientId,
  response_type: 'code',
  response_mode: 'query',
  redirect_uri: window.location.origin,
  post_logout_redirect_uri: window.location.origin,
  filterProtocolClaims: true,
  loadUserInfo: false,
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

async function init() {
  const user = await userManager.getUser();
  if (user == null) {
    try {
      const userFromCallback = await userManager.signinRedirectCallback();
      setUser(userFromCallback as UserExtended);
    } catch (e: unknown) {
      console.warn('User not found');
    }
  } else {
    setUser(user as UserExtended);
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
  await userManager.signinRedirect({ redirect_uri: redirectUri });
}

async function loginResourceOwnerCredentials(username: string, password: string) {
  const user = await userManager.signinResourceOwnerCredentials({ username, password });
  setUser(user as UserExtended);
}

async function logoutRedirect() {
  token.value = null;
  await userManager.signoutRedirect();
}

export const authService = {
  init,
  loginRedirect,
  loginResourceOwnerCredentials,
  logoutRedirect,
};
