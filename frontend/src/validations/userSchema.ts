import { differenceInYears, isAfter, isValid } from "date-fns";
import { z } from "zod";
const dateFormat = /^\d{4}-?\d{2}-?\d{2}$/;
const nameFormat = /^[a-zA-Z\s]*$/;
export const createUserSchema = z.object({
  firstName: z
    .string()
    .trim()
    .min(2, { message: "The First Name must be at least 2 letters long." })
    .max(50, { message: "The First Name must be no longer than 50 letters." })
    .regex(nameFormat, {
      message: "The First Name must only contain letters and spaces.",
    }),
  lastName: z
    .string()
    .trim()
    .min(2, { message: "The Last Name must be at least 2 letters long." })
    .max(50, { message: "The Last Name must be no longer than 50 letters." })
    .regex(nameFormat, {
      message: "The Last Name must only contain letters and spaces.",
    }),
  dateOfBirth: z
    .string()
    .regex(dateFormat, { message: "Please select Date Of Birth." })
    .refine(
      (dateString) => {
        // Parse the date
        const [year, month, day] = dateString.split("-").map(Number);

        // Create a new Date object
        // Note: Months are 0-indexed in JavaScript Date objects, so subtract 1 from the month.
        const parsedDate = new Date(year, month - 1, day);

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
        message:
          "Age must be between 18 and 65 years old, or after future day.",
      },
    ),
  joinedDate: z
    .string()
    .regex(dateFormat, { message: "Please select Joined Date." })
    .refine(
      (dateString) => {
        // Parse the date
        const [year, month, day] = dateString.split("-").map(Number);

        // Create a new Date object
        // Note: Months are 0-indexed in JavaScript Date objects, so subtract 1 from the month.
        const parsedDate = new Date(year, month - 1, day);

        // Check if it's a valid date
        if (!isValid(parsedDate)) {
          return false;
        }

        // Check if the day is after future day
        const futureDate = new Date();
        if (isAfter(parsedDate, futureDate)) return false;

        // Check if the day is not Saturday (1) or Sunday (2)
        const dayOfWeek = parsedDate.toString().substring(0, 3);
        if (dayOfWeek === "Sat" || dayOfWeek === "Sun") {
          return false;
        }
        return true;
      },
      {
        message:
          "Joined date can't be on Saturday, Sunday, earlier than DOB, or after future date.",
      },
    ),
  gender: z.enum(["1", "2", "3"]),
  roleId: z.enum(["2", "1"]),
  location: z.enum(["3", "1", "2"]),
});
