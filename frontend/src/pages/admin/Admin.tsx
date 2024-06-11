import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbList,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb";
import { Link, useLocation } from "react-router-dom";
import { Content } from "./Content";
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
        <Content />
      </div>
    </div>
  );
};
