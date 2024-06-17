import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";

import { FullPageModal } from "@/components/FullPageModal";
import { Button } from "@/components/ui/button";
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
import { PaginationState, UserRes } from "@/models";
import { getUserByIdService } from "@/services";
import { Dialog, DialogContent } from "@radix-ui/react-dialog";
import { format } from "date-fns";
import { Dispatch, SetStateAction, useState } from "react";

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
    var result = await getUserByIdService(id);
    setUserDetails(result.data.data);
  };

  return (
    <div>
      <div className="rounded-md border relative">
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
            {table.getRowModel().rows?.length ? (
              table.getRowModel().rows.map((row) => (
                <TableRow
                  key={row.id}
                  data-state={row.getIsSelected() && "selected"}
                  className="hover:cursor-pointer"
                  onClick={async ()=>handleOpenDetails(row.getValue("id"))}
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
          <DialogContent className="p-6 relative">
              <div className="font-bold text-2xl">{userDetails?.staffCode}</div>
              <Separator/>
              <table className="mt-2 text-xl">
                <tbody>
                  <tr>
                    <td className="w-[150px]">Username</td>
                    <td>{userDetails?.username}</td>
                  </tr>
                  <tr>
                    <td>First Name</td>
                    <td>{userDetails?.firstName}</td>
                  </tr>
                  <tr>
                    <td>Last Name</td>
                    <td>{userDetails?.lastName}</td>
                  </tr>
                  <tr>
                    <td>Date of Birth</td>
                    <td>{userDetails?.dateOfBirth ? format(userDetails?.dateOfBirth, "yyyy/MM/dd") : ""}</td>
                  </tr>
                  <tr>
                    <td>Joined date</td>
                    <td>{userDetails?.joinedDate ? format(userDetails?.joinedDate, "yyyy/MM/dd") : ""}</td>
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
                    <td>{userDetails?.location ? LOCATIONS[userDetails.location-1].label : ""}</td>
                  </tr>
                </tbody>
              </table>
          </DialogContent>
        </Dialog>
      </FullPageModal>
      
    </div>
  );
}
