import { StackNavigationProp } from "@react-navigation/stack";
import { UserMemory } from "../utils";

export type ScreenTypes = {
  LoginScreen: undefined;
  RegisterScreen: undefined;
  PlayScreen: undefined;
  HomeScreen: undefined;
  ProfileScreen: undefined;
  MiniGameScreen: { gameType: string };
  MemoryScreen: { memory: UserMemory };
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

export type MemoryScreenScreenNavigationProp = StackNavigationProp<
  ScreenTypes,
  "MemoryScreen"
>;

// export type MiniGameScreenNavigationProp = StackNavigationProp<
//   ScreenTypes,
//   "MiniGameScreen"
// >;
