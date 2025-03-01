import React, {
  createContext,
  useState,
  useContext,
  PropsWithChildren,
} from "react";
import { User } from "../utils/types/User";
import { removeData, storeData } from "../utils";
import { post } from "../services/APIService";

type AuthState = {
  user: User | null;
  loading: boolean;
  error: string | null;
  isLoggedIn: boolean;
};

type response = { token: string };

export type AuthProps = {
  authState?: AuthState;
  login? (user: any): Promise<response | undefined>;
  register? (user: any): Promise<response | undefined>;
  logout? (): Promise<void>;
};

const AuthContext = createContext<AuthProps>({});

export const useAuth = () => {
  return useContext(AuthContext);
};

export const AuthProvider = ({ children }: PropsWithChildren<{}>) => {
  const [authState, setAuthState] = useState<AuthState>({
    user: null,
    loading: false,
    error: null,
    isLoggedIn: false,
  });

  const login = async (user: any): Promise<response | undefined> => {
    try {
      setAuthState({ ...authState, loading: true });
      const response = await post<response>("/users/login", user);
      setAuthState({
        user: null,
        loading: false,
        error: null,
        isLoggedIn: true,
      });
      await storeData('AUTH_TOKEN', response.token);
      return response;
    } catch (error: any) {
      setAuthState({ ...authState, loading: false, error: error.message });
    }
  };

  const register = async (user: any): Promise<response | undefined> => {
    try {
      setAuthState({ ...authState, loading: true });
      const response = await post<response>("/auth/register", user);
      setAuthState({
        user: null,
        loading: false,
        error: null,
        isLoggedIn: true,
      });
      return response;
    } catch (error: any) {
      setAuthState({ ...authState, loading: false, error: error.message });
    }
  };

  const logout = async () => {
    setAuthState({ ...authState, loading: true });
    const token = await removeData("token");
    setAuthState({
      user: null,
      loading: false,
      error: null,
      isLoggedIn: false,
    });
  };

  return (
    <AuthContext.Provider value={{ authState, login, register, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
