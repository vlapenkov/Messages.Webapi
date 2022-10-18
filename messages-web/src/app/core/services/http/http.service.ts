import axios from 'axios';
import { keycloakToken } from '../../../services/keycloak.service';

export const httpApi = axios.create({
  baseURL: process.env.VUE_APP_API_URL,
});
httpApi.interceptors.request.use(
  (config) => {
    const token = keycloakToken.value;
    if (token != null && config.headers != null) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error),
);
