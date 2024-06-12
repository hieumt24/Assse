import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export const removeExtraWhitespace = (str: string): string => {
  return str.replace(/\s+/g, " ").trim();
};
