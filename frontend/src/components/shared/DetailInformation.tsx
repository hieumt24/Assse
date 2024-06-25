import { LOCATIONS } from "@/constants";
import { AssetRes, UserRes } from "@/models";
import { format } from "date-fns";
import { DialogContent } from "../ui/dialog";

interface DetailInformationProps<T> {
  info: T;
  variant: "User" | "Asset";
}

type FormattableValue = string | number | Date | null | undefined;

export const DetailInformation = <T extends UserRes | AssetRes>({
  info,
  variant,
}: DetailInformationProps<T>) => {
  const formatValue = (key: string, value: FormattableValue): string => {
    if (value == null) return "N/A";

    const formatters: Record<string, (val: FormattableValue) => string> = {
      dateOfBirth: formatDate,
      joinedDate: formatDate,
      installedDate: formatDate,
      gender: formatGender,
      role: formatRole,
      location: formatLocation,
      assetLocation: formatLocation,
      state: formatState,
    };

    return formatters[key] ? formatters[key](value) : String(value);
  };

  const formatDate = (value: FormattableValue) =>
    value instanceof Date ? format(value, "MM/dd/yyyy") : String(value);

  const formatGender = (value: FormattableValue) =>
    value === 2 ? "Male" : value === 1 ? "Female" : "Other";

  const formatRole = (value: FormattableValue) =>
    value === 1 ? "Admin" : value === 2 ? "Staff" : "Unknown";

  const formatLocation = (value: FormattableValue) =>
    LOCATIONS[Number(value) - 1]?.label || "Unknown";

  const formatState = (value: FormattableValue) => {
    const states: Record<number, string> = {
      1: "Available",
      2: "Not Available",
      3: "Assigned",
      4: "Waiting For Recycling",
      5: "Recycled",
    };
    return states[Number(value)] || String(value);
  };

  const excludedKeys = [
    "id",
    "createdOn",
    "lastModifiedOn",
    "lastModifiedBy",
    "createdBy",
    "categoryId",
  ];

  const formatKey = (key: string) =>
    key.replace(/([A-Z])/g, " $1").replace(/^./, (str) => str.toUpperCase());

  return (
    <DialogContent className="max-w-md border-none p-0">
      <div className="overflow-hidden rounded-lg bg-white shadow-lg">
        <h2 className="bg-red-600 p-6 text-xl font-semibold text-white">
          Detailed {variant} Information
        </h2>
        <div className="p-6">
          <table className="w-full">
            <tbody>
              {Object.entries(info)
                .filter(([key]) => !excludedKeys.includes(key))
                .map(([key, value]) => (
                  <tr
                    key={key}
                    className="border-b border-gray-200 last:border-b-0"
                  >
                    <td className="py-3 pr-4 font-medium text-gray-600">
                      {formatKey(key)}
                    </td>
                    <td className="py-3 text-gray-800">
                      {formatValue(key, value)}
                    </td>
                  </tr>
                ))}
            </tbody>
          </table>
        </div>
      </div>
    </DialogContent>
  );
};
