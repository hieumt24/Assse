import { SearchForm, UserTable, userColumns } from "@/components";
import { FullPageModal } from "@/components/FullPageModal";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { Button } from "@/components/ui/button";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";

import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { LOCATIONS } from "@/constants";
import { useLoading } from "@/context/LoadingContext";
import { useAuth, useUsers } from "@/hooks";
import { usePagination } from "@/hooks/usePagination";
import { disableUserService } from "@/services";
import { useState } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

export const ManageUser = () => {
  const { token, user } = useAuth();
  const { onPaginationChange, pagination } = usePagination();
  const [search, setSearch] = useState("");
  const [orderBy, setOrderBy] = useState("");
  const [isDescending, setIsDescending] = useState(false);
  const [roleType, setRoleType] = useState(0);
  const { users, loading, error, pageCount, fetchUsers } = useUsers(
    token!,
    pagination,
    LOCATIONS.find((location) => location.label === user.location)?.value || 1,
    search,
    roleType,
    orderBy,
    isDescending,
  );
  console.log(user);
  console.log(LOCATIONS.find((location) => location.label === user.location));

  const navigate = useNavigate();
  const { setIsLoading } = useLoading();
  const [userIdToDisable, setUserIdToDisable] = useState<string>("");
  const handleOpenDisable = (id: string) => {
    setUserIdToDisable(id);
    setOpenDisable(true);
  };

  const handleDisable = async () => {
    try {
      setIsLoading(true);
      const res = await disableUserService(userIdToDisable);
      if (res.success) {
        toast.success(res.message);
      } else {
        toast.error(res.message);
      }
      fetchUsers();
      setOpenDisable(false);
    } catch (err) {
      console.log(err);
      toast.error("Error when disable user");
    } finally {
      setIsLoading(false);
    }
  };
  const [openDisable, setOpenDisable] = useState(false);
  return (
    <div className="m-24 flex flex-grow flex-col gap-8">
      <p className="text-2xl font-bold text-red-600">User List</p>
      <div className="flex items-center justify-between">
        <Select
          onValueChange={(value) => {
            setRoleType(parseInt(value));
          }}
        >
          <SelectTrigger className="w-32">
            <SelectValue placeholder="Type" />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="0">All</SelectItem>
            <SelectItem value="1">Admin</SelectItem>
            <SelectItem value="2">Staff</SelectItem>
          </SelectContent>
        </Select>

        <div className="flex justify-between gap-6">
          <SearchForm setSearch={setSearch} />
          <Button
            variant={"destructive"}
            onClick={() => navigate("/admin/user/create-user")}
          >
            <span className="capitalize">Create new user</span>
          </Button>
        </div>
      </div>
      {loading ? (
        <LoadingSpinner />
      ) : error ? (
        <div>Error</div>
      ) : (
        <>
          <UserTable
            columns={userColumns({
              handleOpenDisable,
              setOrderBy,
              setIsDescending,
              isDescending,
            })}
            data={users!}
            onPaginationChange={onPaginationChange}
            pagination={pagination}
            pageCount={pageCount}
          />
          <FullPageModal show={openDisable}>
            <Dialog open={openDisable} onOpenChange={setOpenDisable}>
              <DialogContent>
                <DialogHeader>
                  <DialogTitle className="text-center text-2xl font-bold text-red-600">
                    Are you sure?
                  </DialogTitle>
                  <DialogDescription className="text-center text-lg">
                    Do you want to disable this user
                  </DialogDescription>
                  <div className="flex items-center justify-center gap-4">
                    <Button variant={"destructive"} onClick={handleDisable}>
                      Yes
                    </Button>
                    <Button
                      variant="outline"
                      onClick={() => setOpenDisable(false)}
                    >
                      Cancel
                    </Button>
                  </div>
                </DialogHeader>
              </DialogContent>
            </Dialog>
          </FullPageModal>
        </>
      )}
      <Outlet />
    </div>
  );
};
