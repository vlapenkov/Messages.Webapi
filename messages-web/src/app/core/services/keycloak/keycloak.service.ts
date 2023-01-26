import { registerStore } from '@/store/register.store';
import { setToken } from '@/store/user.store';
import Keycloak, { KeycloakInitOptions, KeycloakTokenParsed } from 'keycloak-js';
import { watch } from 'vue';
import { RouteLocationNormalizedLoaded } from 'vue-router';
import { keycloakToken, keycloakTokenRefresh, userData } from './local-storage.service';

const keycloakInitOptions = {
  url: process.env.VUE_APP_KEYCLOACK_URL ?? '',
  realm: process.env.VUE_APP_KEYCLOACK_REALM ?? '',
  clientId: process.env.VUE_APP_KEYCLOACK_CLIENT_ID ?? '',
};

const tokenRefreshInterval =
  process.env.VUE_APP_KEYCLOACK_TOKEN_REFRESH_INTERVAL != null
    ? +process.env.VUE_APP_KEYCLOACK_TOKEN_REFRESH_INTERVAL
    : 0;

const keycloakInst = new Keycloak(keycloakInitOptions);

function refreshTokens() {
  userData.value =
    keycloakInst.tokenParsed == null ? null : JSON.stringify(keycloakInst.tokenParsed);
  keycloakToken.value = keycloakInst.token ?? null;
  keycloakTokenRefresh.value = keycloakInst.refreshToken ?? null;
}

function cleanTokens() {
  userData.value = null;
  keycloakToken.value = null;
  keycloakTokenRefresh.value = null;
}

/** Этот метод надо обязательно вызвать, но ровно один раз  */
export async function initKeycloak() {
  console.log('start init keycloak');
  watch(
    userData,
    (val) => {
      console.log('from watch, start parse json', val);

      const parsedToken = val != null ? (JSON.parse(val) as KeycloakTokenParsed) : val;
      // if (parsedToken != null && parsedToken.exp != null) {
      //   console.log('Token expires', new Date(parsedToken.exp * 1000));
      // }
      console.log('from watch', {
        parsedToken,
        userData: userData.value,
        keycloakToken: keycloakToken.value,
        keycloakTokenRefresh: keycloakTokenRefresh.value,
      });
      setToken(parsedToken);
    },
    {
      immediate: true,
    },
  );
  const initOptions: KeycloakInitOptions = {
    checkLoginIframe: false,
    onLoad: 'check-sso',
    token: keycloakToken.value != null ? keycloakToken.value : undefined,
    refreshToken: keycloakTokenRefresh.value != null ? keycloakTokenRefresh.value : undefined,
    enableLogging: true,
    // silentCheckSsoRedirectUri: `${window.location.origin}/silent-check-sso.html`,
  };
  console.log('initOptions', initOptions);

  const authSuccess = await keycloakInst.init(initOptions);
  console.log('authSuccess', authSuccess);

  if (!authSuccess) {
    console.log('cleanTokens');
    // cleanTokens();
  } else {
    console.log('refreshTokens and set interval');
    refreshTokens();
    console.log('tokenRefreshInterval', tokenRefreshInterval);
    setInterval(async () => {
      try {
        const success = await keycloakInst.updateToken(tokenRefreshInterval * 2);
        console.log('success', success);

        if (success) {
          console.log('success and refreshTokens', refreshTokens);
          refreshTokens();
        }
      } catch (error) {
        console.log('unsuccess and error', error);

        cleanTokens();
        setTimeout(() => {
          window.location.reload();
        }, 5000);
      }
    }, tokenRefreshInterval * 1000);
  }
}

export function login(route: RouteLocationNormalizedLoaded) {
  const { origin } = window.location;
  const { redirectToLocation: to } = registerStore;

  let redirectUri;
  if (route.name === 'register') {
    redirectUri = `${origin}/`;
  } else {
    redirectUri = `${origin}${to.value != null ? to.value.fullPath : route.fullPath}`;
    console.log(to.value?.fullPath, route.fullPath);
  }
  keycloakInst.login({
    redirectUri,
  });
}

export function logout() {
  cleanTokens();
  const { origin } = window.location;
  keycloakInst.logout({
    redirectUri: `${origin}/`,
  });
}
