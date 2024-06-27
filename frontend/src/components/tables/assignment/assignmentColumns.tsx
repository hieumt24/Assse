import { GenericDialog } from "@/components/shared";
import { renderHeader } from "@/lib/utils";
import { AssignmentRes } from "@/models";
import { createReturnRequest } from "@/services/admin/manageReturningRequestService";
import { ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { useState } from "react";
import { FiEdit2 } from "react-icons/fi";
import { IoCloseCircleOutline, IoReload } from "react-icons/io5";
import { useNavigate } from "react-router-dom";

interface AssignmentColumnsProps {
  handleOpenDisable: (id: string) => void;
  setOrderBy: React.Dispatch<React.SetStateAction<string>>;
  setIsDescending: React.Dispatch<React.SetStateAction<boolean>>;
  isDescending: boolean;
  orderBy: string;
  requestedBy?: string;
}

export const assignmentColumns = ({
  handleOpenDisable,
  setOrderBy,
  setIsDescending,
  isDescending,
  orderBy,
  requestedBy,
}: AssignmentColumnsProps): ColumnDef<AssignmentRes>[] => [
  {
    accessorKey: "id",
    header: "",
    cell: ({ row }) => {
      const assignment = row.original;
      return <div className="hidden">{assignment.id}</div>;
    },
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
    accessorKey: "state",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
    cell: ({ row }) => {
      const state = row.original.state;
      switch (state) {
        case 1:
          return <p className="text-green-600">Accepted</p>;
        case 2:
          return <p className="text-yellow-600">Waiting for acceptance</p>;
        default:
          return <p>{}</p>;
      }
    },
  },
  {
    accessorKey: "action",
    header: "Actions",
    cell: ({ row }) => {
      const assignment = row.original;
      // eslint-disable-next-line react-hooks/rules-of-hooks
      const navigate = useNavigate();
      // eslint-disable-next-line react-hooks/rules-of-hooks
      const [openCancel, setOpenCancel] = useState(false);
      return (
        <div className="flex gap-3">
          <button
            className="text-blue-500 hover:text-blue-700"
            onClick={(e) => {
              e.stopPropagation();
              navigate(`edit/${assignment.id}`);
            }}
          >
            <FiEdit2 size={18} />
          </button>
          <button
            className="text-red-500 hover:text-red-700"
            onClick={(e) => {
              e.stopPropagation();
              handleOpenDisable(assignment.id!);
            }}
          >
            <IoCloseCircleOutline size={20} />
          </button>
          <button
            className="text-green-500 hover:text-green-700"
            onClick={(e) => {
              console.log(222);
              e.stopPropagation();
              setOpenCancel(!openCancel);
            }}
          >
            <IoReload size={20} />
            <GenericDialog
              title="Are you sure?"
              desc="Do you want to cancel this assignment?"
              confirmText="Yes"
              open={openCancel}
              setOpen={setOpenCancel}
              onConfirm={() =>
                createReturnRequest({
                  assignmentId: row.original.id,
                  requestedBy: requestedBy,
                })
              }
            />
          </button>
        </div>
      );
    },
  },
];
