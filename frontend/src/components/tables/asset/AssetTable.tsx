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
import { ASSET_STATES, LOCATIONS } from "@/constants";
import { useLoading } from "@/context/LoadingContext";
import { AssetRes, PaginationState } from "@/models";
import { getAssetByAssetCodeService } from "@/services";
import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";
import { format } from "date-fns";
import { Dispatch, SetStateAction, useState } from "react";
import { toast } from "react-toastify";
import Pagination from "../Pagination";

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
}

export function AssetTable<TData, TValue>({
  columns,
  data,
  pagination,
  onPaginationChange,
  pageCount,
  totalRecords,
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

  const handleOpenDetails = async (assetCode: string) => {
    setOpenDetails(true);
    try {
      setIsLoading(true);
      const result = await getAssetByAssetCodeService(assetCode);
      console.log(result.data);
      if (result.success) {
        setAssetDetails(result.data);
      } else {
        toast.error(result.message);
      }
    } catch (error) {
      console.log(error);
      toast.error("Error fetching asset details");
    } finally {
      setIsLoading(false);
    }
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
                  onClick={async () =>
                    handleOpenDetails(row.getValue("assetCode"))
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
      />
      <FullPageModal show={openDetails}>
        <Dialog open={openDetails} onOpenChange={setOpenDetails}>
          {isLoading ? (
            <LoadingSpinner />
          ) : (
            <DialogContent className="max-w-[40%] border-none p-0">
              <div className="rounded-lg p-0 text-lg shadow-lg">
                <h1 className="rounded-t-lg bg-zinc-300 p-6 px-16 text-xl font-bold text-red-600">
                  Detailed Asset Information
                </h1>
                <div className="w-full px-16 py-6">
                  <table className="w-full">
                    <tr>
                      <td className="w-[40%] font-semibold">Asset Code</td>
                      <td>{assetDetails?.assetCode}</td>
                    </tr>
                    <tr>
                      <td className="font-semibold">Asset Name</td>
                      <td>{assetDetails?.assetName}</td>
                    </tr>
                    <tr>
                      <td className="font-semibold">Category</td>
                      <td>{assetDetails?.categoryName}</td>
                    </tr>
                    <tr>
                      <td className="font-semibold">State</td>
                      <td>
                        {
                          ASSET_STATES.find(
                            (state) => state.value === assetDetails?.state,
                          )?.label
                        }
                      </td>
                    </tr>
                    <tr>
                      <td className="font-semibold">Location</td>
                      <td>
                        {
                          LOCATIONS.find(
                            (location) =>
                              location.value === assetDetails?.assetLocation,
                          )?.label
                        }
                      </td>
                    </tr>
                    <tr>
                      <td className="font-semibold">Installed Date</td>
                      <td>
                        {assetDetails?.installedDate
                          ? format(assetDetails?.installedDate, "MM/dd/yyyy")
                          : ""}
                      </td>
                    </tr>
                    <tr>
                      <td className="font-semibold">Specification</td>
                      <td>{assetDetails?.specification}</td>
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
