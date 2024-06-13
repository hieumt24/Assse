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
import { loginService } from "@/services";
import { loginSchema } from "@/validations";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { z } from "zod";

export const LoginForm = () => {
  const { setIsFirstTime } = useAuth();
  // Define form
  const form = useForm<z.infer<typeof loginSchema>>({
    mode: "all",
    resolver: zodResolver(loginSchema),
    defaultValues: {
      username: "",
      password: "",
    },
  });

  // Function handle onSubmit
  const onSubmit = async (values: z.infer<typeof loginSchema>) => {
    const res = await loginService({ ...values });
    if (res.success) {
      setIsFirstTime(res.data.isFirstTimeLogin);
      toast.success(res.message);
      navigate("/admin");
    } else {
      toast.error(res.message);
    }
  };

  const navigate = useNavigate();

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="w-1/3 space-y-5 rounded-2xl border bg-white p-6 text-lg shadow-lg"
      >
        <h1 className="text-xl font-bold text-red-600">Login</h1>
        {/* Username */}
        <FormField
          control={form.control}
          name="username"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Username</FormLabel>
              <FormControl>
                <Input
                  placeholder="Enter username"
                  {...field}
                  onBlur={(e) => {
                    const cleanedValue = removeExtraWhitespace(e.target.value); // Clean the input value
                    field.onChange(cleanedValue); // Update the form state
                  }}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Password */}
        <FormField
          control={form.control}
          name="password"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Password</FormLabel>
              <FormControl>
                <Input placeholder="Enter password" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <div className="flex justify-end gap-8">
          <Button
            type="submit"
            className="bg-red-500 hover:bg-white hover:text-red-500"
            disabled={!form.formState.isValid}
          >
            Login
          </Button>
          <Button
            type="button"
            className="border bg-white text-black shadow-none hover:text-white"
            onClick={() => {
              navigate("/admin/user");
            }}
          >
            Cancel
          </Button>
        </div>
      </form>
    </Form>
  );
};
