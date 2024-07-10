import { Button } from "@/components/ui/button";
import {
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger,
} from "@/components/ui/collapsible";
import { useAuth } from "@/hooks";
import useClickOutside from "@/hooks/useClickOutside";
import { useCallback, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { MyBreadcrumb } from "./MyBreadcrumb";
import { ChangePasswordForm } from "./forms/user/ChangePasswordForm";
import { GenericDialog } from "./shared";
import { Separator } from "./ui/separator";

export const Header = () => {
  const { user, setIsAuthenticated } = useAuth();
  const [isUserMenuOpen, setIsUserMenuOpen] = useState(false);
  const [openLogout, setOpenLogout] = useState(false);
  const [openChangePassword, setOpenChangePassword] = useState(false);
  const collapsibleRef = useRef<HTMLDivElement>(null);
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.setItem("logout", Date.now().toString()); // Trigger storage event
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
      <MyBreadcrumb />
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
            cancelText="Cancel"
            onConfirm={handleLogout}
            open={openLogout}
            setOpen={setOpenLogout}
            variant={"outline"}
            classButton="border-none w-full justify-start"
          />
        </CollapsibleContent>
      </Collapsible>
    </div>
  );
};
