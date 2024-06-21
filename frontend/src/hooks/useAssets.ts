import { AssetRes } from "@/models";
import { getAllAssestService } from "@/services/";
import { useEffect, useState } from "react";

export const useAssets = (
  token: string,
  pagination: {
    pageIndex: number;
    pageSize: number;
  },
  adminLocation: number,
  search?: string,
  orderBy?: string,
  isDescending?: boolean,
) => {
  const [assets, setAssets] = useState<AssetRes[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<boolean | null>(false);
  const [pageCount, setPageCount] = useState<number>(0);

  const fetchAssets = async () => {
    try {
      const data = await getAllAssestService({
        token,
        pagination,
        search,
        orderBy,
        isDescending,
        adminLocation,
      });

      setAssets(data.data.data);
      setPageCount(data.data.totalPages);
    } catch (error) {
      setError(true);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchAssets();
  }, [token, pagination, search, orderBy, isDescending]);

  return { assets, loading, error, setAssets, pageCount, fetchAssets };
};
