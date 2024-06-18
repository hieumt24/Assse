import axiosInstance from "@/api/axiosInstance";
import { CreateUserReq, GetUserReq, UpdateUserReq } from "@/models";

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
      return { success: false, message: "Failed to create user.", data: err };
    });
};

export const getAllUserService = (req: GetUserReq) => {
  return axiosInstance
    .get(
      `/users?pageSize=${req.pageSize}&pageNumber=${req.pageNumber + 1}&search=${req.search ? req.search : ""}`,
      {
        headers: {
          Authorization: `Bearer ${req.token}`,
        },
      },
    )
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

export const getUserByIdService = (id: string) => {
  return axiosInstance
    .get(`/users/${id}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    })
    .then((res) => {
      return {
        success: true,
        message: "User fetched successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return { success: false, message: "Failed to fetch user.", data: err };
    });
};

export const getUserByStaffCodeService = (staffCode: string | undefined) => {
  return axiosInstance
    .get(`/users/staffCode/${staffCode}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    })
    .then((res) => {
      return {
        success: true,
        message: "User fetched successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return { success: false, message: "Failed to fetch user.", data: err };
    });
};

export const updateUserService = (req: UpdateUserReq) => {
  return axiosInstance
    .put(`/users`, req, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    })
    .then((res) => {
      return {
        success: true,
        message: "User updated successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return { success: false, message: "Failed to update user.", data: err };
    });
};
