import { z } from "zod";

export const createAssetSchema = z.object({
  name: z.string(),
  category: z.string(),
  specification: z.string(),
  installedDate: z.string(),
  state: z.enum(["Available", "Not available"]),
});
