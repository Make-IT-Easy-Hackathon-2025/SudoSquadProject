import { createStackNavigator } from "@react-navigation/stack";
import { ScreenTypes } from "../ScreenTypes";
import { LoginScreen, RegisterScreen } from "../../screens";

const AuthStack = createStackNavigator<ScreenTypes>();

export const AuthStackNavigation = () => {
  return (
    <AuthStack.Navigator screenOptions={{ headerShown: false }}>
      <AuthStack.Screen
        name='LoginScreen'
        component={LoginScreen}
      />
      <AuthStack.Screen
        name='RegisterScreen'
        component={RegisterScreen}
      />
    </AuthStack.Navigator>
  );
};
