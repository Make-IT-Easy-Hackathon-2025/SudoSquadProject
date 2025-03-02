import { useState } from "react";
import { UserStatistics } from "../../utils";
import { get } from "../../services/APIService";
import { useAuth } from "../../contexts/AuthContext";

export const useProfile = () => {
  const [statistics, setStatistics] = useState<UserStatistics>();
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string>("");

  const auth = useAuth();

  let username: string | null | undefined = "";

  if (auth && auth.authState?.user) {
    username = auth.authState?.user;
  }

  const getUserStaistics = async () => {
    try {
      setLoading((prev) => !prev);
      const response: UserStatistics = await get("/users/statistcs");

      setStatistics(response);
      setLoading((prev) => !prev);
    } catch (e: any) {
      setError(e.message);
    }
  };

  return { statistics, setStatistics, loading, getUserStaistics, username };
};
