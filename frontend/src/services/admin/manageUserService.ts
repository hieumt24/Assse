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
  // If roleType value equal 0, then show all
  req.roleType = req.roleType === "0" ? "" : req.roleType;
  return axiosInstance
    .post(
      `/users/filter-users?pageSize=${req.pageSize}&pageNumber=${req.pageNumber + 1}&search=${req.search ? req.search : ""}&roleType=${req.roleType ? req.roleType : ""}`,
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
    .get(`/users/${id}`)
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
    .get(`/users/staffCode/${staffCode}`)
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
    .put(`/users`, req)
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

export const disableUserService = (id: string) => {
  return axiosInstance
    .post(`/users/disable/${id}`)
    .then((res) => {
      return {
        success: true,
        message: "User disabled successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return { success: false, message: "Failed to disable user.", data: err };
    });
};
