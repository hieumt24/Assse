import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { cn } from "@/lib/utils";
import { format } from "date-fns";
import { Dispatch, SetStateAction, useState } from "react";
import { IoCalendar } from "react-icons/io5";

interface DatePickerProps {
  formatDate?: string;
  setValue: Dispatch<SetStateAction<Date | null>>;
  placeholder?: string;
}

export function DatePicker(props: DatePickerProps) {
  const { formatDate, setValue, placeholder } = props;
  const [date, setDate] = useState<Date>();
  setValue(date!);

  return (
    <Popover>
      <PopoverTrigger asChild>
        <Button
          variant={"outline"}
          className={cn(
            "w-48 items-center justify-between font-normal",
            !date && "text-muted-foreground",
          )}
        >
          {date ? (
            format(date, formatDate ?? "dd/MM/yyyy")
          ) : (
            <span>{placeholder || "Select date"}</span>
          )}
          <IoCalendar size={20} />
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-auto p-0">
        <Calendar
          mode="single"
          selected={date}
          onSelect={setDate}
          initialFocus
        />
      </PopoverContent>
    </Popover>
  );
}
