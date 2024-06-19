import { UserRes } from "@/models";
import { getAllUserService } from "@/services";
import { useEffect, useState } from "react";

export const useUsers = (
  token: string,
  pagination: {
    pageIndex: number;
    pageSize: number;
  },
  search?: string,
  roleType?: number,
) => {
  const [users, setUsers] = useState<UserRes[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<boolean | null>(false);
  const [pageCount, setPageCount] = useState<number>(0);

  const fetchUsers = async () => {
    try {
      const data = await getAllUserService({
        token,
        pagination,
        search,
        roleType,
        adminLocation: 1,
      });

      setUsers(data.data.data);
      setPageCount(data.data.totalPages);
    } catch (error) {
      setError(true);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, [token, pagination.pageIndex, pagination.pageSize, search, roleType]);

  return { users, loading, error, setUsers, pageCount, fetchUsers };
};
