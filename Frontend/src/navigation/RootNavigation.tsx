import { RootTabNavigation } from "./TabNavigation/RootTabNavigation";
import { AuthStackNavigation } from "../navigation/StackNavigation";
import { getData } from "../utils";
import { useAuth } from "../contexts/AuthContext";

export const RootNavigation = () => {
  // const token = getData("AUTH_TOKEN");
  // if (token !== null) {
  //   return <RootTabNavigation />;
  // } else {
  //   return <AuthStackNavigation />;
  // }
  const auth = useAuth();
  if (auth && auth.authState?.isLoggedIn === false) {
    return <AuthStackNavigation />;
  } else {
    return <RootTabNavigation />;
  }
  // if (getData('AUTH_TOKEN') !== null) {
  //   return <AuthStackNavigation />;
  // } else return <RootTabNavigation />;
  // return <HomeStackNavigation />;
};
