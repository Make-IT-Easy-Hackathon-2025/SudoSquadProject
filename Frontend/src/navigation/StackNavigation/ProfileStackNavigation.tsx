import { createStackNavigator } from "@react-navigation/stack";
import { ScreenTypes } from "../ScreenTypes";
import { ProfileScreen } from "../../screens";

const ProfileStack = createStackNavigator<ScreenTypes>();

export const ProfileStackNavigation = () => {
  return (
    <ProfileStack.Navigator screenOptions={{ headerShown: false }}>
      <ProfileStack.Screen
        name='ProfileScreen'
        component={ProfileScreen}
      />
    </ProfileStack.Navigator>
  );
};
