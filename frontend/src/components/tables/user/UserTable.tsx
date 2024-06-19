import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";

import { FullPageModal } from "@/components/FullPageModal";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { Dialog, DialogContent } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Separator } from "@/components/ui/separator";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { LOCATIONS } from "@/constants";
import { useLoading } from "@/context/LoadingContext";
import { PaginationState, UserRes } from "@/models";
import { getUserByIdService } from "@/services";
import { format } from "date-fns";
import { Dispatch, SetStateAction, useState } from "react";
import { toast } from "react-toastify";
import Pagination from "../Pagination";

interface UserTableProps<TData, TValue> {
  columns: ColumnDef<TData, TValue>[];
  data: TData[];
  pagination: PaginationState;
  onPaginationChange: Dispatch<
    SetStateAction<{ pageSize: number; pageIndex: number }>
  >;
  pageCount?: number;
}

export function UserTable<TData, TValue>({
  columns,
  data,
  pagination,
  onPaginationChange,
  pageCount,
}: Readonly<UserTableProps<TData, TValue>>) {
  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
    manualPagination: true,
    state: { pagination },
    onPaginationChange,
    pageCount,
  });

  const [openDetails, setOpenDetails] = useState(false);
  const [userDetails, setUserDetails] = useState<UserRes>();

  const handleOpenDetails = async (id: string) => {
    setOpenDetails(true);
    try {
      setIsLoading(true);
      const result = await getUserByIdService(id);
      if (result.success) {
        setUserDetails(result.data.data);
      } else {
        toast.error(result.message);
      }
    } catch (error) {
      console.log(error);
      toast.error("Error fetching user details");
    } finally {
      setIsLoading(false);
    }
  };

  const { isLoading, setIsLoading } = useLoading();
  // Set the page directly in the table state
  const setPage = (pageIndex: number) => {
    onPaginationChange((prev) => ({
      ...prev,
      pageIndex: pageIndex,
    }));
  };

  return (
    <div>
      <div className="relative rounded-md border">
        <Table>
          <TableHeader className="bg-zinc-200 text-lg font-bold">
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => {
                  return (
                    <TableHead key={header.id}>
                      {header.isPlaceholder
                        ? null
                        : flexRender(
                            header.column.columnDef.header,
                            header.getContext(),
                          )}
                    </TableHead>
                  );
                })}
              </TableRow>
            ))}
          </TableHeader>
          <TableBody>
            {table.getRowModel().rows?.length ? (
              table.getRowModel().rows.map((row) => (
                <TableRow
                  key={row.id}
                  data-state={row.getIsSelected() && "selected"}
                  className="hover:cursor-pointer"
                  onClick={async () => handleOpenDetails(row.getValue("id"))}
                >
                  {row.getVisibleCells().map((cell) => (
                    <TableCell key={cell.id}>
                      {flexRender(
                        cell.column.columnDef.cell,
                        cell.getContext(),
                      )}
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell
                  colSpan={columns.length}
                  className="h-24 text-center"
                >
                  No results.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </div>
      <Pagination
        pageIndex={pagination.pageIndex + 1}
        pageCount={pageCount || 1}
        setPage={setPage}
        previousPage={table.previousPage} // Added
        getCanPreviousPage={table.getCanPreviousPage} // Added
        nextPage={table.nextPage} // Added
        getCanNextPage={table.getCanNextPage}
      />
      <FullPageModal show={openDetails}>
        <Dialog open={openDetails} onOpenChange={setOpenDetails}>
          {isLoading ? (
            <LoadingSpinner />
          ) : (
            <DialogContent className="p-6">
              <div className="text-2xl font-bold text-red-600">
                {userDetails?.staffCode}
              </div>
              <Separator />
              <div className="flex flex-col gap-2">
                <div>
                  <label htmlFor="username" className="font-medium">
                    Username
                  </label>
                  <Input id="username" value={userDetails?.username} readOnly />
                </div>
                <div>
                  <label htmlFor="firstName" className="font-medium">
                    First name
                  </label>
                  <Input
                    id="firstName"
                    value={userDetails?.firstName}
                    readOnly
                  />
                </div>
                <div>
                  <label htmlFor="lastName" className="font-medium">
                    Last name
                  </label>
                  <Input id="lastName" value={userDetails?.lastName} readOnly />
                </div>
                <div>
                  <label htmlFor="dateOfBirth" className="font-medium">
                    Date of birth
                  </label>
                  <Input
                    id="dateOfBirth"
                    value={
                      userDetails?.dateOfBirth
                        ? format(userDetails?.dateOfBirth, "yyyy/MM/dd")
                        : ""
                    }
                    readOnly
                  />
                </div>
                <div>
                  <label htmlFor="joinedDate" className="font-medium">
                    Joined date
                  </label>
                  <Input
                    id="joinedDate"
                    value={
                      userDetails?.joinedDate
                        ? format(userDetails?.joinedDate, "yyyy/MM/dd")
                        : ""
                    }
                    readOnly
                  />
                </div>
                <div>
                  <label htmlFor="gender" className="font-medium">
                    Gender
                  </label>
                  <Input
                    id="gender"
                    value={userDetails?.gender === 2 ? "Male" : "Female"}
                    readOnly
                  />
                </div>
                <div>
                  <label htmlFor="role" className="font-medium">
                    Role
                  </label>
                  <Input
                    id="role"
                    value={userDetails?.role === 1 ? "Admin" : "Staff"}
                    readOnly
                  />
                </div>
                <div>
                  <label htmlFor="location" className="font-medium">
                    Location
                  </label>
                  <Input
                    id="location"
                    value={
                      userDetails?.location
                        ? LOCATIONS[userDetails.location - 1].label
                        : ""
                    }
                    readOnly
                  />
                </div>
              </div>
            </DialogContent>
          )}
        </Dialog>
      </FullPageModal>
    </div>
  );
}
