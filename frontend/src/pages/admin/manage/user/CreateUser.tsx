import { CreateUserForm } from "@/components";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { useLoading } from "@/context/LoadingContext";

export const CreateUser = () => {
  const { isLoading } = useLoading();
  return (
    <div className="mt-16 flex h-fit flex-grow justify-center">
      {isLoading ? <LoadingSpinner /> : <CreateUserForm />}
    </div>
  );
};
