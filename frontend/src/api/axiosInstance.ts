import axios, { AxiosInstance } from "axios";

const axiosInstace: AxiosInstance = axios.create({
  // this is for vite
  // for node is process.env
  baseURL: import.meta.env.VITE_REACT_APP_API_URL,
});

axiosInstace.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);

export default axiosInstace;
