import axiosInstance from "@/api/axiosInstance";
import { LoginReq } from "@/models";

export const loginService = (req: LoginReq) => {
  return axiosInstance
    .post("/auth", req)
    .then((res) => {
      return {
        success: true,
        message: "Login successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return { success: false, message: "Failed to login.", error: err };
    });
};
