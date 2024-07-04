import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";

import { FullPageModal } from "@/components/FullPageModal";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { DetailInformation } from "@/components/shared";
import { Dialog } from "@/components/ui/dialog";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { useLoading } from "@/context/LoadingContext";
import { PaginationState, UserRes } from "@/models";
import { getUserByIdService } from "@/services";
import { Dispatch, SetStateAction, useState } from "react";
import { toast } from "react-toastify";
import Pagination from "../Pagination";

interface UserTableProps<TData, TValue> {
  columns: ColumnDef<TData, TValue>[];
  data: TData[];
  pagination: PaginationState;
  onPaginationChange: Dispatch<
    SetStateAction<{
      pageSize: number;
      pageIndex: number;
    }>
  >;
  pageCount?: number;
  totalRecords: number;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  onRowClick?: any;
}

export function UserTable<TData, TValue>({
  columns,
  data,
  pagination,
  onPaginationChange,
  pageCount,
  totalRecords,
  onRowClick,
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
      console.log(result);
      if (result.success) {
        setUserDetails(result.data);
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
          <TableHeader className="bg-zinc-200 font-bold">
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
                  onClick={
                    onRowClick
                      ? () => {
                          onRowClick(row.original);
                        }
                      : async () => handleOpenDetails(row.getValue("id"))
                  }
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
        pageIndex={pagination.pageIndex}
        pageCount={pageCount || 1}
        setPage={setPage}
        totalRecords={totalRecords}
        pageSize={pagination.pageSize}
      />
      <FullPageModal show={openDetails}>
        <Dialog open={openDetails} onOpenChange={setOpenDetails}>
          {isLoading ? (
            <LoadingSpinner />
          ) : (
            <DetailInformation info={userDetails!} variant="User" />
          )}
        </Dialog>
      </FullPageModal>
    </div>
  );
}
