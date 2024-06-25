import { z } from "zod";

export const createCategorySchema = z.object({
  categoryName: z
    .string()
    .min(1, { message: "Category name cannot be blank" })
    .min(2, { message: "Category name must be at least 2 characters long." })
    .max(50, {
      message: "Category name must be no longer than 50 characters.",
    }),
  prefix: z
    .string()
    .min(1, { message: "Prefix cannot be blank" })
    .min(2, { message: "Prefix must be at least 2 characters long." })
    .max(5, { message: "Prefix must be no longer than 5 characters." })
    .regex(/^[a-zA-Z]*$/, {
      message: "Prefix must not contain special characters or numbers.",
    }),
});
