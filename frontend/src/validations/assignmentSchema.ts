import { z } from "zod";

export const createAssigmentSchema = z.object({
  userId: z.string(),
});
