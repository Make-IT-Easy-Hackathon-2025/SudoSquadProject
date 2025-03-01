import { RootTabNavigation } from "./TabNavigation/RootTabNavigation";
import { AuthStackNavigation } from "../navigation/StackNavigation";
import { getData } from "../utils";

export const RootNavigation = () => {
  if (getData("token") !== null) {
    return <AuthStackNavigation />;
  } else return <RootTabNavigation />;
};
