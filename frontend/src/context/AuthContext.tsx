import { jwtDecode } from "jwt-decode";
import React, {
  ReactNode,
  createContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { useNavigate } from "react-router-dom";

interface User {
  id: string;
  username: string;
  dateOfBirth: string;
  isFirstTimeLogin: boolean;
  staffCode: string;
  role: string;
  location: string;
}

export interface AuthContextProps {
  token: string | null;
  isAuthenticated: boolean;
  setIsAuthenticated: (value: boolean) => void;
  user: User;
  setUser: (value: User) => void;
}

export const AuthContext = createContext<AuthContextProps>({
  token: null,
  isAuthenticated: false,
  setIsAuthenticated: () => {},
  user: {
    id: "",
    username: "",
    dateOfBirth: "",
    isFirstTimeLogin: false,
    staffCode: "",
    role: "Staff",
    location: "",
  },
  setUser: () => {},
});

interface AuthProviderProps {
  children: ReactNode;
}

interface Token {
  UserId: string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": string;
  StaffCode: string;
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string;
  IsFirstTimeLogin: string;
  DateOfBirth: string;
  Location: string;
  exp: number;
}

const isTokenExpired = (token: string): boolean => {
  const decodedToken = jwtDecode<Token>(token);
  const currentTime = Math.floor(Date.now() / 1000);
  return decodedToken.exp < currentTime;
};

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [token, setToken] = useState<string | null>(
    localStorage.getItem("token"),
  );
  const [user, setUser] = useState<User>({
    id: "",
    username: "",
    dateOfBirth: "",
    isFirstTimeLogin: false,
    staffCode: "",
    role: "Staff",
    location: "",
  });

  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(!!token);
  const fetchUserFromToken = () => {
    const storedToken = localStorage.getItem("token");
    const navigate = useNavigate();

    if (storedToken) {
      if (isTokenExpired(storedToken)) {
        localStorage.removeItem("token");
        setToken(null);
        setIsAuthenticated(false);
        navigate("/auth/login");
      } else {
        setIsAuthenticated(true);
        setToken(storedToken);
        const decodedToken = jwtDecode<Token>(storedToken);
        setUser({
          id: decodedToken.UserId,
          username:
            decodedToken[
              "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
            ],
          dateOfBirth: decodedToken.DateOfBirth,
          isFirstTimeLogin: decodedToken.IsFirstTimeLogin === "true",
          staffCode: decodedToken.StaffCode,
          role: decodedToken[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
          ],
          location: decodedToken.Location,
        });
      }
    } else {
      setUser({
        id: "",
        username: "",
        dateOfBirth: "",
        isFirstTimeLogin: false,
        staffCode: "",
        role: "Staff",
        location: "",
      });
      setIsAuthenticated(false);
    }
  };

  useEffect(() => {
    fetchUserFromToken();
  }, [token, isAuthenticated]);

  const authContextValue: AuthContextProps = useMemo(
    () => ({
      token,
      isAuthenticated,
      setIsAuthenticated,
      user,
      setUser,
    }),
    [token, user, isAuthenticated],
  );

  return (
    <AuthContext.Provider value={authContextValue}>
      {children}
    </AuthContext.Provider>
  );
};
