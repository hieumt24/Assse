import { UserRes } from "@/models";
import { getAllUserService } from "@/services";
import { useEffect, useState } from "react";

export const useUsers = (
  pagination: {
    pageIndex: number;
    pageSize: number;
  },
  adminLocation: number,
  search?: string,
  roleType?: number,
  orderBy?: string,
  isDescending?: boolean,
) => {
  const [users, setUsers] = useState<UserRes[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<boolean | null>(false);
  const [pageCount, setPageCount] = useState<number>(0);
  const [totalRecords, setTotalRecords] = useState<number>(0);

  const fetchUsers = async () => {
    // Check if localStorage have item
    const orderByLocalStorage = localStorage.getItem("orderBy");
    setLoading(true);
    try {
      const data = await getAllUserService({
        pagination,
        search,
        roleType,
        adminLocation,
        orderBy: orderByLocalStorage ?? orderBy,
        isDescending,
      });

      setUsers(data.data.data);
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
    fetchUsers();
  }, [pagination, search, roleType, orderBy, isDescending]);

  return {
    users,
    loading,
    error,
    setUsers,
    pageCount,
    totalRecords,
    fetchUsers,
  };
};
