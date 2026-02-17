import axios from "axios";

const apiClient = axios.create({
  baseURL: "https://localhost:44335",
});

apiClient.interceptors.request.use((config) => {
  const oidcStorage = localStorage.getItem(
    `oidc.user:https://localhost:44335:BookStore_React`
  );
  if (oidcStorage) {
    const user = JSON.parse(oidcStorage);
    if (user?.access_token) {
      config.headers.Authorization = `Bearer ${user.access_token}`;
    }
  }
  return config;
});

export default apiClient;