import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Textarea } from "@/components/ui/textarea";
import { ASSET_STATES } from "@/constants";
import { CategoryRes } from "@/models";
import { getAllCategoryService } from "@/services/admin/manageAssetService";
import { createAssetSchema } from "@/validations/assetSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useRef, useState } from "react";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { z } from "zod";
import { CreateCategoryForm } from "./CreateCategoryForm";

export const CreateAssetForm: React.FC = () => {
  const [categories, setCategories] = useState(Array<CategoryRes>);
  const [filteredCategories, setFilteredCategories] = useState(
    Array<CategoryRes>,
  );
  const [openCreateCategory, setOpenCreateCategory] = useState(false);
  const [categorySearch, setCategorySearch] = useState("");
  const inputRef = useRef<HTMLInputElement>(null);

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

  useEffect(() => {
    const fetchCategories = async () => {
      const res = await getAllCategoryService();
      if (res.success) {
        setCategories(res.data.data);
        setFilteredCategories(res.data.data);
      } else {
        console.log(res.message);
      }
    };
    fetchCategories();
  }, []);

  const form = useForm<z.infer<typeof createAssetSchema>>({
    mode: "all",
    resolver: zodResolver(createAssetSchema),
    defaultValues: {
      name: "",
      category: "",
      specification: "",
      installedDate: "",
      state: "1",
    },
  });

  const onSubmit = async (values: z.infer<typeof createAssetSchema>) => {
    console.log(values);
    toast.success("Asset created successfully!");
  };

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="w-1/3 space-y-5 rounded-2xl bg-white p-6 shadow-md"
      >
        <h1 className="text-2xl font-bold text-red-600">Create New Asset</h1>

        <FormField
          control={form.control}
          name="name"
          render={({ field }) => (
            <FormItem>
              <FormLabel className="text-md">
                Name <span className="text-red-600">*</span>
              </FormLabel>
              <FormControl>
                <Input
                  placeholder="Enter asset name"
                  {...field}
                  onBlur={(e) => {
                    const cleanedValue = e.target.value.trim();
                    field.onChange(cleanedValue);
                  }}
                  autoFocus
                />
              </FormControl>
              <FormMessage>{form.formState.errors.name?.message}</FormMessage>
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="category"
          render={({ field }) => (
            <FormItem>
              <FormLabel className="text-md">
                Category <span className="text-red-600">*</span>
              </FormLabel>
              <FormControl>
                <Select value={field.value} onValueChange={field.onChange}>
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
                    <div className="h-[100px] overflow-y-scroll">
                      {filteredCategories?.map((category) => (
                        <SelectItem
                          key={category.id}
                          value={category.categoryName}
                        >
                          {category.categoryName} ({category.prefix})
                        </SelectItem>
                      ))}
                    </div>
                    <Button
                      variant={"ghost"}
                      className="w-full"
                      onClick={() => {
                        setOpenCreateCategory(true);
                      }}
                    >
                      + Add new category
                    </Button>
                  </SelectContent>
                </Select>
              </FormControl>
              <FormMessage>
                {form.formState.errors.category?.message}
              </FormMessage>
            </FormItem>
          )}
        />

        {/* Specification */}
        <FormField
          control={form.control}
          name="specification"
          render={({ field }) => (
            <FormItem>
              <FormLabel className="text-md">
                Specification <span className="text-red-600">*</span>
              </FormLabel>
              <FormControl>
                <Textarea placeholder="Enter specification" {...field} />
              </FormControl>
              <FormMessage>
                {form.formState.errors.specification?.message}
              </FormMessage>
            </FormItem>
          )}
        />

        {/* Installed Date */}
        <FormField
          control={form.control}
          name="installedDate"
          render={({ field }) => (
            <FormItem>
              <FormLabel className="text-md">
                Installed Date <span className="text-red-600">*</span>
              </FormLabel>
              <FormControl>
                <Input
                  style={{ justifyContent: "center" }}
                  type="date"
                  {...field}
                />
              </FormControl>
              <FormMessage>
                {form.formState.errors.installedDate?.message}
              </FormMessage>
            </FormItem>
          )}
        />

        {/* State */}
        <FormField
          control={form.control}
          name="state"
          render={({ field }) => (
            <FormItem>
              <FormLabel className="text-md">State</FormLabel>
              <FormControl>
                <RadioGroup
                  onValueChange={field.onChange}
                  defaultValue={field.value}
                  className="flex gap-5"
                >
                  {ASSET_STATES.map((state) => {
                    return (
                      <FormItem
                        className="flex items-center gap-1 space-y-0"
                        key={state.value}
                      >
                        <FormControl>
                          <RadioGroupItem value={state.value.toString()} />
                        </FormControl>
                        <FormLabel className="font-normal">
                          {state.label}
                        </FormLabel>
                      </FormItem>
                    );
                  })}
                </RadioGroup>
              </FormControl>
              <FormMessage>{form.formState.errors.state?.message}</FormMessage>
            </FormItem>
          )}
        />

        <div className="flex justify-end gap-4">
          <Button
            type="submit"
            className="w-[76px] bg-red-500 hover:bg-white hover:text-red-500"
            disabled={!form.formState.isValid}
          >
            Save
          </Button>
          <Button
            type="button"
            className="w-[76px] border bg-white text-black shadow-none hover:text-white"
          >
            Cancel
          </Button>
        </div>
        <CreateCategoryForm
          open={openCreateCategory}
          setOpen={setOpenCreateCategory}
        />
      </form>
    </Form>
  );
};
