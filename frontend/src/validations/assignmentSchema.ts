import { z } from "zod";

export const createAssigmentSchema = z.object({
  userId: z.string().min(1, "User is required"),
  assetId: z.string(),
  assignedDate: z.string(),
  note: z.string(),
});
