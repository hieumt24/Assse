import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { removeExtraWhitespace } from "@/lib/utils";
import { searchUserSchema } from "@/validations";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { MdSearch } from "react-icons/md";

import { z } from "zod";

interface SearchUserFormProps {
  setSearch: React.Dispatch<React.SetStateAction<string>>;
}

export const SearchUserForm = (props: SearchUserFormProps) => {
  // Define form
  const form = useForm<z.infer<typeof searchUserSchema>>({
    mode: "all",
    resolver: zodResolver(searchUserSchema),
    defaultValues: {
      searchTerm: "",
    },
  });

  // Function handle onSubmit
  const onSubmit = async (values: z.infer<typeof searchUserSchema>) => {
    props.setSearch(values.searchTerm);
  };
  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="flex w-1/5 text-lg"
      >
        {/* Search term */}
        <FormField
          control={form.control}
          name="searchTerm"
          render={({ field }) => (
            <FormItem>
              <FormControl>
                <Input
                  placeholder="Enter search term"
                  {...field}
                  onBlur={(e) => {
                    const cleanedValue = removeExtraWhitespace(e.target.value); // Clean the input value
                    field.onChange(cleanedValue); // Update the form state
                  }}
                  autoFocus
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        <Button
          type="submit"
          disabled={!form.formState.isValid}
          variant={"outline"}
        >
          <MdSearch />
        </Button>
      </form>
    </Form>
  );
};
