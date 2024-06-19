import { isAfter, isValid } from "date-fns";
import { z } from "zod";

const dateFormat = /^\d{4}-?\d{2}-?\d{2}$/;
const nameFormat = /^[a-zA-Z\s]*$/;

export const createAssetSchema = z.object({
  name: z
    .string()
    .trim()
    .min(2, { message: "Name must be at least 2 letters long." })
    .max(50, { message: "Name must be no longer than 50 letters." })
    .regex(nameFormat, {
      message: "Name must not contain accent marks or numbers.",
    }),
  category: z.string(),
  specification: z
    .string()
    .trim()
    .min(2, { message: "Specification must be at least 2 letters long." })
    .max(100, { message: "Specification must be no longer than 100 letters." }),
  installedDate: z
    .string()
    .regex(dateFormat, { message: "Please select a valid Joined Date." })
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);
        return isValid(parsedDate);
      },
      { message: "Invalid Joined Date. Please enter a valid date." },
    )
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);
        return !isAfter(parsedDate, new Date());
      },
      { message: "Joined Date cannot be in the future." },
    )
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);

        const dayOfWeek = parsedDate.toString().substring(0, 3);
        if (dayOfWeek === "Sat" || dayOfWeek === "Sun") {
          return false;
        }
        return true;
      },
      {
        message: "Joined date can't be on Saturday, Sunday.",
      },
    ),
  state: z.enum(["1", "2"]),
});
