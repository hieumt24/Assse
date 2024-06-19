import { LoadingSpinner } from "@/components/LoadingSpinner";
import { EditUserForm } from "@/components/forms/user/EditUserForm";
import { useLoading } from "@/context/LoadingContext";

export const EditUser = () => {
  const { isLoading } = useLoading();
  return (
    <div className="mt-16 flex h-fit flex-grow justify-center">
      {isLoading ? <LoadingSpinner /> : <EditUserForm />}
    </div>
  );
};
