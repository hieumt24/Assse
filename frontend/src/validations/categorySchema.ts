import { z } from "zod";

export const createCategorySchema = z.object({
  categoryName: z
    .string()
    .min(2, { message: "Category name must be at least 2 letters long." })
    .max(50, { message: "Category name must be no longer than 50 letters." }),
  prefix: z
    .string()
    .min(2, { message: "Prefix must be at least 2 letters long." })
    .max(5, { message: "Prefix must be no longer than 5 letters." })
    .regex(/^[a-zA-Z\s]*$/, {
      message: "First name must not contain accent marks or numbers.",
    })
    .regex(/^[A-Za-z]+$/, {
      message: "The First Name must contain only 1 word.",
    }),
});
