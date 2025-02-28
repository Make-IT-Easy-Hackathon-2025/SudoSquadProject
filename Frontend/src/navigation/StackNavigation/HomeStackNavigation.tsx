import { createStackNavigator } from "@react-navigation/stack";
import { ScreenTypes } from "../ScreenTypes";
import { HomeScreen } from "../../screens";

const HomeStack = createStackNavigator<ScreenTypes>();

export const HomeStackNavigation = () => {
  return (
    <HomeStack.Navigator screenOptions={{ headerShown: false }}>
      <HomeStack.Screen
        name='HomeScreen'
        component={HomeScreen}
      />
    </HomeStack.Navigator>
  );
};
