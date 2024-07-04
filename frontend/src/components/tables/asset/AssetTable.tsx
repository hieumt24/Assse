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
import { useLoading } from "@/context/LoadingContext";
import { AssetRes, AssignmentRes, PaginationState } from "@/models";
import { getAssetByAssetCodeService, getAssignmentByAssetService } from "@/services";
import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";
import { Dispatch, SetStateAction, useState } from "react";
import { toast } from "react-toastify";
import Pagination from "../Pagination";
import { format } from "date-fns";
import { ASSET_STATES, LOCATIONS } from "@/constants";
import { usePagination } from "@/hooks";
import { AssignmentTable } from "../assignment/AssignmentTable";
import { assetAssignmentColumns } from "../assignment/assetAssignmentColumns";

interface AssetTableProps<TData, TValue> {
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

export function AssetTable<TData, TValue>({
  columns,
  data,
  pagination,
  onPaginationChange,
  pageCount,
  totalRecords,
  onRowClick,
}: Readonly<AssetTableProps<TData, TValue>>) {
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
  const [assetDetails, setAssetDetails] = useState<AssetRes>();
  const { onPaginationChange: assignmentsOnPaginationChange, pagination: assignmentsPagination } = usePagination();
  const [orderBy, setOrderBy] = useState("");
  const [isDescending, setIsDescending] = useState(true);
  const [assignments, setAssignments] = useState<Array<AssignmentRes>>([]);
  const [assignmentsPageCount, setAssignmentsPageCount] = useState<number>(0);
  const [assignmentsTotalRecords, setAssignmentsTotalRecords] = useState<number>(0);

  assignmentsPagination.pageSize = 3;

  const handleOpenDetails = async (assetCode: string) => {
    setOpenDetails(true);
    setIsLoading(true);
    const result = await getAssetByAssetCodeService(assetCode);
    if (result.success) {
      setAssetDetails(result.data);
    } else {
      toast.error(result.message);
    }

    const result1 = await getAssignmentByAssetService({
      pagination: assignmentsPagination,
      assetId: assetDetails?.id || "",
      orderBy,
      isDescending
    })
    if (result1.success) {
      setAssignments(result1.data.data || []);
      setAssignmentsPageCount(result1.data.totalPages);
      setAssignmentsTotalRecords(result1.data.totalRecords);
    } else {
      toast.error(result1.message);
    }
    setIsLoading(false);
    console.log(result1)
  };

  const { isLoading, setIsLoading } = useLoading();

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
                      : async () => handleOpenDetails(row.getValue("assetCode"))
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
            <DialogContent className="max-w-lg border-none p-0" title={"white"}>
              <div className="overflow-hidden rounded-lg bg-white shadow-lg">
                <h2 className="bg-red-600 p-6 text-xl font-semibold text-white">
                  Detailed Asset Information
                </h2>
                <div className="px-6 py-4">
                  <table className="w-full">
                    <tbody>
                      <tr className="border-b border-gray-200 last:border-b-0">
                        <td className="w-[160px] py-2 pr-4 font-medium text-gray-600">
                          Asset Code
                        </td>
                        <td className="py-2 text-gray-800">
                          {assetDetails?.assetCode}
                        </td>
                      </tr>
                      <tr className="border-b border-gray-200 last:border-b-0">
                        <td className="w-[160px] py-2 pr-4 font-medium text-gray-600">
                          Asset Name
                        </td>
                        <td className="py-2 text-gray-800">
                          {assetDetails?.assetName}
                        </td>
                      </tr>
                      <tr className="border-b border-gray-200 last:border-b-0">
                        <td className="w-[160px] py-2 pr-4 font-medium text-gray-600">
                          Specification
                        </td>
                        <td className="py-2 text-gray-800">
                          {assetDetails?.specification}
                        </td>
                      </tr>
                      <tr className="border-b border-gray-200 last:border-b-0">
                        <td className="w-[160px] py-2 pr-4 font-medium text-gray-600">
                          Installed Date
                        </td>
                        <td className="py-2 text-gray-800">
                          {assetDetails?.installedDate
                            ? format(assetDetails?.installedDate, "MM/dd/yyyy")
                            : ""}
                        </td>
                      </tr>
                      <tr className="border-b border-gray-200 last:border-b-0">
                        <td className="w-[160px] py-2 pr-4 font-medium text-gray-600">
                          State
                        </td>
                        <td className="py-2 text-gray-800">
                          {ASSET_STATES.find(
                            (state) => state.value === assetDetails?.state,
                          )?.label || ""}
                        </td>
                      </tr>
                      <tr className="border-b border-gray-200 last:border-b-0">
                        <td className="w-[160px] py-2 pr-4 font-medium text-gray-600">
                          Asset Location
                        </td>
                        <td className="py-2 text-gray-800">
                          {LOCATIONS.find(
                            (location) =>
                              location.value === assetDetails?.assetLocation,
                          )?.label || ""}
                        </td>
                      </tr>
                      <tr className="border-b border-gray-200 last:border-b-0">
                        <td className="w-[160px] py-2 pr-4 font-medium text-gray-600">
                          Category Name
                        </td>
                        <td className="py-2 text-gray-800">
                          {assetDetails?.categoryName}
                        </td>
                      </tr>
                      <tr>
                        <td colSpan={2} className="pt-2">
                        <AssignmentTable
                          columns={assetAssignmentColumns({
                            setOrderBy,
                            setIsDescending,
                            isDescending,
                            orderBy,
                          })}
                          data={assignments!}
                          pagination={assignmentsPagination}
                          onPaginationChange={assignmentsOnPaginationChange}
                          pageCount={assignmentsPageCount}
                          totalRecords={assignmentsTotalRecords}
                          withIndex={false}
                          onRowClick={()=>{}}
                        />
                        </td>
                      </tr>
                    </tbody>
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
