import axiosInstance from "@/api/axiosInstance";
import { GetReturningRequestReq } from "@/models";

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
