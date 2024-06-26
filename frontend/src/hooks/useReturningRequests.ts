import { ReturningRequestRes } from "@/models";
import { getReturningRequest } from "@/services/admin/manageReturningRequestService";
import { useEffect, useState } from "react";

export const useReturningRequests = (
  pagination: {
    pageIndex: number;
    pageSize: number;
  },
  location?: number,
  returnStatus?: number,
  returnDate?: string,
  search?: string,
  orderBy?: string,
  isDescending?: boolean,
) => {
  const [requests, setRequests] = useState<ReturningRequestRes[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<boolean | null>(false);
  const [pageCount, setPageCount] = useState<number>(0);
  const [totalRecords, setTotalRecords] = useState<number>(0);

  const fetchRequests = async () => {
    // Check if localStorage have item
    const orderByLocalStorage = localStorage.getItem("orderBy");
    setLoading(true);
    try {
      const data = await getReturningRequest({
        pagination,
        search,
        orderBy: orderByLocalStorage ?? orderBy,
        isDescending,
        returnStatus,
        returnDate,
        location,
      });

      setRequests(data.data.data);
      setPageCount(data.data.totalPages);
      setTotalRecords(data.data.totalRecords);
      localStorage.removeItem("orderBy");
    } catch (error) {
      setError(true);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchRequests();
  }, [pagination, search, returnStatus, returnDate, orderBy, isDescending]);

  return {
    requests,
    loading,
    error,
    setRequests,
    pageCount,
    totalRecords,
    fetchRequests,
  };
};
