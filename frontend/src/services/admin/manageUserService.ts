import axiosInstance from "@/api/axiosInstance";
import { CreateUserReq, GetUserReq } from "@/models";

export const createUserService = (req: CreateUserReq) => {
  console.log(import.meta.env.BASE_URL);

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

export const getAllUserService = (req: GetUserReq) => {
  return axiosInstance
    .get(`/users?pageSize=${req.pageSize}&pageNumber=${req.pageNumber + 1}`, {
      headers: {
        Authorization: `Bearer ${req.token}`,
      },
    })
    .then((res) => {
      return {
        success: true,
        message: "Users fetched successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return { success: false, message: "Failed to fetch users.", data: err };
    });
};
