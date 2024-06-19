import { UserRes } from "@/models";
import { ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { MdDelete, MdEdit } from "react-icons/md";
import { useNavigate } from "react-router-dom";

interface UserColumnsProps {
  handleOpenDisable: (id: string) => void;
}

export const userColumns = ({
  handleOpenDisable,
}: UserColumnsProps): ColumnDef<UserRes>[] => [
  {
    accessorKey: "staffCode",
    header: "Staff Code",
  },
  {
    accessorKey: "fullName",
    header: "Full Name",
    cell: ({ row }) => {
      const user = row.original;
      return <p>{`${user.firstName!} ${user.lastName}`}</p>;
    },
  },
  {
    accessorKey: "username",
    header: "Username",
  },
  {
    accessorKey: "joinedDate",
    header: "Joined Date",
    cell: ({ row }) => {
      const formattedDate = format(row.original.joinedDate, "MM/dd/yyyy");
      return <p>{formattedDate}</p>;
    },
  },
  {
    accessorKey: "role",
    header: "Type",
    cell: ({ row }) => {
      const role = row.original.role;
      return <p>{role === 1 ? "Admin" : "Staff"}</p>;
    },
  },
  {
    accessorKey: "action",
    header: "Actions",
    cell: ({ row }) => {
      const user = row.original;
      const navigate = useNavigate();
      return (
        <div className="flex gap-1">
          <button
            className="text-blue-500 hover:text-blue-700"
            onClick={(e) => {
              e.stopPropagation();
              navigate(`edit/${user.staffCode}`);
            }}
          >
            <MdEdit size={20} />
          </button>
          <button
            className="text-red-500 hover:text-red-700"
            onClick={(e) => {
              e.stopPropagation();
              handleOpenDisable(user.id);
            }}
          >
            <MdDelete size={20} />
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
