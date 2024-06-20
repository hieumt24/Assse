import { FullPageModal } from "@/components/FullPageModal";
import { Dialog, DialogContent, DialogHeader } from "@/components/ui/dialog";

interface DialogProps {
  open: boolean;
  setOpen: (open: boolean) => void;
}

export const CreateCategoryForm = (props: DialogProps) => {
  const { open, setOpen } = props;
  return (
    <FullPageModal show={open}>
      <Dialog open={open} onOpenChange={setOpen}>
        <DialogContent>
          <DialogHeader>Add new category</DialogHeader>
        </DialogContent>
      </Dialog>
    </FullPageModal>
  );
};
