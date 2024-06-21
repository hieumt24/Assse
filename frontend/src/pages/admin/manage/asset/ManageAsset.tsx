import { SearchForm } from "@/components";
import { FullPageModal } from "@/components/FullPageModal";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { assetColumns } from "@/components/tables/asset/assetColumns";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { LOCATIONS } from "@/constants";
import { useLoading } from "@/context/LoadingContext";
import { useAuth, usePagination } from "@/hooks";
import { useAssets } from "@/hooks/useAssets";
import { deleteAssetByIdService } from "@/services";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { AssetTable } from "../../../../components/tables/asset/AssetTable";

export const ManageAsset = () => {
  const navigate = useNavigate();
  const { token, user } = useAuth();
  const { onPaginationChange, pagination } = usePagination();
  const [search, setSearch] = useState("");
  const [orderBy, setOrderBy] = useState("");
  const [isDescending, setIsDescending] = useState(false);
  const { assets, loading, error, pageCount, fetchAssets } = useAssets(
    token!,
    pagination,
    LOCATIONS.find((location) => location.label === user.location)?.value || 1,
    search,
    orderBy,
    isDescending,
  );

  const { setIsLoading } = useLoading();
  const [assetIdToDelete, setAssetIdToDelete] = useState<string>("");
  const handleOpenDisable = (id: string) => {
    setAssetIdToDelete(id);
    setOpenDisable(true);
  };

  const handleDelete = async () => {
    try {
      setIsLoading(true);
      const res = await deleteAssetByIdService(assetIdToDelete);
      if (res.success) {
        toast.success(res.message);
      } else {
        toast.error(res.message);
      }
      fetchAssets();
      setOpenDisable(false);
    } catch (err) {
      console.log(err);
      toast.error("Error when disable user");
    } finally {
      setIsLoading(false);
    }
  };
  const [openDisable, setOpenDisable] = useState(false);

  return (
    <div className="m-24 flex h-full flex-grow flex-col gap-8">
      <p className="text-2xl font-bold text-red-600">Asset List</p>
      <div className="flex items-center justify-between">
        <SearchForm setSearch={setSearch} />
        <Button
          variant={"destructive"}
          onClick={() => navigate("/admin/asset/create-asset")}
        >
          <span className="capitalize">Create new asset</span>
        </Button>
      </div>
      {loading ? (
        <LoadingSpinner />
      ) : error ? (
        <div>Error</div>
      ) : (
        <>
          <AssetTable
            columns={assetColumns({
              handleOpenDisable,
              setOrderBy,
              setIsDescending,
              isDescending,
            })}
            data={assets!}
            pagination={pagination}
            onPaginationChange={onPaginationChange}
            pageCount={pageCount}
          />
          <FullPageModal show={openDisable}>
            <Dialog open={openDisable} onOpenChange={setOpenDisable}>
              <DialogContent onClick={(e) => e.stopPropagation()}>
                <DialogHeader>
                  <DialogTitle className="text-center text-2xl font-bold text-red-600">
                    Are you sure?
                  </DialogTitle>
                  <DialogDescription className="text-center text-lg">
                    Do you want to delete this asset
                  </DialogDescription>
                  <div className="flex items-center justify-center gap-4">
                    <Button variant={"destructive"} onClick={handleDelete}>
                      Yes
                    </Button>
                    <Button
                      variant="outline"
                      onClick={() => setOpenDisable(false)}
                    >
                      Cancel
                    </Button>
                  </div>
                </DialogHeader>
              </DialogContent>
            </Dialog>
          </FullPageModal>
        </>
      )}
    </div>
  );
};
