import { registerStore } from '@/store/register.store';
import { setToken } from '@/store/user.store';
import Keycloak, { KeycloakTokenParsed } from 'keycloak-js';
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

// function setKeycloakToken(token: string, refreshToken: string) {
//   localStorage.setItem('vue-token', token);
//   localStorage.setItem('vue-refresh-token', refreshToken);
// }

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
  watch(
    userData,
    (val) => {
      const parsedToken = val != null ? (JSON.parse(val) as KeycloakTokenParsed) : val;
      // if (parsedToken != null && parsedToken.exp != null) {
      //   console.log('Token expires', new Date(parsedToken.exp * 1000));
      // }
      setToken(parsedToken);
    },
    {
      immediate: true,
    },
  );
  const authSuccess = await keycloakInst.init({
    checkLoginIframe: false,
    onLoad: 'check-sso',
    // silentCheckSsoRedirectUri: `${window.location.origin}/silent-check-sso.html`,
  });
  if (!authSuccess) {
    cleanTokens();
  } else {
    refreshTokens();
    setInterval(async () => {
      try {
        const success = await keycloakInst.updateToken(tokenRefreshInterval * 2);
        if (success) {
          refreshTokens();
        }
      } catch (error) {
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
