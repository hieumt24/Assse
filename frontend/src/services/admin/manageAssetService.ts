import axiosInstace from "@/api/axiosInstance";

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
