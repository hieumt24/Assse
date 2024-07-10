import { LoginForm } from "@/components";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { Card, CardContent } from "@/components/ui/card";
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
      <Card className="mb-60 w-full max-w-xl shadow-lg">
        <CardContent className="p-6">
          {isLoading ? <LoadingSpinner className="" /> : <LoginForm />}
        </CardContent>
      </Card>
    </div>
  );
};
