import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { createAssigmentSchema } from "@/validations/assignmentSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";

export const CreateAssignmentForm = () => {
  const form = useForm<z.infer<typeof createAssigmentSchema>>({
    mode: "all",
    resolver: zodResolver(createAssigmentSchema),
  });
  return (
    <Form {...form}>
      <form>
        <h1 className="text-2xl font-bold text-red-600">
          Create New Assignment
        </h1>
        <FormField
          control={form.control}
          name="userId"
          render={({ field }) => (
            <FormItem>
              <FormLabel className="text-md">
                User <span className="text-red-600">*</span>
              </FormLabel>
              <FormControl>
                <Input placeholder="Enter first name" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
      </form>
    </Form>
  );
};
