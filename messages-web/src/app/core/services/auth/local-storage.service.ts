import { useStorage } from '@vueuse/core';

const authTokenKey = '__token_auth__';

export const token = useStorage<string | null>(authTokenKey, null);
