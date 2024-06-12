import { differenceInYears, getDay, isAfter, isValid, parse } from "date-fns";
import { z } from "zod";
const dateFormat = /^\d{4}-?\d{2}-?\d{2}$/;
const nameFormat = /^[a-zA-Z]{2,50}$/;
export const createUserSchema = z.object({
  firstName: z.string().regex(nameFormat, {
    message: "The First Name length should be 2 - 50 letters.",
  }),
  lastName: z.string().regex(nameFormat, {
    message: "The Last Name length should be 2 - 50 letters.",
  }),
  dob: z
    .string()
    .regex(dateFormat, { message: "Please select Date Of Birth." })
    .refine(
      (dateString) => {
        // Parse the date
        const parsedDate = parse(dateString, "yyyy-mm-dd", new Date());

        // Check if it's a valid date
        if (!isValid(parsedDate)) {
          return false;
        }

        // Check if the day is after future day
        const futureDate = new Date();
        if (isAfter(parsedDate, futureDate)) return false;

        // Check if the date is at least 18 years ago
        const age = differenceInYears(new Date(), parsedDate);
        return age >= 18 && age <= 65;
      },
      {
        message: "Age must be between 18 and 65 years old, or after future day.",
      },
    ),
  joinedDate: z
    .string()
    .regex(dateFormat, { message: "Please select Joined Date." })
    .refine(
      (dateString) => {
        // Parse the date
        const parsedDate = parse(dateString, "yyyy-mm-dd", new Date());

        // Check if it's a valid date
        if (!isValid(parsedDate)) {
          return false;
        }

        // Check if the day is after future day
        const futureDate = new Date();
        if (isAfter(parsedDate, futureDate)) return false;

        // Check if the day is not Saturday (1) or Sunday (2)
        const dayOfWeek = getDay(parsedDate);
        if (dayOfWeek === 1 || dayOfWeek === 2) {
          return false;
        }
        return true;
      },
      {
        message:
          "Joined date can't be on Saturday, Sunday, earlier than DOB, or after future date.",
      },
    ),
  gender: z.enum(["0", "1", "2"]),
  role: z.enum(["0", "1"]),
  location: z.enum(["0", "1", "2"]),
});
