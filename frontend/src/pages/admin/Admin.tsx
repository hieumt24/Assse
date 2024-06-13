import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbList,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb";
import { Button } from "@/components/ui/button";
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "@/components/ui/collapsible";
import { CreateUser, ManageUser } from "@/pages/admin/";
import { } from "@radix-ui/react-collapsible";
import { useState } from "react";
import { FaUser } from "react-icons/fa";
import { Link, Route, Routes, useLocation } from "react-router-dom";
import { Sidebar } from "./SideBar";

export const Admin = () => {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter(Boolean);
  const [isUserMenuOpen, setIsUserMenuOpen] = useState(false);

  return (
    <div className="h-full items-start">
      <div className="w-full bg-red-600 flex justify-between p-6">
        <Breadcrumb className="flex">
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
        <Collapsible
          open={isUserMenuOpen}
          onOpenChange={setIsUserMenuOpen}
          className="relative"
        >
            <CollapsibleTrigger
              asChild
            >
              <Button variant="ghost" size="sm" className="text-white text-xl hover:text-red-600 hover:bg-white">
                <FaUser className="mx-2"/>
                <span className="text-sm">&#9660;</span>
              </Button>
            </CollapsibleTrigger>
            <CollapsibleContent className="space-y-2 absolute right-0 bg-white w-[150px] shadow-md font-semibold">
              <div className=" px-4 py-3 text-sm hover:bg-zinc-200 transition-all">
                Change password
              </div>
              <div className=" px-4 py-3 text-sm hover:bg-zinc-200 transition-all">
                Log out
              </div>
            </CollapsibleContent>
        </Collapsible>
      </div>
      
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
