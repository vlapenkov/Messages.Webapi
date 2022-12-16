import axios from 'axios';
import { keycloakToken } from '../../keycloak/local-storage.service';

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
