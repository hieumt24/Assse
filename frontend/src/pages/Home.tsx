import { useAuth } from "@/hooks";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export const Home = () => {
  const { token } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (!token) {
      console.log("Not logged in");

      navigate("/auth/login");
    }
  }, [navigate, token]);

  return <div>Home</div>;
};
