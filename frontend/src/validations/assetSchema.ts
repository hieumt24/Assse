import { isAfter, isBefore, isValid } from "date-fns";
import { z } from "zod";

const dateFormat = /^\d{4}-?\d{2}-?\d{2}$/;
const nameFormat = /^[a-zA-Z0-9\s!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]*$/;
const containsLetters = /[a-zA-Z]/;

export const createAssetSchema = z.object({
  name: z
    .string()
    .trim()
    .min(2, { message: "Name must be at least 2 characters long." })
    .max(50, { message: "Name must be no longer than 50 characters." })
    .regex(containsLetters, {
      message: "The asset name must contain letters.",
    })
    .regex(nameFormat, {
      message: "Name must not contain accent marks.",
    }),
  category: z.string(),
  specification: z
    .string()
    .trim()
    .regex(containsLetters, {
      message: "The specification name must contain letters.",
    })
    .min(2, { message: "Specification must be at least 2 characters long." })
    .max(100, {
      message: "Specification must be no longer than 100 characters.",
    }),
  installedDate: z
    .string()
    .regex(dateFormat, { message: "Please select a valid Installed Date." })
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);
        return isValid(parsedDate);
      },
      { message: "Invalid Installed Date. Please enter a valid date." },
    )
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);
        return !isBefore(parsedDate, new Date("2000/01/01"));
      },
      { message: "Installed Date must be from the year 2000 or later." },
    )
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);
        return !isAfter(parsedDate, new Date());
      },
      { message: "Installed Date cannot be in the future." },
    ),
  state: z.enum(["1", "2"]),
});

export const updateAssetSchema = z.object({
  name: z
    .string()
    .trim()
    .min(1, "Asset name cannot be blank")
    .min(2, { message: "Name must be at least 2 letters long." })
    .max(50, { message: "Name must be no longer than 50 letters." })
    .regex(containsLetters, {
      message: "The asset name must contain letters.",
    })
    .regex(nameFormat, {
      message: "Name must not contain accent marks.",
    }),
  specification: z
    .string()
    .trim()
    .regex(containsLetters, {
      message: "The specification name must contain letters.",
    })
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
        return !isBefore(parsedDate, new Date("2000/01/01"));
      },
      { message: "Installed Date must be from the year 2000 or later." },
    )
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);
        return !isAfter(parsedDate, new Date());
      },
      { message: "Joined Date cannot be in the future." },
    ),
  state: z.enum(["1", "2", "3", "4", "5"]),
});
