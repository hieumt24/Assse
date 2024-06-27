import { SearchForm } from "@/components";
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
import { useLoading } from "@/context/LoadingContext";
import { useAuth } from "@/hooks";
import { usePagination } from "@/hooks/usePagination";
import { useReturningRequests } from "@/hooks/useReturningRequests";
import { format } from "date-fns";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

export const ManageUser = () => {
  const { user } = useAuth();
  const { onPaginationChange, pagination } = usePagination();
  const [search, setSearch] = useState("");

  const [orderBy, setOrderBy] = useState("");
  const [isDescending, setIsDescending] = useState(true);
  const [requestState, setRequestState] = useState(0);
  const [returnedDate, setreturnedDate] = useState<Date>();
  const { requests, loading, error, pageCount, fetchRequests, totalRecords } =
    useReturningRequests(
      pagination,
      user.location,
      requestState,
      returnedDate ? format(returnedDate, "yyyy-MM-dd") : "",
      search,
      orderBy,
      isDescending,
    );

  const navigate = useNavigate();
  const { setIsLoading } = useLoading();
  const [userIdToDisable, setUserIdToDisable] = useState<string>("");
  const handleOpenDisable = (id: string) => {
    setUserIdToDisable(id);
    setOpenDisable(true);
  };

  const handleDisable = async () => {};
  const [openDisable, setOpenDisable] = useState(false);
  return (
    <div className="m-24 flex flex-grow flex-col gap-8">
      <p className="text-2xl font-bold text-red-600">Request List</p>
      <div className="flex items-center justify-between">
        <Select
          onValueChange={(value) => {
            setRequestState(parseInt(value));
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
          <SearchForm
            setSearch={setSearch}
            onSubmit={() => {
              onPaginationChange((prev) => ({
                ...prev,
                pageIndex: 1,
              }));
            }}
          />
          <Button
            variant={"destructive"}
            onClick={() => navigate("/users/create")}
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
          {/* <UserTable
            columns={userColumns({
              handleOpenDisable,
              setOrderBy,
              setIsDescending,
              isDescending,
              orderBy,
            })}
            data={requests!}
            onPaginationChange={onPaginationChange}
            pagination={pagination}
            pageCount={pageCount}
            totalRecords={totalRecords}
          /> */}
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
        </>
      )}
    </div>
  );
};
