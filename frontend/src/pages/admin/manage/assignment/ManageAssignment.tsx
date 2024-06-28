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
  const [assignedDate, setAssignedDate] = useState<Date | undefined>(undefined);
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
    search,
    orderBy,
    user.location,
    isDescending,
    assignmentState,
    assignedDate!,
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
    alert("Not implemented");
    fetchAssignments();
  };

  const handleCreateRequest = async () => {
    try {
      setIsLoading(true);
      const res = await createReturnRequest({
        assignmentId: assignmentId,
        requestedBy: user.id,
        returnedDate: format(new Date(), "yyyy-MM-dd"),
        location: user.location,
      });
      if (res.success) {
        toast.success(res.message);
      } else {
        toast.error(res.message);
      }
      setOpenCreateRequest(false);
    } catch (err) {
      console.log(err);
      toast.error("Error when creating request.");
    } finally {
      setIsLoading(false);
    }
  };

  const navigate = useNavigate();
  return (
    <div className="m-16 flex h-full flex-grow flex-col gap-8">
      <p className="text-2xl font-bold text-red-600">Assignment List</p>
      <div className="flex items-center justify-between">
        <div className="flex items-center justify-center gap-4">
          <Select
            onValueChange={(value) => {
              setAssignmentState(Number(value));
              pagination.pageIndex = 1;
            }}
          >
            <SelectTrigger className="w-32">
              <SelectValue placeholder="State" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="0">All</SelectItem>
              <SelectItem value="1">Accepted</SelectItem>
              <SelectItem value="2">Waiting</SelectItem>
              <SelectItem value="3">Declined</SelectItem>
            </SelectContent>
          </Select>
          <DatePicker setValue={setAssignedDate} placeholder="Assigned Date" />
        </div>
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
            onClick={() => navigate("/assignments/create")}
          >
            <span className="capitalize">Create new assignment</span>
          </Button>
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
            desc="Do you want to delete this assignment"
            confirmText="Yes"
            onConfirm={handleDelete}
            open={openDelete}
            setOpen={setOpenDelete}
          />
          <GenericDialog
            title="Are you sure?"
            desc="Do you want to create a returning request for this asset?"
            confirmText="Yes"
            open={openCreateRequest}
            setOpen={setOpenCreateRequest}
            onConfirm={handleCreateRequest}
          />
        </>
      )}
    </div>
  );
};
