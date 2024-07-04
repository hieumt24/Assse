import { ReportTable, SearchForm, reportColumns } from "@/components";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { Button } from "@/components/ui/button";

import { useAuth, useReport } from "@/hooks";
import { usePagination } from "@/hooks/usePagination";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

export const Report = () => {
  const { user } = useAuth();
  const { onPaginationChange, pagination } = usePagination();
  const [search, setSearch] = useState("");

  const [orderBy, setOrderBy] = useState("");
  const [isDescending, setIsDescending] = useState(true);
  const { report, loading, error, pageCount, totalRecords } = useReport({
    pagination,
    search,
    adminLocation: user.location,
    orderBy,
    isDescending,
  });

  const navigate = useNavigate();

  return (
    <div className="m-16 flex flex-grow flex-col gap-8">
      <p className="text-2xl font-bold text-red-600">Report</p>
      <div className="flex items-center justify-end gap-6">
        <SearchForm
          setSearch={setSearch}
          onSubmit={() => {
            onPaginationChange((prev) => ({
              ...prev,
              pageIndex: 1,
            }));
          }}
          placeholder="Search by category"
          className="w-[300px]"
        />
        <Button
          variant={"destructive"}
          onClick={() => navigate("/users/create")}
        >
          <span className="capitalize">Export</span>
        </Button>
      </div>
      {loading ? (
        <LoadingSpinner />
      ) : error ? (
        <div>Error</div>
      ) : (
        <ReportTable
          columns={reportColumns({
            setOrderBy,
            setIsDescending,
            isDescending,
            orderBy,
          })}
          data={report!}
          onPaginationChange={onPaginationChange}
          pagination={pagination}
          pageCount={pageCount}
          totalRecords={totalRecords}
        />
      )}
    </div>
  );
};
