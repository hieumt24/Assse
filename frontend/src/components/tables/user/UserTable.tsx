import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";

import { FullPageModal } from "@/components/FullPageModal";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { Button } from "@/components/ui/button";
import { Dialog, DialogContent } from "@/components/ui/dialog";
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

interface UserTableProps<TData, TValue> {
  columns: ColumnDef<TData, TValue>[];
  data: TData[];
  pagination: PaginationState;
  onPaginationChange: Dispatch<
    SetStateAction<{ pageSize: number; pageIndex: number }>
  >;
}

export function UserTable<TData, TValue>({
  columns,
  data,
  pagination,
  onPaginationChange,
}: Readonly<UserTableProps<TData, TValue>>) {
  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
    manualPagination: true,
    state: { pagination },
    onPaginationChange,
  });

  const [openDetails, setOpenDetails] = useState(false);
  const [userDetails, setUserDetails] = useState<UserRes>();

  const handleOpenDetails = async (id: string) => {
    setOpenDetails(true);
    try {
      setIsLoading(true);
      var result = await getUserByIdService(id);
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

  return (
    <div>
      <div className="relative rounded-md border">
        <Table>
          <TableHeader>
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
            {table?.getRowModel().rows?.length ? (
              table?.getRowModel().rows.map((row) => (
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
      <div className="flex items-center justify-end space-x-2 py-4">
        <Button
          variant="outline"
          size="sm"
          onClick={() => table.previousPage()}
          disabled={!table.getCanPreviousPage()}
        >
          Previous
        </Button>
        <span className="bg-red-500 px-2 text-white">{`${pagination.pageIndex + 1}`}</span>
        <Button
          variant="outline"
          size="sm"
          onClick={() => table.nextPage()}
          disabled={!table.getCanNextPage()}
        >
          Next
        </Button>
      </div>
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
              <table className="text-xl">
                <tbody>
                  <tr>
                    <td className="w-[150px] font-medium">Username</td>
                    <td>{userDetails?.username}</td>
                  </tr>
                  <tr>
                    <td className="font-medium">First Name</td>
                    <td>{userDetails?.firstName}</td>
                  </tr>
                  <tr>
                    <td className="font-medium">Last Name</td>
                    <td>{userDetails?.lastName}</td>
                  </tr>
                  <tr>
                    <td className="font-medium">Date of Birth</td>
                    <td>
                      {userDetails?.dateOfBirth
                        ? format(userDetails?.dateOfBirth, "yyyy/MM/dd")
                        : ""}
                    </td>
                  </tr>
                  <tr>
                    <td className="font-medium">Joined date</td>
                    <td>
                      {userDetails?.joinedDate
                        ? format(userDetails?.joinedDate, "yyyy/MM/dd")
                        : ""}
                    </td>
                  </tr>
                  <tr>
                    <td className="font-medium">Gender</td>
                    <td>{userDetails?.gender === 2 ? "Male" : "Female"}</td>
                  </tr>
                  <tr>
                    <td className="font-medium">Role</td>
                    <td>{userDetails?.role === 1 ? "Admin" : "Staff"}</td>
                  </tr>
                  <tr>
                    <td className="font-medium">Location</td>
                    <td>
                      {userDetails?.location
                        ? LOCATIONS[userDetails.location - 1].label
                        : ""}
                    </td>
                  </tr>
                </tbody>
              </table>
            </DialogContent>
          )}
        </Dialog>
      </FullPageModal>
    </div>
  );
}
