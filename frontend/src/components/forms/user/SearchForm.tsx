import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import useDebounce from "@/hooks/useDebounce";
import { removeExtraWhitespace } from "@/lib/utils";
import { searchSchema } from "@/validations";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useState } from "react";
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

  const [isInitialRender, setIsInitialRender] = useState(true);

  useEffect(() => {
    setIsInitialRender(false);
  }, []);

  useDebounce(
    () => {
      if (!isInitialRender) {
        props.setSearch(form.getValues("searchTerm"));
        if (props.onSubmit) {
          props.onSubmit();
        }
      }
    },
    [form.watch("searchTerm")],
    500,
  );

  // Function handle onSubmit
  const onSubmit = async (values: z.infer<typeof searchSchema>) => {
    props.setSearch(values.searchTerm);
    if (props.onSubmit) {
      props.onSubmit();
    }
  };
  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="flex rounded-lg border border-zinc-200 text-lg"
      >
        {/* Search term */}
        <FormField
          control={form.control}
          name="searchTerm"
          render={({ field }) => (
            <FormItem>
              <FormControl>
                <Input
                  className="border-none focus-visible:ring-0"
                  placeholder="Search"
                  {...field}
                  onBlur={(e) => {
                    const cleanedValue = removeExtraWhitespace(e.target.value);
                    field.onChange(cleanedValue);
                    field.onBlur();
                  }}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        <Button
          type="submit"
          variant={"outline"}
          className="border-none p-2 hover:bg-transparent"
        >
          <MdSearch />
        </Button>
      </form>
    </Form>
  );
};
