import { isBefore, isValid, startOfDay } from "date-fns";
import { z } from "zod";

export const createAssignmentSchema = z.object({
  userId: z.string().min(1, "User is required."),
  assetId: z.string().min(1, "Asset is required."),
  assignedDate: z
    .string()
    .refine(
      (dateString) => {
        return dateString.length > 0;
      },
      { message: "Assigned Date is required." },
    )
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);
        return isValid(parsedDate);
      },
      { message: "Invalid Assigned Date. Please enter a valid date." },
    )
    .refine(
      (dateString) => {
        const parsedDate = new Date(dateString);
        return !isBefore(parsedDate, startOfDay(new Date()));
      },
      { message: "Assigned Date can only be today or in the future." },
    ),
  note: z
    .string()
    .trim()
    .min(1, "Note must not be blank.")
    .min(2, "Note must be at least 2 characters long.")
    .max(256, "Note must not be longer than 256 characters.")
    .regex(/[a-zA-Z]/, "Note must contain letters."),
});
