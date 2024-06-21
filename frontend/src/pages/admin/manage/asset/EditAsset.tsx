import { LoadingSpinner } from "@/components/LoadingSpinner";
import { EditAssetForm } from "@/components/forms/asset/EditAssetForm";
import { useLoading } from "@/context/LoadingContext";

export const EditAsset = () => {
  const { isLoading } = useLoading();
  return (
    <div className="mt-16 flex h-fit flex-grow justify-center">
      {isLoading ? <LoadingSpinner /> : <EditAssetForm />}
    </div>
  );
};
