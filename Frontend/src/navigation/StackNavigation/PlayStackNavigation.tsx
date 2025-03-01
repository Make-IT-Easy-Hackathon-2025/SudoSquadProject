import { createStackNavigator } from "@react-navigation/stack";
import { ScreenTypes } from "../ScreenTypes";
import { PlayScreen } from "../../screens";
import { MiniGameScreen } from "../../screens";

const PlayStack = createStackNavigator<ScreenTypes>();

export const PlayStackNavigation = () => {
  return (
    <PlayStack.Navigator screenOptions={{ headerShown: false }}>
      <PlayStack.Screen
        name='PlayScreen'
        component={PlayScreen}
      />
      <PlayStack.Screen
        name='MiniGameScreen'
        component={MiniGameScreen}
      />
    </PlayStack.Navigator>
  );
};
