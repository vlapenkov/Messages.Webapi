import { keycloakToken } from '@/app/core/services/keycloak/keycloak.service';
import axios from 'axios';

export const http = axios.create({
  baseURL: process.env.VUE_APP_API_URL,
});
http.interceptors.request.use(
  (config) => {
    const token = keycloakToken.value;
    if (token != null && config.headers != null) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error),
);

export const fileStoreHttp = axios.create({
  baseURL: process.env.VUE_APP_FilE_STORE_URL,
});
http.interceptors.request.use(
  (config) => {
    const token = keycloakToken.value;
    if (token != null && config.headers != null) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error),
);
