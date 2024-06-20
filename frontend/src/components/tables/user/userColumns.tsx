import { Button } from "@/components/ui/button";
import { UserRes } from "@/models";
import { ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { IoMdArrowDropdown, IoMdArrowDropup } from "react-icons/io";
import { MdDelete, MdEdit } from "react-icons/md";
import { useNavigate } from "react-router-dom";

interface UserColumnsProps {
  handleOpenDisable: (id: string) => void;
  setOrderBy: React.Dispatch<React.SetStateAction<string>>;
  setIsDescending: React.Dispatch<React.SetStateAction<boolean>>;
  isDescending: boolean;
}

export const userColumns = ({
  handleOpenDisable,
  setOrderBy,
  setIsDescending,
  isDescending,
}: UserColumnsProps): ColumnDef<UserRes>[] => [
  {
    accessorKey: "staffCode",
    header: ({ column }) => {
      return (
        <Button
          variant={"ghost"}
          onClick={() => {
            setOrderBy(column.id);
            setIsDescending((prev) => !prev);
          }}
          className="p-0 hover:bg-muted/50"
        >
          <div className="flex items-center justify-center">
            <span
              className={`${!isDescending ? "font-black text-red-600" : ""}`}
            >
              {column.id.toUpperCase()}
            </span>
            {isDescending ? (
              <IoMdArrowDropup size={24} />
            ) : (
              <IoMdArrowDropdown
                size={24}
                className={`${!isDescending ? "font-black text-red-600" : ""}`}
              />
            )}
          </div>
        </Button>
      );
    },
  },
  {
    accessorKey: "fullName",
    header: ({ column }) => {
      return (
        <Button
          variant={"ghost"}
          onClick={() => {
            setOrderBy(column.id);
            setIsDescending((prev) => !prev);
          }}
          className="p-0 hover:bg-muted/50"
        >
          <div className="flex items-center justify-center">
            <span
              className={`${!isDescending ? "font-black text-red-600" : ""}`}
            >
              {column.id.toUpperCase()}
            </span>
            {isDescending ? (
              <IoMdArrowDropup size={24} />
            ) : (
              <IoMdArrowDropdown
                size={24}
                className={`${!isDescending ? "font-black text-red-600" : ""}`}
              />
            )}
          </div>
        </Button>
      );
    },
    cell: ({ row }) => {
      const user = row.original;
      return <p>{`${user.firstName} ${user.lastName}`}</p>;
    },
  },
  {
    accessorKey: "username",
    header: ({ column }) => {
      return (
        <Button
          variant={"ghost"}
          onClick={() => {
            setOrderBy(column.id);
            setIsDescending((prev) => !prev);
          }}
          className="p-0 hover:bg-muted/50"
        >
          <div className="flex items-center justify-center">
            <span
              className={`${!isDescending ? "font-black text-red-600" : ""}`}
            >
              {column.id.toUpperCase()}
            </span>
            {isDescending ? (
              <IoMdArrowDropup size={24} />
            ) : (
              <IoMdArrowDropdown
                size={24}
                className={`${!isDescending ? "font-black text-red-600" : ""}`}
              />
            )}
          </div>
        </Button>
      );
    },
  },
  {
    accessorKey: "joinedDate",
    header: ({ column }) => {
      return (
        <Button
          variant={"ghost"}
          onClick={() => {
            setOrderBy(column.id);
            setIsDescending((prev) => !prev);
          }}
          className="p-0 hover:bg-muted/50"
        >
          <div className="flex items-center justify-center">
            <span
              className={`${!isDescending ? "font-black text-red-600" : ""}`}
            >
              {column.id.toUpperCase()}
            </span>
            {isDescending ? (
              <IoMdArrowDropup size={24} />
            ) : (
              <IoMdArrowDropdown
                size={24}
                className={`${!isDescending ? "font-black text-red-600" : ""}`}
              />
            )}
          </div>
        </Button>
      );
    },
    cell: ({ row }) => {
      const formattedDate = format(row.original.joinedDate, "MM/dd/yyyy");
      return <p>{formattedDate}</p>;
    },
  },
  {
    accessorKey: "role",
    header: ({ column }) => {
      return (
        <Button
          variant={"ghost"}
          onClick={() => {
            setOrderBy(column.id);
            setIsDescending((prev) => !prev);
          }}
          className="p-0 hover:bg-muted/50"
        >
          <div className="flex items-center justify-center">
            <span
              className={`${!isDescending ? "font-black text-red-600" : ""}`}
            >
              TYPE
            </span>
            {isDescending ? (
              <IoMdArrowDropup size={24} />
            ) : (
              <IoMdArrowDropdown
                size={24}
                className={`${!isDescending ? "font-black text-red-600" : ""}`}
              />
            )}
          </div>
        </Button>
      );
    },
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
