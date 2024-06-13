import { jwtDecode } from "jwt-decode";
import React, {
  ReactNode,
  createContext,
  useEffect,
  useMemo,
  useState,
} from "react";

interface User {
  id: string,
  username: string,
  dateOfBirth: string,
  isFirstTimeLogin: boolean,
  staffCode: string,
  role: number,
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
    role: 2,
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
  "IsFirstTimeLogin" : boolean;
  "DateOfBirth" : string;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<User>({
    id: "",
    username: "",
    dateOfBirth: "",
    isFirstTimeLogin: false,
    staffCode: "",
    role: 2,
  });

  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

  const fetchUserFromToken = () => {
    const storedToken = localStorage.getItem("token");

    if (storedToken) {
      setIsAuthenticated(true);
      setToken(storedToken);
      const decodedToken = jwtDecode<Token>(storedToken);
      setUser({
        id: decodedToken.UserId,
        username: decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        dateOfBirth: decodedToken.DateOfBirth,
        isFirstTimeLogin: decodedToken.IsFirstTimeLogin,
        staffCode: decodedToken.StaffCode,
        role: parseInt(decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]),
      })
    }
  }

  useEffect(() => {
    fetchUserFromToken();
  }, [token, isAuthenticated]);

  const authContextValue: AuthContextProps = useMemo(
    () => ({
      token,
      isAuthenticated,
      setIsAuthenticated,
      user,
      setUser
    }),
    [token, user],
  );

  return (
    <AuthContext.Provider value={authContextValue}>
      {children}
    </AuthContext.Provider>
  );
};
