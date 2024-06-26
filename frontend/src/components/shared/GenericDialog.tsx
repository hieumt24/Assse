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

interface GenericDialogProps {
  trigger?: string;
  title: string;
  desc: string;
  confirmText: string;
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
    onConfirm,
    open,
    setOpen,
    variant,
    classButton,
  } = props;

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger className="w-full">
        <Button type="button" variant={variant} className={classButton}>
          {trigger}
        </Button>
      </DialogTrigger>
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
          <Button type="button" variant="destructive" onClick={onConfirm}>
            {confirmText}
          </Button>
          <DialogClose asChild>
            <Button type="button">Cancel</Button>
          </DialogClose>
        </div>
      </DialogContent>
    </Dialog>
  );
};
