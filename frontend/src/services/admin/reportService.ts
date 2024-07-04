import axiosInstance from "@/api/axiosInstance";
import { GetReportReq } from "@/models";

export const getReportService = (req: GetReportReq) => {
  return axiosInstance
    .post("/reports", req)
    .then((res) => {
      return {
        success: true,
        message: "Report fetched successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return {
        success: false,
        message: err.response?.data.message || "Failed to fetch report.",
        data: err,
      };
    });
};
