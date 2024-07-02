import { AssignmentRes } from "@/models";
import { getAllAssignmentService } from "@/services/admin/manageAssignmentService";
import { useCallback, useEffect, useState } from "react";

export const useAssignments = (
  pagination: {
    pageSize: number;
    pageIndex: number;
  },
  search?: string,
  orderBy?: string,
  adminLocation?: number,
  isDescending?: boolean,
  assignmentState?: number,
  assignedDate?: string,
) => {
  const [assignments, setAssignments] = useState<AssignmentRes[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<boolean | null>(false);
  const [pageCount, setPageCount] = useState<number>(0);
  const [totalRecords, setTotalRecords] = useState<number>(0);

  // Function to fetch assignments
  const fetchAssignments = useCallback(async () => {
    setLoading(true);
    try {
      const data = await getAllAssignmentService({
        pagination,
        search,
        orderBy,
        isDescending,
        adminLocation,
        assignmentState,
        assignedDate,
      });

      setAssignments(data.data.data || []);
      setPageCount(data.data.totalPages);
      setTotalRecords(data.data.totalRecords);
      return data.data.data || [];
    } catch (error) {
      console.error("Error in fetchAssignments:", error);
      setError(true);
      return [];
    } finally {
      setLoading(false);
    }
  }, [
    pagination,
    search,
    orderBy,
    isDescending,
    adminLocation,
    assignmentState,
    assignedDate,
  ]);

  useEffect(() => {
    const fetchAndUpdateAssignments = async () => {
      setLoading(true);
      try {
        const isAdded = localStorage.getItem("added");
        const isEdited = localStorage.getItem("edited");

        // Always fetch the latest data
        const currentAssignments = await fetchAssignments();

        if (isAdded || isEdited) {
          const orderByField = isAdded
            ? "createdOn"
            : isEdited
              ? "lastModifiedOn"
              : orderBy;
          const newAssignmentData = await getAllAssignmentService({
            pagination,
            search,
            orderBy: orderByField,
            isDescending: true, // Ensure we get the most recent assignment
            adminLocation,
            assignmentState,
            assignedDate,
          });

          const newAssignment = newAssignmentData.data.data[0];
          if (newAssignment) {
            setAssignments([
              newAssignment,
              ...currentAssignments.filter(
                (assignment: AssignmentRes) =>
                  assignment.id !== newAssignment.id,
              ),
            ]);
          }

          localStorage.removeItem("added");
          localStorage.removeItem("edited");
        }
      } catch (error) {
        console.error("Error in fetchAndUpdateAssignments:", error);
        setError(true);
      } finally {
        setLoading(false);
      }
    };

    fetchAndUpdateAssignments();
  }, [
    pagination,
    search,
    orderBy,
    isDescending,
    adminLocation,
    assignmentState,
    assignedDate,
    fetchAssignments,
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
