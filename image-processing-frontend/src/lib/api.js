import axios from "axios";

export const publicApi = axios.create();

publicApi.interceptors.request.use((config) => {
  config.baseURL = `${import.meta.env.VITE_API_ENDPOINT}`;
  return config;
});

publicApi.interceptors.response.use(
  (response) => {
    return response.data;
  },
  (error) => {
    return Promise.reject(error);
  }
);
