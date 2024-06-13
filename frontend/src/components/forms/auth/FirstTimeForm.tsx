import { FullPageModal } from "@/components/FullPageModal";
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
import { useAuth } from "@/hooks";
import { removeExtraWhitespace } from "@/lib/utils";
import { firstTimeLoginSchema } from "@/validations";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import "react-toastify/dist/ReactToastify.css";
import { z } from "zod";

export const FirstTimeForm = () => {
  const { user } = useAuth();
  const isFirstTime = user.isFirstTimeLogin;
  const [showModal, setShowModal] = useState<boolean>(isFirstTime);
  let dateParts = user.dateOfBirth.split('-');
    
    // Join the parts into a single string without any separators
    let newDateStr = dateParts.join('');
  const oldPassword = user.username + "@" + newDateStr;
  useEffect(() => {
    if (isFirstTime) {
      setShowModal(true);
    }
  }, [user]);
  // Define form
  const form = useForm<z.infer<typeof firstTimeLoginSchema>>({
    mode: "all",
    resolver: zodResolver(firstTimeLoginSchema),
    defaultValues: {
      newPassword: "",
      oldPassword: oldPassword,
      username: user.username,
    },
  });

  // Function handle onSubmit
  const onSubmit = async (values: z.infer<typeof firstTimeLoginSchema>) => {
    console.log(values.newPassword);
    setShowModal(false);
  };

  return (
    <FullPageModal show={showModal}>
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit)}
          className="w-full space-y-5 rounded-lg border-2 border-black bg-zinc-100 text-lg shadow-lg"
        >
          <h1 className="rounded-t-lg border-b-2 border-black bg-zinc-300 p-6 text-xl font-bold text-red-600">
            Change password
          </h1>
          {/* New password */}
          <div className="p-6">
            <div className="mb-6">
              <p>This is the first time you logged in.</p>
              <p>You have to change your password to continue.</p>
            </div>
            <FormField
              control={form.control}
              name="newPassword"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>New password</FormLabel>
                  <FormControl>
                    <Input
                      type="password"
                      placeholder="Enter your new password"
                      {...field}
                      onBlur={(e) => {
                        const cleanedValue = removeExtraWhitespace(
                          e.target.value,
                        ); // Clean the input value
                        field.onChange(cleanedValue); // Update the form state
                      }}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="oldPassword"
              render={({ field }) => (
                <FormItem>
                  <FormControl>
                    <Input
                      type="hidden"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="username"
              render={({ field }) => (
                <FormItem>
                  <FormControl>
                    <Input
                      type="hidden"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <div className="mt-6 flex justify-end gap-8">
              <Button
                type="submit"
                className="bg-red-500 hover:bg-white hover:text-red-500"
                disabled={!form.formState.isValid}
              >
                Save
              </Button>
            </div>
          </div>
        </form>
      </Form>
    </FullPageModal>
  );
};
