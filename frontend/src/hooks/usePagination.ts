import { useState } from "react";

export const usePagination = () => {
  const [pagination, setPagination] = useState({
    pageSize: 10,
    pageIndex: 0,
  });

  const { pageIndex, pageSize } = pagination;

  return {
    pageSize: pageSize,
    pageNumber: pageIndex,
    onPaginationChange: setPagination,
    pagination,
  };
};
