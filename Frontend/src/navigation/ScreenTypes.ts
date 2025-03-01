import { StackNavigationProp } from '@react-navigation/stack';

export type ScreenTypes = {
  LoginScreen: undefined;
  RegisterScreen: undefined;
  PlayScreen: undefined;
  HomeScreen: undefined;
  ProfileScreen: undefined;
  MiniGameScreen: {gameType: string};
};

export type TabTypes = {
    Home: undefined;
    Profile: undefined;
    Play: undefined;
};

export type HomeScreenNavigationProp = StackNavigationProp<
  ScreenTypes,
  "HomeScreen"
>;

export type PlayScreenNavigationProp = StackNavigationProp<
  ScreenTypes,
  "PlayScreen"
>;

export type ProfileScreenNavigationProp = StackNavigationProp<
  ScreenTypes,
  "ProfileScreen"
>;

// export type MiniGameScreenNavigationProp = StackNavigationProp<
//   ScreenTypes,
//   "MiniGameScreen"
// >;

