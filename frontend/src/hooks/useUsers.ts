import { UserRes } from "@/models";
import { getAllUserService } from "@/services";
import { useEffect, useState } from "react";

export const useUsers = (
  token: string,
  pageNumber: number,
  pageSize: number,
  search?: string,
) => {
  const [users, setUsers] = useState<UserRes[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<boolean | null>(false);
  const [pageCount, setPageCount] = useState<number>(0);

  const fetchUsers = async () => {
    try {
      const data = await getAllUserService({
        token,
        pageNumber,
        pageSize,
        search,
      });
      console.log(data);

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
  }, [token, pageNumber, pageSize, search]);

  return { users, loading, error, setUsers, pageCount, fetchUsers };
};
