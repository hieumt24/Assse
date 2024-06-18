import { SearchUserForm, UserTable, userColumns } from "@/components";
import { FullPageModal } from "@/components/FullPageModal";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { useLoading } from "@/context/LoadingContext";
import { useAuth, useUsers } from "@/hooks";
import { usePagination } from "@/hooks/usePagination";
import { disableUserService } from "@/services";
import { useState } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

export const ManageUser = () => {
  const { token } = useAuth();
  const { pageSize, pageNumber, onPaginationChange, pagination } =
    usePagination();
  const [search, setSearch] = useState("");
  const { users, loading, error, pageCount, fetchUsers } = useUsers(
    token!,
    pageNumber,
    pageSize,
    search,
  );
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
      var res = await disableUserService(userIdToDisable);
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
        <LoadingSpinner />
      ) : error ? (
        <div>Error</div>
      ) : (
        <>
          <UserTable
            columns={userColumns({ handleOpenDisable })}
            data={users!}
            onPaginationChange={onPaginationChange}
            pagination={pagination}
            pageCount={pageCount}
          />
          <FullPageModal show={openDisable}>
            <Dialog open={openDisable} onOpenChange={setOpenDisable}>
              <DialogContent onClick={(e) => e.stopPropagation()}>
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
