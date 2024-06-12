import axiosInstance from "@/api/axiosInstance";
import { CreateUserReq } from "@/models";

export const createUserService = (req: CreateUserReq) => {
  return axiosInstance
    .post("/users", req)
    .then((res) => {
      return {
        success: true,
        message: "User created successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return { success: false, message: "Failed to create user.", error: err };
    });
};
