import axiosInstace from "@/api/axiosInstance";
import { CreateAssetReq, CreateCategoryReq } from "@/models";

export const getAllCategoryService = () => {
  return axiosInstace
    .get("/categories")
    .then((res) => {
      return {
        success: true,
        message: "Categories fetched successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return {
        success: false,
        message: "Failed to fetch categories.",
        data: err,
      };
    });
};

export const createCategoryService = (req: CreateCategoryReq) => {
  return axiosInstace
    .post("/categories", req)
    .then((res) => {
      return {
        success: true,
        message: "Category created successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      console.log(err.response?.data);
      return {
        success: false,
        message: err.response?.data.message,
        data: err.response,
      };
    });
};

export const createAssetService = (req: CreateAssetReq) => {
  return axiosInstace
    .post("/assets", req)
    .then((res) => {
      return {
        success: true,
        message: "Asset created successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      console.log(err.response?.data);
      return {
        success: false,
        message: err.response?.data.message,
        data: err.response,
      };
    });
};
