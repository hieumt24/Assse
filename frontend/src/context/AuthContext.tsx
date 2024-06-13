import React, {
  ReactNode,
  createContext,
  useEffect,
  useMemo,
  useState,
} from "react";

export interface AuthContextProps {
  token: string | null;
}

export const AuthContext = createContext<AuthContextProps>({
  token: null,
});

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [token, setToken] = useState<string | null>(null);

  useEffect(() => {
    const storedToken = localStorage.getItem("token");

    if (storedToken) {
      setToken(storedToken);
    }
  }, [token]);

  const authContextValue: AuthContextProps = useMemo(
    () => ({
      token,
    }),
    [token],
  );

  return (
    <AuthContext.Provider value={authContextValue}>
      {children}
    </AuthContext.Provider>
  );
};
