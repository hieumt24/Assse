import { GenericDialog, SearchForm } from "@/components";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";

import { ReturningRequestTable } from "@/components/tables/returningRequest/ReturningRequestTable";
import { returningRequestColumns } from "@/components/tables/returningRequest/returningRequestColumns";
import { useAuth } from "@/hooks";
import { usePagination } from "@/hooks/usePagination";
import { useReturningRequests } from "@/hooks/useReturningRequests";
import { format } from "date-fns";
import { useState } from "react";

export const ManageReturningRequest = () => {
  const { user } = useAuth();
  const { onPaginationChange, pagination } = usePagination();
  const [search, setSearch] = useState("");

  const [orderBy, setOrderBy] = useState("");
  const [isDescending, setIsDescending] = useState(true);
  const [requestState, setRequestState] = useState(0);
  const [returnedDate] = useState<Date>();
  const { requests, loading, error, pageCount, totalRecords } =
    useReturningRequests(
      pagination,
      user.location,
      requestState,
      returnedDate ? format(returnedDate, "yyyy-MM-dd") : "",
      search,
      orderBy,
      isDescending,
    );
  const [requestId, setRequestId] = useState<string>("");
  const handleOpenCancel = (id: string) => {
    setRequestId(id);
    setOpenCancel(true);
  };
  const handleOpenComplete = (id: string) => {
    setRequestId(id);
    setOpenComplete(true);
  };

  const handleCancel = async () => {};
  const handleComplete = async () => {
    console.log(requestId);
  };
  const [openCancel, setOpenCancel] = useState(false);
  const [openComplete, setOpenComplete] = useState(false);

  return (
    <div className="m-16 flex flex-grow flex-col gap-8">
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
            <SelectItem value="1">Completed</SelectItem>
            <SelectItem value="2">Waiting</SelectItem>
            {/* <SelectItem value="3">Cancelled</SelectItem> */}
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
        </div>
      </div>
      {loading ? (
        <LoadingSpinner />
      ) : error ? (
        <div>Error</div>
      ) : (
        <>
          <ReturningRequestTable
            columns={returningRequestColumns({
              handleOpenComplete,
              handleOpenCancel,
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
          />

          <GenericDialog
            title="Are you sure?"
            desc="Do you want to cancel this request?"
            confirmText="Yes"
            open={openCancel}
            setOpen={setOpenCancel}
            onConfirm={handleCancel}
          />
          <GenericDialog
            title="Are you sure?"
            desc="Do you want to mark this returning as 'Completed'?"
            confirmText="Yes"
            open={openComplete}
            setOpen={setOpenComplete}
            onConfirm={handleComplete}
          />
        </>
      )}
    </div>
  );
};
