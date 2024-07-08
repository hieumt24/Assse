import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { cn } from "@/lib/utils";
import { format } from "date-fns";
import { useState } from "react";
import { DateRange } from "react-day-picker";
import { IoCalendar, IoClose } from "react-icons/io5";

interface DatePickerProps {
  formatDate?: string;
  setValue: any;
  placeholder?: string;
  onChange?: any;
  className?: string;
  mode?: "multiple" | "default" | "single" | "range" | undefined;
}

export function DatePicker(props: Readonly<DatePickerProps>) {
  const {
    formatDate,
    setValue,
    placeholder,
    onChange,
    className,
    mode = "single",
  } = props;
  const [date, setDate] = useState<Date>();
  const [dateRange, setDateRange] = useState<DateRange | undefined>();

  const handleDateSelect = (selectedDate: Date | undefined) => {
    setDate(selectedDate);

    if (selectedDate) {
      const nextDay = new Date(selectedDate);
      nextDay.setDate(nextDay.getDate());
      setValue(nextDay);
    } else {
      setValue(null);
    }
    onChange && onChange();
  };

  const handleDateRangeSelect = (selectedDate: DateRange | undefined) => {
    setDateRange(selectedDate);

    if (selectedDate) {
      setValue(selectedDate);
    }
    onChange && onChange();
  };

  const handleClear = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.stopPropagation(); // Prevent the Popover from opening
    setDate(undefined);
    setValue(null);
    onChange();
  };

  return (
    <Popover>
      <PopoverTrigger asChild>
        <Button
          variant={"outline"}
          className={cn(
            "w-48 items-center justify-between font-normal",
            !date && "text-muted-foreground",
            className,
          )}
        >
          {date ? (
            <>
              {format(date, formatDate ?? "dd/MM/yyyy")}
              <Button
                variant="ghost"
                size="icon"
                className="h-4 w-4 p-0"
                onClick={handleClear}
              >
                <IoClose size={14} />
              </Button>
            </>
          ) : dateRange?.from ? (
            dateRange.to ? (
              <>
                {format(dateRange.from, "LLL dd, y")} -{" "}
                {format(dateRange.to, "LLL dd, y")}
              </>
            ) : (
              format(dateRange.from, "LLL dd, y")
            )
          ) : (
            <span>{placeholder || "Select date"}</span>
          )}
          <IoCalendar size={20} />
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-auto p-0">
        {mode === "single" ? (
          <Calendar
            mode={"single"}
            selected={date}
            onSelect={handleDateSelect}
            initialFocus
          />
        ) : mode === "range" ? (
          <Calendar
            mode={"range"}
            selected={dateRange}
            onSelect={handleDateRangeSelect}
            initialFocus
          />
        ) : (
          <></>
        )}
      </PopoverContent>
    </Popover>
  );
}
