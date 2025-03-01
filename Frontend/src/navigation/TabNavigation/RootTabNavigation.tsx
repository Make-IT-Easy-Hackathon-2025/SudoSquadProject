import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import React from "react";
import {
  HomeStackNavigation,
  PlayStackNavigation,
  ProfileStackNavigation,
} from "../StackNavigation";
import { TabTypes } from "../ScreenTypes";
// import FontAwesome from "react-native-vector-icons/FontAwesome";
import FontAwesome from "react-native-vector-icons/FontAwesome";

const Tab = createBottomTabNavigator();

export const RootTabNavigation = () => {
  return (
    <Tab.Navigator screenOptions={{ headerShown: false }}>
      <Tab.Screen
        name='Home'
        component={HomeStackNavigation}
        options={{
          tabBarIcon: ({ focused }) => {
            return (
              <FontAwesome
                name='key'
                size={20}
                color={focused ? "blue" : "black"}
              />
            );
          },
          tabBarLabelStyle: {
            fontSize: 12,
            fontWeight: "bold",
            paddingBottom: 5,
          },
          tabBarActiveTintColor: "black",
          tabBarInactiveTintColor: "gray",
        }}
      />
      <Tab.Screen
        name='Play'
        component={PlayStackNavigation}
        options={{
          tabBarLabelStyle: {
            fontSize: 12,
            fontWeight: "bold",
            paddingBottom: 5,
          },
          tabBarActiveTintColor: "black",
          tabBarInactiveTintColor: "gray",
        }}
      />
      <Tab.Screen
        name='Profile'
        component={ProfileStackNavigation}
        options={{
          tabBarLabelStyle: {
            fontSize: 12,
            fontWeight: "bold",
            paddingBottom: 5,
          },
          tabBarActiveTintColor: "black",
          tabBarInactiveTintColor: "gray",
        }}
      />
    </Tab.Navigator>
  );
};
