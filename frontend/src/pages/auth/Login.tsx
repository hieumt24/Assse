import { LoginForm } from "@/components";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { useLoading } from "@/context/LoadingContext";
import { useAuth } from "@/hooks";
import { Navigate } from "react-router-dom";

export const Login: React.FC = () => {
  const { isAuthenticated } = useAuth();
  const { isLoading } = useLoading();

  if (isAuthenticated) return <Navigate to="/" />;

  return (
    <div className="flex h-screen flex-col items-center justify-center bg-gradient-to-br from-blue-100 to-red-100 dark:from-gray-800 dark:to-gray-900">
      <div className="mb-24 flex flex-col items-center justify-center gap-2">
        <img src="/logo.svg" alt="Logo" className="h-24 w-24 sm:h-32 sm:w-32" />
        <p className="text-center text-xl font-bold text-red-600 sm:text-2xl">
          Rookies - Group 4
        </p>
      </div>
      <div className="mb-48 flex w-full justify-center border">
        {isLoading ? <LoadingSpinner /> : <LoginForm />}
      </div>
    </div>
  );
};
