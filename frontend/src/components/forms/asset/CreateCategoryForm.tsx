import { FullPageModal } from "@/components/FullPageModal";
import { Dialog, DialogContent } from "@/components/ui/dialog";

interface DialogProps {
  open: boolean;
  setOpen: (open: boolean) => void;
}

export const CreateCategoryForm = (props: DialogProps) => {
  const { open, setOpen } = props;
  return (
    <FullPageModal show={open}>
      <Dialog open={open} onOpenChange={setOpen}>
        <DialogContent>Add new category</DialogContent>
      </Dialog>
    </FullPageModal>
  );
};
