import { Button } from "@/components/ui/button";
import { useNavigate } from "react-router-dom";

export const ManageAssignment = () => {
  const navigate = useNavigate();
  return (
    <div className="m-24 flex h-full flex-grow flex-col gap-8">
      <p className="text-2xl font-bold text-red-600">Assignment List</p>
      <div className="flex items-center justify-between">
        <div className="flex gap-2"></div>
        <Button
          variant={"destructive"}
          onClick={() => navigate("/assignments/create")}
        >
          <span className="capitalize">Create new assignment</span>
        </Button>
      </div>
    </div>
  );
};
