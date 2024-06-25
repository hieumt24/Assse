import { LoadingSpinner } from "@/components/LoadingSpinner";
import { UserTable } from "@/components/tables";
import { assignmentUserColumns } from "@/components/tables/user/assignmentUserColumns";
import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogTrigger } from "@/components/ui/dialog";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { useAuth, usePagination, useUsers } from "@/hooks";
import { UserRes } from "@/models";
import { createAssigmentSchema } from "@/validations/assignmentSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { IoIosSearch } from "react-icons/io";
import { z } from "zod";
import { SearchForm } from "../user";

export const CreateAssignmentForm = () => {
  const form = useForm<z.infer<typeof createAssigmentSchema>>({
    mode: "all",
    resolver: zodResolver(createAssigmentSchema),
    defaultValues: {
      userId: "",
      assetId: "",
      assignedDate: "",
    },
  });
  const [openChooseUser, setOpenChooseUser] = useState(false);
  //   const [openChooseAsset, setOpenChooseAsset] = useState(false);

  const [userSearchQuery, setUserSearchQuery] = useState("");
  const { onPaginationChange, pagination } = usePagination();
  const { user } = useAuth();
  const [orderBy, setOrderBy] = useState("");
  const [isDescending, setIsDescending] = useState(true);
  const [roleType, setRoleType] = useState(0);
  const [selectedUser, setSelectedUser] = useState<UserRes>();
  pagination.pageSize = 5;
  const { users, loading, pageCount, totalRecords } = useUsers(
    pagination,
    user.location,
    userSearchQuery,
    roleType,
    orderBy,
    isDescending,
  );

  return (
    <Form {...form}>
      <form className="w-1/3 space-y-5 rounded-2xl bg-white p-6 shadow-md">
        <h1 className="text-2xl font-bold text-red-600">
          Create New Assignment
        </h1>
        <FormField
          control={form.control}
          name="userId"
          render={() => (
            <FormItem>
              <FormLabel className="text-md">
                User <span className="text-red-600">*</span>
              </FormLabel>
              <FormControl>
                <Dialog open={openChooseUser} onOpenChange={setOpenChooseUser}>
                  <DialogTrigger className="flex h-9 w-full items-center justify-between rounded-md border border-input bg-transparent px-3 py-1 text-sm shadow-sm transition-colors">
                    <span className="text-zinc-400">
                      {form.getValues("userId") !== ""
                        ? `${selectedUser?.staffCode} ${selectedUser?.firstName} ${selectedUser?.lastName}`
                        : "Choose user"}
                    </span>
                    <IoIosSearch />
                  </DialogTrigger>
                  <DialogContent className="max-w-2xl">
                    <div className="w-full px-6">
                      <div className="flex w-full justify-between">
                        <div className="flex items-center text-lg font-bold text-red-600">
                          Select User
                        </div>
                        <SearchForm setSearch={setUserSearchQuery} />
                      </div>
                      <div className="mt-8">
                        {loading ? (
                          <div className="h-[400px]">
                            <LoadingSpinner />
                          </div>
                        ) : (
                          <UserTable
                            columns={assignmentUserColumns({
                              selectedId: selectedUser?.id || "",
                              setOrderBy,
                              setIsDescending,
                              isDescending,
                              orderBy,
                            })}
                            data={users!}
                            onPaginationChange={onPaginationChange}
                            pagination={pagination}
                            pageCount={pageCount}
                            totalRecords={totalRecords}
                            onRowClick={setSelectedUser}
                          />
                        )}
                      </div>
                      <div className="flex justify-end gap-2">
                        <Button
                          variant={"destructive"}
                          onClick={() => {
                            form.setValue("userId", selectedUser?.id || "");
                            setOpenChooseUser(false);
                          }}
                        >
                          Save
                        </Button>
                        <Button
                          variant={"ghost"}
                          onClick={() => setOpenChooseUser(false)}
                        >
                          Cancel
                        </Button>
                      </div>
                    </div>
                  </DialogContent>
                </Dialog>
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
      </form>
    </Form>
  );
};
