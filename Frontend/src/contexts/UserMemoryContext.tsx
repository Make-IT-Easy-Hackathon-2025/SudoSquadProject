import { createContext, PropsWithChildren, useContext, useState } from "react";
import { get, post } from "../services/APIService";
import { UserMemory } from "../utils/types/UserMemory";
import { Quiz } from "../utils";

type UserMemoryState = {
  userMemories: UserMemory[];
  loading: boolean;
  error: string | null;
};

export type UserMemoryProps = {
  userMemoryState?: UserMemoryState;
  getUserMemories?(): Promise<UserMemory[] | undefined>;
  createUserMemory?(data: UserMemory): Promise<UserMemory | undefined>;
  storeQuizToUserMemory?(data: UserMemory): any;
};

const UserMemoryContext = createContext<UserMemoryProps>({});

export const getUserMemoryContext = () => {
  return useContext(UserMemoryContext);
};

export const UserMemoryProvider = ({ children }: PropsWithChildren<{}>) => {
  const [userMemoryState, setUserMemoryState] = useState<UserMemoryState>({
    userMemories: [],
    loading: false,
    error: null,
  });

  const getUserMemories = async (): Promise<UserMemory[] | undefined> => {
    try {
      setUserMemoryState({ ...userMemoryState, loading: true });
      const response = await get<UserMemory[]>("/memories");
      setUserMemoryState({
        userMemories: response,
        loading: false,
        error: null,
      });
      return response;
    } catch (error: any) {
      setUserMemoryState({
        ...userMemoryState,
        loading: false,
        error: error.message,
      });
    }
  };

  const createUserMemory = async (
    data: UserMemory
  ): Promise<UserMemory | undefined> => {
    try {
      setUserMemoryState({ ...userMemoryState, loading: true });
      const response = await post<UserMemory>("/memories", data);
      setUserMemoryState({
        userMemories: [...userMemoryState.userMemories, response],
        loading: false,
        error: null,
      });
      return response;
    } catch (error: any) {
      setUserMemoryState({
        ...userMemoryState,
        loading: false,
        error: error.message,
      });
    }
  };

  const storeQuizToUserMemory = async (data: UserMemory) => {
    try {
      setUserMemoryState({
        ...userMemoryState,
        loading: true,
        userMemories: [...userMemoryState.userMemories, data],
      });
      setUserMemoryState({ ...userMemoryState, loading: false });
    } catch (error: any) {
      setUserMemoryState({
        ...userMemoryState,
        loading: false,
        error: error.message,
      });
    }
  };
  return (
    <UserMemoryContext.Provider
      value={{
        userMemoryState,
        getUserMemories,
        createUserMemory,
        storeQuizToUserMemory,
      }}
    >
      {children}
    </UserMemoryContext.Provider>
  );
};
