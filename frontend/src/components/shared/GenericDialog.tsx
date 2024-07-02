import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { SetStateAction } from "react";
import { Button } from "../ui/button";
import { useLoading } from "@/context/LoadingContext";

interface GenericDialogProps {
  trigger?: string;
  title: string;
  desc: string;
  confirmText: string;
  cancelText?: string;
  onConfirm?: () => void;
  open?: boolean;
  setOpen?: React.Dispatch<SetStateAction<boolean>>;
  classButton?: string;
  variant?:
    | "link"
    | "default"
    | "destructive"
    | "outline"
    | "secondary"
    | "ghost"
    | null
    | undefined;
}

export const GenericDialog = (props: GenericDialogProps) => {
  const {
    trigger,
    title,
    desc,
    confirmText,
    cancelText,
    onConfirm,
    open,
    setOpen,
    variant,
    classButton,
  } = props;

  const {isLoading} = useLoading();

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      {trigger && (
        <DialogTrigger className="w-full">
          <Button type="button" variant={variant} className={classButton} >
            {trigger}
          </Button>
        </DialogTrigger>
      )}
      <DialogContent className="max-w-md">
        <DialogHeader>
          <DialogTitle>
            <p className="py-2 text-center text-2xl font-bold text-red-600">
              {title}
            </p>
          </DialogTitle>
          <DialogDescription>
            <p className="text-center text-lg">{desc}</p>
          </DialogDescription>
        </DialogHeader>
        <div className="mt-4 flex w-full justify-center gap-4">
          <Button type="button" variant="destructive" onClick={onConfirm} disabled={isLoading}>
            {isLoading ? "Loading..." : confirmText}
          </Button>
          <DialogClose asChild>
            <Button type="button">{cancelText || "Cancel"}</Button>
          </DialogClose>
        </div>
      </DialogContent>
    </Dialog>
  );
};
