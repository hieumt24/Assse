import axios, { AxiosInstance } from "axios";

const axiosInstace: AxiosInstance = axios.create({
  // this is for vite
  // for node is process.env
  baseURL: import.meta.env.VITE_REACT_APP_API_URL,
});

export default axiosInstace;
