import axiosInstance from "@/api/axiosInstance";
import { CreateAssignmentReq } from "@/models";

export const createAssignmentService = (req: CreateAssignmentReq) => {
  return axiosInstance
    .post("/assignments", req)
    .then((res) => {
      return {
        success: true,
        message: "Assignment created successfully!",
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
