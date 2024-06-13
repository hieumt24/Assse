import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbList,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb";
import { CreateUser, ManageUser } from "@/pages/admin/";
import { Link, Route, Routes, useLocation } from "react-router-dom";
import { Sidebar } from "./SideBar";

export const Admin = () => {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter(Boolean);

  return (
    <div className="h-full items-start">
      <Breadcrumb className="bg-red-600 p-6 flex justify-between">
        <BreadcrumbList className="text-xl font-bold text-white">
          {pathnames.length > 0 && (
            <>
              <BreadcrumbItem>
                <Link to="/admin">Admin</Link>
              </BreadcrumbItem>
              <BreadcrumbSeparator />
            </>
          )}

        </BreadcrumbList>
        <BreadcrumbList className="text-xl font-bold text-white">
          <BreadcrumbItem>
            
          </BreadcrumbItem>
        </BreadcrumbList>
      </Breadcrumb>
      <div className="flex flex-grow h-full">
          <Sidebar />  
        <Routes>
          <Route path="user" element={<ManageUser />} />
          <Route path="user/create-user" element={<CreateUser />} />
        </Routes>
      </div>
    </div>
  );
};
