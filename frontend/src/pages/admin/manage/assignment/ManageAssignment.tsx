import {
  DatePicker,
  GenericDialog,
  SearchForm,
  assignmentColumns,
} from "@/components";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { AssignmentTable } from "@/components/tables/assignment/AssignmentTable";
import { Button } from "@/components/ui/button";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { useLoading } from "@/context/LoadingContext";
import { useAssignments, useAuth, usePagination } from "@/hooks";
import { deleteAssignmentService } from "@/services/admin/manageAssignmentService";
import { createReturnRequest } from "@/services/admin/manageReturningRequestService";
import { format } from "date-fns";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

export const ManageAssignment = () => {
  const { onPaginationChange, pagination } = usePagination();
  const [search, setSearch] = useState("");
  const [orderBy, setOrderBy] = useState("");
  const [isDescending, setIsDescending] = useState(true);
  const [assignmentState, setAssignmentState] = useState(0);
  const [assignedDateFrom, setAssignedDateFrom] = useState<Date | null>(null);
  const [assignedDateTo, setAssignedDateTo] = useState<Date | null>(null);
  const { user } = useAuth();

  const {
    assignments,
    loading,
    error,
    pageCount,
    totalRecords,
    fetchAssignments,
  } = useAssignments(
    pagination,
    search.trim(),
    orderBy,
    user.location,
    isDescending,
    assignmentState,
    assignedDateFrom ? format(assignedDateFrom, "yyyy-MM-dd") : "",
    assignedDateTo ? format(assignedDateTo, "yyyy-MM-dd") : "",
  );

  const { setIsLoading } = useLoading();
  const [openDelete, setOpenDelete] = useState(false);
  const [assignmentId, setAssignmentId] = useState<string>("");
  const [openCreateRequest, setOpenCreateRequest] = useState(false);
  const handleOpenDelete = (id: string) => {
    setAssignmentId(id);
    setOpenDelete(true);
  };
  const handleOpenCreateRequest = (id: string) => {
    setAssignmentId(id);
    setOpenCreateRequest(true);
  };

  const handleDelete = async () => {
    setIsLoading(true);
    const res = await deleteAssignmentService(assignmentId);
    if (res.success) {
      toast.success(res.message);
      fetchAssignments();
    } else {
      toast.error(res.message);
    }
    setOpenDelete(false);
    setIsLoading(false);
    fetchAssignments();
  };

  const handleCreateRequest = async () => {
    setIsLoading(true);
    const res = await createReturnRequest({
      assignmentId: assignmentId,
      requestedBy: user.id,
      location: user.location,
    });
    if (res.success) {
      toast.success(res.message);
      fetchAssignments();
    } else {
      toast.error(res.message);
    }
    setOpenCreateRequest(false);
    setIsLoading(false);
  };

  const navigate = useNavigate();
  return (
    <div className="m-16 flex flex-shrink-0 flex-col gap-8">
      <div className="flex items-center justify-between">
        <p className="text-2xl font-bold text-red-600">Assignment List</p>
        <Button
          variant={"destructive"}
          onClick={() => navigate("/assignments/create")}
        >
          <span className="capitalize">Create new assignment</span>
        </Button>
      </div>

      <div className="flex items-center justify-between gap-4">
        <div className="flex items-center justify-center gap-2">
          <Select
            onValueChange={(value) => {
              setAssignmentState(Number(value));
              pagination.pageIndex = 1;
            }}
          >
            <SelectTrigger className="min-w-24">
              <SelectValue placeholder="State" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="0">All states</SelectItem>
              <SelectItem value="1">Accepted</SelectItem>
              <SelectItem value="2">Waiting for acceptance</SelectItem>
              <SelectItem value="3">Declined</SelectItem>
              <SelectItem value="4">Waiting for returning</SelectItem>
            </SelectContent>
          </Select>
          <div className="flex items-center">
            From:&nbsp;
            <DatePicker
              setValue={setAssignedDateFrom}
              placeholder="Assigned Date"
              onChange={() => {
                pagination.pageIndex = 1;
                //if (assignedDateTo != null && assignedDateFrom != null && assignedDateFrom > assignedDateTo) setAssignedDateFrom(null);
              }}
              className="w-[150px]"
            />
          </div>
          <div className="flex items-center">
            To:&nbsp;
            <DatePicker
              setValue={setAssignedDateTo}
              placeholder="Assigned Date"
              onChange={() => {
                pagination.pageIndex = 1;
                //if (assignedDateTo != null && assignedDateFrom != null && assignedDateFrom > assignedDateTo) setAssignedDateTo(null);
              }}
              className="w-[150px]"
            />
          </div>
        </div>
        <div className="flex justify-between gap-4">
          <SearchForm
            setSearch={setSearch}
            onSubmit={() => {
              onPaginationChange((prev) => ({
                ...prev,
                pageIndex: 1,
              }));
            }}
            placeholder="Search by asset code, asset name, user assigned"
            className="w-[352px]"
          />
        </div>
      </div>

      {loading ? (
        <LoadingSpinner />
      ) : error ? (
        <div>Error</div>
      ) : (
        <>
          <AssignmentTable
            columns={assignmentColumns({
              handleOpenCreateRequest,
              handleOpenDelete,
              setOrderBy,
              setIsDescending,
              isDescending,
              orderBy,
            })}
            data={assignments!}
            pagination={pagination}
            onPaginationChange={onPaginationChange}
            pageCount={pageCount}
            totalRecords={totalRecords}
          />
          <GenericDialog
            title="Are you sure?"
            desc="Do you want to delete this assignment?"
            confirmText="Delete"
            onConfirm={handleDelete}
            open={openDelete}
            setOpen={setOpenDelete}
          />
          <GenericDialog
            title="Are you sure?"
            desc="Do you want to create a returning request for this asset?"
            confirmText="Yes"
            cancelText="No"
            open={openCreateRequest}
            setOpen={setOpenCreateRequest}
            onConfirm={handleCreateRequest}
          />
        </>
      )}
    </div>
  );
};
