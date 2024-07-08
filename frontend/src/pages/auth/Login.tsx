import { LoginForm } from "@/components";
import { LoadingSpinner } from "@/components/LoadingSpinner";
import { useLoading } from "@/context/LoadingContext";
import { useAuth } from "@/hooks";
import { Navigate } from "react-router-dom";

export const Login = () => {
  const { isAuthenticated } = useAuth();
  const { isLoading } = useLoading();
  if (isAuthenticated) {
    return <Navigate to="/" />;
  }
  

  return (
    <div className="flex h-screen flex-col items-center justify-center">
      {isLoading ? <LoadingSpinner /> : <LoginForm />}
    </div>
  );
};
