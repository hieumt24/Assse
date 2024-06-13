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
    .regex(dateFormat, {
      message: "Please select a valid Date Of Birth.",
    })
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);
        return isValid(parsedDate);
      },
      { message: "Invalid Date of Birth. Please enter a valid date." },
    )
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);
        return !isAfter(parsedDate, new Date());
      },
      { message: "Date of Birth cannot be in the future." },
    )
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);
        const age = differenceInYears(new Date(), parsedDate);
        return age >= 18;
      },
      { message: "Age must be at least 18 years old." },
    )
    .refine(
      (dateString) => {
        const [year, month, day] = dateString.split("-").map(Number);
        const parsedDate = new Date(year, month - 1, day);
        const age = differenceInYears(new Date(), parsedDate);
        return age <= 65;
      },
      { message: "Age must be no more than 65 years old." },
    ),
  joinedDate: z
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
        message:
          "Joined date can't be on Saturday, Sunday.",
      },
    ),
  gender: z.enum(["1", "2", "3"]),
  role: z.enum(["2", "1"]),
  location: z.enum(["3", "1", "2"]),
})
.refine(
  (data) => {
    const dateOfBirth = new Date(data.dateOfBirth);
    const joinedDate = new Date(data.joinedDate);

    return isAfter(joinedDate, dateOfBirth);
  },
  {
    message: "Joined date must be after the date of birth.",
    path: ["joinedDate"],
  }
);
