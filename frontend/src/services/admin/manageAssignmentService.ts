import axiosInstance from "@/api/axiosInstance";
import { CreateAssignmentReq, GetAssignemntReq } from "@/models";
export const getAllAssignmentService = (req: GetAssignemntReq) => {
  if (req.assignmentState === 0) {
    delete req.assignmentState;
  }

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

export const getAssignmentByIdService = (id: string) => {
  return axiosInstance
    .get(`/assignments/${id}`)
    .then((res) => {
      return {
        success: true,
        message: "Assignment fetched successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return {
        success: false,
        message: "Failed to fetch assignment.",
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
        message: "Failed to create assignment.",
        data: err,
      };
    });
};
