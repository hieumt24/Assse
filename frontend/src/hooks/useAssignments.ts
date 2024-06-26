import { AssignmentRes } from "@/models";
import { getAllAssignmentService } from "@/services/admin/manageAssignmentService";
import { useEffect, useState } from "react";

export const useAssignments = (
  pagination: {
    pageSize: number;
    pageIndex: number;
  },
  search?: string,
  orderBy?: string,
  adminLocation?: number,
  isDescending?: boolean,
  assignmentStatus?: number,
  assignedDate?: Date,
) => {
  const [assignments, setAssignments] = useState<AssignmentRes[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<boolean | null>(false);
  const [pageCount, setPageCount] = useState<number>(0);
  const [totalRecords, setTotalRecords] = useState<number>(0);

  const fetchAssignments = async () => {
    setLoading(true);
    try {
      const data = await getAllAssignmentService({
        pagination,
        search,
        orderBy,
        isDescending,
        adminLocation,
        assignmentStatus,
        assignedDate,
      });

      setAssignments(data.data.data);
      setPageCount(data.data.totalPages);
      setTotalRecords(data.data.totalRecords);
    } catch (error) {
      setError(true);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchAssignments();
  }, [
    pagination,
    search,
    orderBy,
    isDescending,
    adminLocation,
    assignedDate,
    assignmentStatus,
  ]);

  return {
    assignments,
    loading,
    error,
    setAssignments,
    pageCount,
    totalRecords,
    fetchAssignments,
  };
};
