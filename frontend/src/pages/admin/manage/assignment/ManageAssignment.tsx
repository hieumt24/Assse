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
import { deleteAssetByIdService } from "@/services";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

export const ManageAssignment = () => {
  const { onPaginationChange, pagination } = usePagination();
  const [search, setSearch] = useState("");
  const [orderBy, setOrderBy] = useState("");
  const [isDescending, setIsDescending] = useState(true);
  const [assignmentState, setAssignmentState] = useState(0);
  const [assignedDate, setAssignedDate] = useState<Date | null>(null);
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
  const [openDisable, setOpenDisable] = useState(false);
  const [assignmentIdToDelete, setAssignmentIdToDelete] = useState<string>("");
  const handleOpenDisable = (id: string) => {
    setAssignmentIdToDelete(id);
  };

  const handleDelete = async () => {
    try {
      setIsLoading(true);
      // TODO : Need change the function into delete assignment not asset
      const res = await deleteAssetByIdService(assignmentIdToDelete);
      if (res.success) {
        toast.success(res.message);
      } else {
        toast.error(res.message);
      }
      fetchAssignments();
      setOpenDisable(false);
    } catch (err) {
      console.log(err);
      toast.error("Error when disable user");
    } finally {
      setIsLoading(false);
    }
  };

  const navigate = useNavigate();
  return (
    <div className="m-24 flex h-full flex-grow flex-col gap-8">
      <p className="text-2xl font-bold text-red-600">Assignment List</p>
      <div className="flex items-center justify-between">
        <div className="flex items-center justify-center gap-4">
          <Select
            onValueChange={(value) => {
              setAssignmentState(Number(value));
            }}
          >
            <SelectTrigger className="w-32">
              <SelectValue placeholder="State" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="0">All</SelectItem>
              <SelectItem value="1">Accepted</SelectItem>
              <SelectItem value="2">Waiting</SelectItem>
            </SelectContent>
          </Select>
          <DatePicker setValue={setAssignedDate} />
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
              handleOpenDisable,
              setOrderBy,
              setIsDescending,
              isDescending,
              orderBy,
              requestedBy: user.id,
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
            open={openDisable}
            setOpen={setOpenDisable}
          />
        </>
      )}
    </div>
  );
};
