import React from "react";
import { MdKeyboardArrowLeft, MdKeyboardArrowRight } from "react-icons/md";
import { Button } from "../ui/button";

interface PaginationProps {
  pageIndex: number;
  pageCount: number;
  previousPage: () => void; // Added prop
  getCanPreviousPage: () => boolean; // Added prop
  nextPage: () => void; // Added prop
  getCanNextPage: () => boolean; // Added prop
  setPage: (pageIndex: number) => void;
}
const Pagination: React.FC<PaginationProps> = ({
  pageIndex,
  pageCount,
  previousPage,
  getCanPreviousPage,
  nextPage,
  getCanNextPage,
  setPage,
}) => {
  const getPaginationNumbers = () => {
    const pageNumbers: (number | string)[] = [];

    if (pageCount <= 7) {
      for (let i = 1; i <= pageCount; i++) {
        pageNumbers.push(i);
      }
    } else {
      if (pageIndex > 3) {
        pageNumbers.push(1);
        if (pageIndex > 4) {
          pageNumbers.push("...");
        }
      }

      for (
        let i = Math.max(1, pageIndex - 2);
        i <= Math.min(pageIndex + 2, pageCount);
        i++
      ) {
        pageNumbers.push(i);
      }

      if (pageIndex < pageCount - 3) {
        if (pageIndex < pageCount - 4) {
          pageNumbers.push("...");
        }
        pageNumbers.push(pageCount);
      }
    }

    return pageNumbers;
  };

  return (
    <div className="flex items-center justify-end space-x-2 py-4">
      <Button
        variant="destructive"
        size="sm"
        onClick={previousPage}
        disabled={!getCanPreviousPage()}
        className="px-1"
      >
        <MdKeyboardArrowLeft size={24} />
      </Button>
      {getPaginationNumbers().map((page, idx) => (
        <button
          key={idx}
          className={`rounded-md border px-3 py-1 transition-all ${
            page === pageIndex ? "bg-red-500 text-white" : "border-gray-300"
          } ${typeof page === "number" ? "hover:bg-red-400" : "cursor-default"}`}
          onClick={() => typeof page === "number" && setPage(page - 1)}
          disabled={typeof page !== "number"}
        >
          {page}
        </button>
      ))}
      <Button
        variant="destructive"
        size="sm"
        onClick={nextPage}
        disabled={!getCanNextPage()}
        className="px-1"
      >
        <MdKeyboardArrowRight size={24} />
      </Button>
    </div>
  );
};

export default Pagination;
