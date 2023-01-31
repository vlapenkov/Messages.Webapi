import axios from 'axios';
import { token } from '../../auth/local-storage.service';

export const http = axios.create({
  baseURL: process.env.VUE_APP_API_URL,
});
http.interceptors.request.use(
  (config) => {
    if (token != null && config.headers != null) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error),
);
