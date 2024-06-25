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
import { ADMIN_NAV_FUNCTIONS, BREADCRUMB_COMPONENTS } from "@/constants";
import { useAuth } from "@/hooks";
import useClickOutside from "@/hooks/useClickOutside";
import { useCallback, useRef, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { ChangePasswordForm } from "./forms/user/ChangePasswordForm";
import { GenericDialog } from "./shared";
import { Separator } from "./ui/separator";

export const Header = () => {
  const { user, setIsAuthenticated } = useAuth();
  const location = useLocation();
  const [isUserMenuOpen, setIsUserMenuOpen] = useState(false);
  const [openLogout, setOpenLogout] = useState(false);
  const [openChangePassword, setOpenChangePassword] = useState(false);
  const collapsibleRef = useRef<HTMLDivElement>(null);
  const navigate = useNavigate();
  const handleLogout = () => {
    localStorage.removeItem("token");
    setIsAuthenticated(false);
    navigate("/auth/login");
    toast.success("You have been logged out");
  };

  const handleClickOutside = useCallback(() => {
    if (openChangePassword || openLogout) return;
    setIsUserMenuOpen(false);
  }, [openLogout, openChangePassword]);

  useClickOutside(collapsibleRef, handleClickOutside);

  return (
    <div className="flex w-full justify-between bg-red-600 p-6">
      <Breadcrumb className="flex">
        <BreadcrumbList className="text-xl font-bold text-white">
          {ADMIN_NAV_FUNCTIONS.map((item) => {
            return location.pathname.includes(item.path) ? (
              <>
                <BreadcrumbItem>
                  <Link to={item.path}>{item.name}</Link>
                </BreadcrumbItem>
              </>
            ) : (
              <></>
            );
          })}
          {BREADCRUMB_COMPONENTS.map((item) => {
            return location.pathname.includes(item.path) ? (
              <>
                <BreadcrumbSeparator />
                <BreadcrumbItem>
                  <Link to={item.path}>{item.name}</Link>
                </BreadcrumbItem>
              </>
            ) : (
              <></>
            );
          })}
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
          <div
            onClick={() => {
              setOpenChangePassword(true);
            }}
            className="block rounded-t-md px-4 py-3 text-sm font-medium transition-all hover:cursor-pointer hover:bg-zinc-200"
          >
            Change password
          </div>
          <Separator />
          <ChangePasswordForm
            open={openChangePassword}
            onOpenChange={setOpenChangePassword}
          />
          <GenericDialog
            trigger="Log out"
            title="Are you sure?"
            desc="Do you want to logout?"
            confirmText="Log out"
            onConfirm={handleLogout}
            open={openLogout}
            setOpen={setOpenLogout}
          />
        </CollapsibleContent>
      </Collapsible>
    </div>
  );
};
