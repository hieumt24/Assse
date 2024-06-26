import { renderHeader } from "@/lib/utils";
import { AssignmentRes } from "@/models";
import { ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { FiEdit2 } from "react-icons/fi";
import { IoCloseCircleOutline } from "react-icons/io5";
import { useNavigate } from "react-router-dom";

interface AssignmentColumnsProps {
  handleOpenDisable: (id: string) => void;
  setOrderBy: React.Dispatch<React.SetStateAction<string>>;
  setIsDescending: React.Dispatch<React.SetStateAction<boolean>>;
  isDescending: boolean;
  orderBy: string;
}

export const assignmentColumns = ({
  handleOpenDisable,
  setOrderBy,
  setIsDescending,
  isDescending,
  orderBy,
}: AssignmentColumnsProps): ColumnDef<AssignmentRes>[] => [
  {
    accessorKey: "id",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
  },
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
    accessorKey: "assignedTo",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
  },
  {
    accessorKey: "assignedBy",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
  },
  {
    accessorKey: "assignedDate",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
    cell: ({ row }) => {
      const formattedDate = format(row.original.assignedDate!, "dd/MM/yyyy");
      return <p>{formattedDate}</p>;
    },
  },
  {
    accessorKey: "action",
    header: "Actions",
    cell: ({ row }) => {
      const asignment = row.original;
      // eslint-disable-next-line react-hooks/rules-of-hooks
      const navigate = useNavigate();
      return (
        <div className="flex gap-4">
          <button
            className="text-blue-500 hover:text-blue-700"
            onClick={(e) => {
              e.stopPropagation();
              navigate(`edit/${asignment.id}`);
            }}
          >
            <FiEdit2 size={18} />
          </button>
          <button
            className="text-red-500 hover:text-red-700"
            onClick={(e) => {
              e.stopPropagation();
              handleOpenDisable(asignment.id!);
            }}
          >
            <IoCloseCircleOutline size={20} />
          </button>
        </div>
      );
    },
  },
];
