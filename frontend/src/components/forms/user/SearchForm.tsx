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
import { searchSchema } from "@/validations";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { MdSearch } from "react-icons/md";

import { z } from "zod";

interface SearchFormProps {
  setSearch: React.Dispatch<React.SetStateAction<string>>;
  onSubmit?: any;
}

export const SearchForm = (props: SearchFormProps) => {
  // Define form
  const form = useForm<z.infer<typeof searchSchema>>({
    mode: "all",
    resolver: zodResolver(searchSchema),
    defaultValues: {
      searchTerm: "",
    },
  });

  // Function handle onSubmit
  const onSubmit = async (values: z.infer<typeof searchSchema>) => {
    props.setSearch(values.searchTerm);
    props.onSubmit();
  };
  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="flex text-lg">
        {/* Search term */}
        <FormField
          control={form.control}
          name="searchTerm"
          render={({ field }) => (
            <FormItem>
              <FormControl>
                <Input
                  placeholder="Search"
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

        <Button type="submit" variant={"outline"}>
          <MdSearch />
        </Button>
      </form>
    </Form>
  );
};
