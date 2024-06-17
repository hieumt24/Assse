import { LoginForm } from "@/components";
import { useAuth } from "@/hooks";
import { Navigate } from "react-router-dom";

export const Login = () => {
  const {isAuthenticated} = useAuth();
  if (isAuthenticated) {
    return <Navigate to="/"/>
  }
  return (
    <div className="flex h-screen flex-col items-center justify-center">
      <LoginForm />
    </div>
  );
};
