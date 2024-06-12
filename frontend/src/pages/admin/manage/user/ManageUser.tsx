import { Button } from "@/components/ui/button";
import { Outlet, useNavigate } from "react-router-dom";

export const ManageUser = () => {
  const navigate = useNavigate();
  return (
    <div className="flex h-full flex-col">
      <Button onClick={() => navigate("/admin/user/create-user")}>
        <span className="capitalize">Create user</span>
      </Button>
      <Outlet />
    </div>
  );
};
