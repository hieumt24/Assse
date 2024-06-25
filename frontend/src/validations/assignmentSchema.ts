import { z } from "zod";

export const createAssigmentSchema = z.object({
  userId: z.string().nonempty("User is required."),
  assetId: z.string().nonempty("Asset is required."),
  assignedDate: z.string().nonempty("Assigned date is required."),
});
