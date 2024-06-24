import { SearchForm } from "@/components";
import { FullPageModal } from "@/components/FullPageModal";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { assetColumns } from "@/components/tables/asset/assetColumns";
import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import {
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger,
} from "@/components/ui/collapsible";
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
import { ASSET_STATES } from "@/constants";
import { useLoading } from "@/context/LoadingContext";
import { useAuth, usePagination } from "@/hooks";
import { useAssets } from "@/hooks/useAssets";
import useClickOutside from "@/hooks/useClickOutside";
import { CategoryRes } from "@/models";
import { deleteAssetByIdService, getAllCategoryService } from "@/services";
import { CaretSortIcon } from "@radix-ui/react-icons";
import { useCallback, useEffect, useRef, useState } from "react";
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
  const [assetStateType, setAssetStateType] = useState<number[]>([1, 2, 3]);
  const [selectedCategory, setSelectedCategory] = useState<string>("all");
  const { assets, loading, error, pageCount, totalRecords, fetchAssets } =
    useAssets(
      token!,
      pagination,
      user.location,
      search,
      orderBy,
      isDescending,
      assetStateType,
      selectedCategory,
    );
  const [isStateListOpen, setIsStateListOpen] = useState(false);
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

  const handleClickOutside = (event: MouseEvent) => {
    if (
      selectRef.current &&
      !selectRef.current.contains(event.target as Node)
    ) {
      setCategorySearch("");
    }
  };

  useEffect(() => {
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

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
  const stateListRef = useRef<HTMLDivElement>(null);

  useClickOutside(
    stateListRef,
    useCallback(() => setIsStateListOpen(false), []),
  );

  const handleCheckboxChange = (stateValue: number) => {
    setAssetStateType((prev) => {
      if (prev.includes(stateValue)) {
        return prev.filter((value) => value !== stateValue);
      } else {
        return [...prev, stateValue];
      }
    });
  };

  return (
    <div className="m-24 flex h-full flex-grow flex-col gap-8">
      <p className="text-2xl font-bold text-red-600">Asset List</p>
      <div className="flex items-center justify-between">
        <div className="flex gap-2">
          <Collapsible
            open={isStateListOpen}
            onOpenChange={setIsStateListOpen}
            className="relative w-[100px]"
            ref={stateListRef}
          >
            <CollapsibleTrigger asChild>
              <Button
                variant="ghost"
                size="sm"
                className="flex h-9 w-full items-center justify-between whitespace-nowrap rounded-md border border-input bg-transparent px-3 py-2 text-sm font-normal shadow-sm ring-offset-background placeholder:text-muted-foreground focus:outline-none disabled:cursor-not-allowed disabled:opacity-50 [&>span]:line-clamp-1"
              >
                State
                <CaretSortIcon className="h-4 w-4 opacity-50" />
              </Button>
            </CollapsibleTrigger>
            <CollapsibleContent className="absolute z-50 max-h-96 min-w-[8rem] overflow-hidden rounded-md border bg-popover bg-white p-1 font-semibold text-popover-foreground shadow-md transition-all data-[state=open]:animate-in data-[state=closed]:animate-out data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0 data-[state=closed]:zoom-out-95 data-[state=open]:zoom-in-95 data-[side=bottom]:slide-in-from-top-2">
              {ASSET_STATES.map((state) => (
                <div className="flex items-center gap-2 rounded-md px-2 py-1.5 text-sm font-normal text-zinc-900 transition-all hover:bg-zinc-100">
                  <Checkbox
                    value={state.value.toString()}
                    key={state.value}
                    id={`state-checkbox-${state.value}`}
                    onCheckedChange={() => {
                      handleCheckboxChange(state.value);
                    }}
                    checked={assetStateType.includes(state.value)}
                  >
                    {state.label}
                  </Checkbox>
                  <label htmlFor={`state-checkbox-${state.value}`}>
                    {state.label}
                  </label>
                </div>
              ))}
            </CollapsibleContent>
          </Collapsible>
          <div ref={selectRef} className="w-[150px]">
            <Select
              onValueChange={(value) => {
                setSelectedCategory(value);
              }}
            >
              <SelectTrigger>
                <SelectValue placeholder="Category" />
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
        </div>

        <div className="flex justify-between gap-6">
          <SearchForm
            setSearch={setSearch}
            onSubmit={() => {
              onPaginationChange((prev) => ({
                ...prev,
                pageIndex: 1,
              }));
            }}
          />
          <Button
            variant={"destructive"}
            onClick={() => navigate("/assets/create")}
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
            totalRecords={totalRecords}
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
