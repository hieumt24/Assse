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
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { ASSET_STATES, LOCATIONS } from "@/constants";
import { useLoading } from "@/context/LoadingContext";
import { useAuth, usePagination } from "@/hooks";
import { useAssets } from "@/hooks/useAssets";
import useClickOutside from "@/hooks/useClickOutside";
import { CategoryRes } from "@/models";
import { deleteAssetByIdService, getAllCategoryService } from "@/services";
import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { AssetTable } from "../../../../components/tables/asset/AssetTable";

export const ManageAsset = () => {
  const navigate = useNavigate();
  const { token, user } = useAuth();
  const { onPaginationChange, pagination } = usePagination();
  const [search, setSearch] = useState("");
  const [orderBy, setOrderBy] = useState("");
  const [isDescending, setIsDescending] = useState(true);
  const [assetStateType, setAssetStateType] = useState(0);
  const [selectedCategory, setSelectedCategory] = useState<string>("all");
  const { assets, loading, error, pageCount, fetchAssets } = useAssets(
    token!,
    pagination,
    LOCATIONS.find((location) => location.label === user.location)?.value || 1,
    search,
    orderBy,
    isDescending,
    assetStateType,
    selectedCategory,
  );
  const [categories, setCategories] = useState(Array<CategoryRes>);
  const [filteredCategories, setFilteredCategories] = useState(
    Array<CategoryRes>,
  );
  const [categorySearch, setCategorySearch] = useState("");
  const inputRef = useRef<HTMLInputElement>(null);
  const selectRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    setFilteredCategories(
      categories.filter((category) =>
        category.categoryName
          .toLowerCase()
          .includes(categorySearch.toLowerCase()),
      ),
    );
  }, [categorySearch]);

  useEffect(() => {
    inputRef?.current?.focus();
  }, [filteredCategories]);

  const fetchCategories = async () => {
    const res = await getAllCategoryService();
    if (res.success) {
      setCategories(res.data.data);
      setFilteredCategories(res.data.data);
    } else {
      console.log(res.message);
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  useClickOutside(selectRef, () => {
    setCategorySearch("");
  });

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
        <div className="flex gap-2">
          <Select
            onValueChange={(value) => {
              setAssetStateType(parseInt(value));
            }}
          >
            <SelectTrigger className="w-32">
              <SelectValue placeholder="Status" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="0">All</SelectItem>
              {ASSET_STATES.map((state) => (
                <SelectItem value={state.value.toString()}>
                  {state.label}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
          <Select
            onValueChange={(value) => {
              setSelectedCategory(value);
            }}
          >
            <SelectTrigger>
              <SelectValue placeholder="Select category" />
            </SelectTrigger>
            <SelectContent>
              <Input
                ref={inputRef}
                placeholder="Search category ..."
                className="border-none shadow-none focus-visible:ring-0"
                value={categorySearch}
                onChange={(e) => {
                  setCategorySearch(e.target.value);
                }}
              />
              <div className="max-h-[100px] overflow-y-scroll">
                <SelectItem key={0} value="all">
                  All
                </SelectItem>
                {filteredCategories?.map((category) => (
                  <SelectItem key={category.id} value={category.id}>
                    {category.categoryName} ({category.prefix})
                  </SelectItem>
                ))}
              </div>
            </SelectContent>
          </Select>
        </div>

        <div className="flex justify-between gap-6">
          <SearchForm setSearch={setSearch} />
          <Button
            variant={"destructive"}
            onClick={() => navigate("/admin/asset/create-asset")}
          >
            <span className="capitalize">Create new asset</span>
          </Button>
        </div>
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
              orderBy,
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
