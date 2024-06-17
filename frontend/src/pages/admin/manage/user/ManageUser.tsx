import { SearchUserForm, UserTable, userColumns } from "@/components";
import { Button } from "@/components/ui/button";
import { useAuth, useUsers } from "@/hooks";
import { usePagination } from "@/hooks/usePagination";
import { useState } from "react";
import { Outlet, useNavigate } from "react-router-dom";

export const ManageUser = () => {
  const { token } = useAuth();
  const { pageSize, pageNumber, onPaginationChange, pagination } =
    usePagination();
  const [search, setSearch] = useState("");
  const { users, loading, error } = useUsers(
    token!,
    pageNumber,
    pageSize,
    search,
  );
  const navigate = useNavigate();

  return (
    <div className="m-24 flex h-full w-2/3 flex-grow flex-col gap-8">
      <p className="text-2xl font-bold text-red-600">User List</p>
      <div className="flex items-center justify-between">
        <SearchUserForm setSearch={setSearch} />
        <Button
          variant={"destructive"}
          onClick={() => navigate("/admin/user/create-user")}
        >
          <span className="capitalize">Create new user</span>
        </Button>
      </div>
      {loading ? (
        <div>Loading...</div>
      ) : error ? (
        <div>Error</div>
      ) : (
        <UserTable
          columns={userColumns}
          data={users!}
          onPaginationChange={onPaginationChange}
          pagination={pagination}
        />
      )}
      <Outlet />
    </div>
  );
};
