import { z } from "zod";

export const createAssigmentSchema = z.object({
  userId: z.string(),
  assetId: z.string(),
  assignedDate: z.string(),
  note: z.string(),
});
