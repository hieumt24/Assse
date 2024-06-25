import { isBefore, isValid, startOfDay } from "date-fns";
import { z } from "zod";

export const createAssigmentSchema = z.object({
  userId: z.string().min(1, "User is required."),
  assetId: z.string().min(1, "Asset is required."),
  assignedDate: z
    .string()
    .min(1, "Assigned date is required.")
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
  note: z.string().min(1, "Note is required."),
});
