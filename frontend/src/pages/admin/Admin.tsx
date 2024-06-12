import { Routes, Route } from 'react-router-dom';
import { CreateUser, ManageUser } from "@/pages/admin/";
import { Breadcrumb, BreadcrumbItem, BreadcrumbList, BreadcrumbSeparator } from "@/components/ui/breadcrumb";
import { Link, useLocation } from "react-router-dom";
import { Sidebar } from "./SideBar";

export const Admin = () => {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter(Boolean);

  return (
    <div className="flex h-screen items-start bg-zinc-100">
      <Sidebar />
      <div className="flex flex-grow flex-col">
        <Breadcrumb className="bg-red-500 p-6">
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
        </Breadcrumb>
        <Routes>
          <Route path="user" element={<ManageUser />} />
          <Route path="user/create-user" element={<CreateUser />} />
        </Routes>
      </div>
    </div>
  );
};