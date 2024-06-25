import { AssetRes } from "@/models";
import { getAllAssestService } from "@/services/";
import { useEffect, useState } from "react";

export const useAssets = (
  pagination: {
    pageIndex: number;
    pageSize: number;
  },
  adminLocation: number,
  search?: string,
  orderBy?: string,
  isDescending?: boolean,
  assetStateType?: Array<number>,
  categoryId?: string,
) => {
  const [assets, setAssets] = useState<AssetRes[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<boolean | null>(false);
  const [pageCount, setPageCount] = useState<number>(0);
  const [totalRecords, setTotalRecords] = useState<number>(0);

  const fetchAssets = async () => {
    // Check if localStorage have item
    const orderByLocalStorage = localStorage.getItem("orderBy");
    setLoading(true);
    try {
      const data = await getAllAssestService({
        pagination,
        search,
        orderBy: orderByLocalStorage ?? orderBy,
        isDescending,
        adminLocation,
        assetStateType,
        categoryId,
      });

      setAssets(data.data.data);
      setPageCount(data.data.totalPages);
      setTotalRecords(data.data.totalRecords);
      localStorage.removeItem("orderBy");
    } catch (error) {
      setError(true);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    console.log("use asset");
    fetchAssets();
  }, [pagination, search, orderBy, isDescending, assetStateType, categoryId]);

  return {
    assets,
    loading,
    error,
    setAssets,
    pageCount,
    totalRecords,
    fetchAssets,
  };
};
