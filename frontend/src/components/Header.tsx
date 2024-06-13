import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbList,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb";
import { Button } from "@/components/ui/button";
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "@/components/ui/collapsible";
import { useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";

export const Header = () => {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter(Boolean);
  const [isUserMenuOpen, setIsUserMenuOpen] = useState(false);
  const navigate = useNavigate();
  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/");
  }
    return (
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
                <span className="mr-2">usernamemockup</span>
                <span className="text-xs">&#9660;</span>
              </Button>
            </CollapsibleTrigger>
            <CollapsibleContent className="space-y-2 absolute right-0 mt-1 bg-white w-[150px] shadow-md font-semibold">
              <Link to="/changepassword" className="block px-4 py-3 text-sm hover:bg-zinc-200 transition-all">
                Change password
              </Link>
              <div 
                className=" px-4 py-3 text-sm hover:bg-zinc-200 transition-all hover:cursor-pointer"
                onClick={handleLogout}>
                Log out
              </div>
            </CollapsibleContent>
        </Collapsible>
      </div>
    )
}