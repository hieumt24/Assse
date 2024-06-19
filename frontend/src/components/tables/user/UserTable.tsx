import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";

import { FullPageModal } from "@/components/FullPageModal";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { Dialog, DialogContent } from "@/components/ui/dialog";
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
            <DialogContent className="max-w-[40%] p-[1px]">
              <div className="rounded-lg border-2 border-black p-0 text-lg shadow-lg">
                <h1 className="rounded-t-lg border-b-2 border-black bg-zinc-300 p-6 px-10 text-xl font-bold text-red-600">
                  Detailed User Information
                </h1>
                <div className="w-full px-10 py-4">
                  <table className="w-full">
                    <tr>
                      <td className="w-[40%]">Staff code</td>
                      <td>{userDetails?.staffCode}</td>
                    </tr>
                    <tr>
                      <td>Username</td>
                      <td>{userDetails?.username}</td>
                    </tr>
                    <tr>
                      <td>First name</td>
                      <td>{userDetails?.firstName}</td>
                    </tr>
                    <tr>
                      <td>Last name</td>
                      <td>{userDetails?.lastName}</td>
                    </tr>
                    <tr>
                      <td>Date of birth</td>
                      <td>
                        {userDetails?.dateOfBirth
                          ? format(userDetails?.dateOfBirth, "MM/dd/yyyy")
                          : ""}
                      </td>
                    </tr>
                    <tr>
                      <td>Joined date</td>
                      <td>
                        {userDetails?.joinedDate
                          ? format(userDetails?.joinedDate, "MM/dd/yyyy")
                          : ""}
                      </td>
                    </tr>
                    <tr>
                      <td>Gender</td>
                      <td>{userDetails?.gender === 2 ? "Male" : "Female"}</td>
                    </tr>
                    <tr>
                      <td>Role</td>
                      <td>{userDetails?.role === 1 ? "Admin" : "Staff"}</td>
                    </tr>
                    <tr>
                      <td>Location</td>
                      <td>
                        {userDetails?.location
                          ? LOCATIONS[userDetails.location - 1].label
                          : ""}
                      </td>
                    </tr>
                  </table>
                </div>
              </div>
            </DialogContent>
          )}
        </Dialog>
      </FullPageModal>
    </div>
  );
}
