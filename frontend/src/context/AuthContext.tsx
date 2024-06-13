import React, {
  ReactNode,
  createContext,
  useEffect,
  useMemo,
  useState,
} from "react";

export interface AuthContextProps {
  token: string | null;
  isFirstTime: boolean;
  setIsFirstTime: (value: boolean) => void;
}

export const AuthContext = createContext<AuthContextProps>({
  token: null,
  isFirstTime: false,
  setIsFirstTime: () => {},
});

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [token, setToken] = useState<string | null>(null);
  const [isFirstTime, setIsFirstTime] = useState(true);

  useEffect(() => {
    const storedToken = localStorage.getItem("token");

    if (storedToken) {
      setToken(storedToken);
    }
  }, [token]);

  const authContextValue: AuthContextProps = useMemo(
    () => ({
      token,
      isFirstTime,
      setIsFirstTime,
    }),
    [token, isFirstTime],
  );

  return (
    <AuthContext.Provider value={authContextValue}>
      {children}
    </AuthContext.Provider>
  );
};
