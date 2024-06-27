import { renderHeader } from "@/lib/utils";
import { ReturningRequestRes } from "@/models";
import { ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { IoCloseCircleOutline } from "react-icons/io5";

interface RequestColumnsProps {
  handleOpenComplete: (id: string) => void;
  handleOpenCancel: (id: string) => void;
  setOrderBy: React.Dispatch<React.SetStateAction<string>>;
  setIsDescending: React.Dispatch<React.SetStateAction<boolean>>;
  isDescending: boolean;
  orderBy: string;
}

export const returningRequestColumns = ({
  handleOpenComplete,
  handleOpenCancel,
  setOrderBy,
  setIsDescending,
  isDescending,
  orderBy,
}: RequestColumnsProps): ColumnDef<ReturningRequestRes>[] => [
  {
    accessorKey: "assetCode",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
  },
  {
    accessorKey: "assetName",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
  },
  {
    accessorKey: "requestedByUsername",
    header: ({ column }) =>
      renderHeader(
        column,
        setOrderBy,
        setIsDescending,
        isDescending,
        orderBy,
        "Requested by",
      ),
  },
  {
    accessorKey: "assignedDate",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
    cell: ({ row }) => {
      const formattedDate = format(row.original.assignedDate, "dd/MM/yyyy");
      return <p>{formattedDate}</p>;
    },
  },
  {
    accessorKey: "acceptedByUsername",
    header: ({ column }) =>
      renderHeader(
        column,
        setOrderBy,
        setIsDescending,
        isDescending,
        orderBy,
        "Accepted by",
      ),
  },
  {
    accessorKey: "returnedDate",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
    cell: ({ row }) => {
      const formattedDate = row.original.returnedDate
        ? format(row.original.returnedDate, "dd/MM/yyyy")
        : "";
      return <p>{formattedDate}</p>;
    },
  },
  {
    accessorKey: "state",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
  },
  {
    accessorKey: "action",
    header: "Actions",
    cell: ({ row }) => {
      const request = row.original;
      return (
        <div className="flex gap-4">
          <button
            className="text-blue-500 hover:text-blue-700"
            onClick={(e) => {
              e.stopPropagation();
              handleOpenComplete(request.id);
            }}
          ></button>
          <button
            className="text-red-500 hover:text-red-700"
            onClick={(e) => {
              e.stopPropagation();
              handleOpenCancel(request.id);
            }}
          >
            <IoCloseCircleOutline size={20} />
          </button>
        </div>
      );
    },
  },
  {
    accessorKey: "id",
    header: "",
    cell: ({ row }) => {
      const user = row.original;
      return <div className="hidden">{user.id}</div>;
    },
  },
];
