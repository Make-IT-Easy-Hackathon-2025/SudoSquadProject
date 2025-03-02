import { createStackNavigator } from "@react-navigation/stack";
import { ScreenTypes } from "../ScreenTypes";
import { HomeScreen } from "../../screens";
import { MemoryScreen } from "../../screens/MemoryScreen";

const HomeStack = createStackNavigator<ScreenTypes>();

export const HomeStackNavigation = () => {
  return (
    <HomeStack.Navigator screenOptions={{ headerShown: false }}>
      <HomeStack.Screen name="HomeScreen" component={HomeScreen} />
      <HomeStack.Screen name="MemoryScreen" component={MemoryScreen} />
    </HomeStack.Navigator>
  );
};
