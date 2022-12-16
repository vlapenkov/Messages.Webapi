import { useStorage } from '@vueuse/core';

const userTokenKey = '__kc_token_parsed__';
const keycloakAuthTokenKey = '__kc_token_auth__';
const keycloakRefreshTokenKey = '__kc_token_refresh__';

export const userData = useStorage<string | null>(userTokenKey, null);
export const keycloakToken = useStorage<string | null>(keycloakAuthTokenKey, null);
export const keycloakTokenRefresh = useStorage<string | null>(keycloakRefreshTokenKey, null);
