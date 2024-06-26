import axiosInstance from "@/api/axiosInstance";
import { GetAssignemntReq, CreateAssignmentReq } from "@/models";
export const getAllAssignmentService = (req: GetAssignemntReq) => {
  return axiosInstance
    .post("/assignments/filter-assignments", req)
    .then((res) => {
      return {
        success: true,
        message: "Assignments fetched successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return {
        success: false,
        message: "Failed to fetch assignments.",
        data: err,
      };
    });
};

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
      return {
        success: false,
        message: "Failed to fetch assignments.",
        data: err,
      }
    });
};
