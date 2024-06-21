import { Button } from "@/components/ui/button";
import { AssetRes } from "@/models";
import { ColumnDef } from "@tanstack/react-table";
import { IoMdArrowDropdown, IoMdArrowDropup } from "react-icons/io";
import { MdDelete, MdEdit } from "react-icons/md";
import { useNavigate } from "react-router-dom";

interface AssetColumnsProps {
  handleOpenDisable: (id: string) => void;
  setOrderBy: React.Dispatch<React.SetStateAction<string>>;
  setIsDescending: React.Dispatch<React.SetStateAction<boolean>>;
  isDescending: boolean;
}

export const assetColumns = ({
  handleOpenDisable,
  setOrderBy,
  setIsDescending,
  isDescending,
}: AssetColumnsProps): ColumnDef<AssetRes>[] => [
  {
    accessorKey: "assetCode",
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
    accessorKey: "assetName",
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
      const asset = row.original;
      return <p>{`${asset.assetName}`}</p>;
    },
  },
  {
    accessorKey: "categoryName",
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
              CATEGORY
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
    accessorKey: "state",
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
              STATE
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
      const state = row.original.state;
      switch (state) {
        case 1:
          return <p className="text-green-600">Available</p>;
        case 2:
          return <p className="text-yellow-600">Not Available</p>;
        case 3:
          return <p className="text-red-600">Assigned</p>;
        case 4:
          return <p className="text-red-600">WaitingForRecycling</p>;
        case 5:
          return <p className="text-red-600">Recycled</p>;
        default:
          return <p>{}</p>;
      }
    },
  },
  {
    accessorKey: "action",
    header: "Actions",
    cell: ({ row }) => {
      const asset = row.original;
      // eslint-disable-next-line react-hooks/rules-of-hooks
      const navigate = useNavigate();
      return (
        <div className="flex gap-1">
          <button
            className="text-blue-500 hover:text-blue-700"
            onClick={(e) => {
              e.stopPropagation();
              navigate(`edit/${asset.id}`);
            }}
          >
            <MdEdit size={20} />
          </button>
          <button
            className="text-red-500 hover:text-red-700"
            onClick={(e) => {
              e.stopPropagation();
              handleOpenDisable(asset.id!);
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
      const asset = row.original;
      return <div className="hidden">{asset.id}</div>;
    },
  },
];
