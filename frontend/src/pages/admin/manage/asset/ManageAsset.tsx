import { Button } from "@/components/ui/button";
import { Outlet, useNavigate } from "react-router-dom";

export const ManageAsset = () => {
  const navigate = useNavigate();
  return (
    <div className="m-24 flex h-full flex-grow flex-col gap-8">
      <p className="text-2xl font-bold text-red-600">Asset List</p>
      <div className="flex items-center justify-between">
        <Button
          variant={"destructive"}
          onClick={() => navigate("/admin/asset/create-asset")}
        >
          <span className="capitalize">Create new asset</span>
        </Button>
      </div>
      <Outlet />
    </div>
  );
};
