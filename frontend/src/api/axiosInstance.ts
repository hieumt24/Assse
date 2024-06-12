import axios, { AxiosInstance } from "axios";

const axiosInstace: AxiosInstance = axios.create({
  baseURL: "http://localhost:5000/api/v1",
});

export default axiosInstace;
