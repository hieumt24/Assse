import { UserRes } from "@/models";
import { getAllUserService } from "@/services";
import { useEffect, useState } from "react";

export const useUsers = (
  token: string,
  pageNumber: number,
  pageSize: number,
) => {
  const [users, setUsers] = useState<UserRes[] | null>(null);
  const [isLoading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<boolean | null>(false);

  const fetchUsers = async () => {
    try {
      const data = await getAllUserService({ token, pageNumber, pageSize });
      setUsers(data.data.data);
    } catch (error) {
      setError(true);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, [token, pageNumber, pageSize]);

  return { users, isLoading, error, fetchUsers };
};
