import axios, { AxiosInstance } from "axios";

const axiosInstace: AxiosInstance = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
});

export default axiosInstace;
