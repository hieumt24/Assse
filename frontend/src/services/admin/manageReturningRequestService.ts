import axiosInstance from "@/api/axiosInstance";
import { CreateReturningRequestReq, GetReturningRequestReq } from "@/models";

export const getReturningRequest = (req: GetReturningRequestReq) => {
  if (req.returnState === 0) {
    delete req.returnState;
  }

  if (req.returnedDate === "") {
    delete req.returnedDate;
  }

  return axiosInstance
    .post("/returnRequests/filter-return-requests", req)
    .then((res) => {
      return {
        success: true,
        message: "Requests fetched successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return {
        success: false,
        message: "Failed to fetch requests.",
        data: err,
      };
    });
};

export const createReturnRequest = (req: CreateReturningRequestReq) => {
  return axiosInstance
    .post("/returnRequests", req)
    .then((res) => {
      return {
        success: true,
        message: "Request created successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return {
        success: false,
        message: "Failed to create request.",
        data: err,
      };
    });
};
