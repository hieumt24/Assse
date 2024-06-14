import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbList,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb";
import { Button } from "@/components/ui/button";
import {
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger,
} from "@/components/ui/collapsible";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { useAuth } from "@/hooks";
import useClickOutside from "@/hooks/useClickOutside";
import { useCallback, useRef, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { Separator } from "./ui/separator";

export const Header = () => {
  const { user, setIsAuthenticated } = useAuth();
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter(Boolean);
  const [isUserMenuOpen, setIsUserMenuOpen] = useState(false);
  const [openPopup, setOpenPopup] = useState(false);
  const collapsibleRef = useRef<HTMLDivElement>(null);

  const navigate = useNavigate();
  const handleLogout = () => {
    localStorage.removeItem("token");
    setIsAuthenticated(false);
    navigate("/auth/login");
  };

  const handleClickOutside = useCallback(() => {
    if (openPopup) return; // Do not close if the popup is open
    setIsUserMenuOpen(false);
  }, [openPopup]);

  useClickOutside(collapsibleRef, handleClickOutside);
  
  return (
    <div className="flex w-full justify-between bg-red-600 p-6">
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
        ref={collapsibleRef}
      >
        <CollapsibleTrigger asChild>
          <Button
            variant="ghost"
            size="sm"
            className="text-xl text-white hover:bg-white hover:text-red-600"
          >
            <span className="mr-2">{user.username}</span>
            <span className="text-xs">&#9660;</span>
          </Button>
        </CollapsibleTrigger>
        <CollapsibleContent className="absolute right-0 mt-1 w-40 rounded-md bg-white font-semibold shadow-md">
          <Link
            to="/changepassword"
            className="block rounded-t-md px-4 py-3 text-sm font-medium transition-all hover:bg-zinc-200"
          >
            Change password
          </Link>
          <Separator />
          <Dialog open={openPopup} onOpenChange={setOpenPopup}>
            <DialogTrigger className="w-full py-2 text-start text-sm transition-all hover:bg-zinc-200">
              <p className="ms-4 font-medium">Log out</p>
            </DialogTrigger>
            <DialogContent className="border-2">
              <DialogHeader>
                <DialogTitle className="text-center text-2xl font-bold text-red-600">
                  Are you sure?
                </DialogTitle>
                <DialogDescription className="text-center text-lg">
                  Do you want to logout?
                </DialogDescription>
                <div className="items-ceter flex justify-center gap-4">
                  <Button variant={"destructive"} onClick={handleLogout}>
                    Log out
                  </Button>
                  <Button variant="outline" onClick={() => setOpenPopup(false)}>
                    Cancel
                  </Button>
                </div>
              </DialogHeader>
            </DialogContent>
          </Dialog>
        </CollapsibleContent>
      </Collapsible>
    </div>
  );
};
