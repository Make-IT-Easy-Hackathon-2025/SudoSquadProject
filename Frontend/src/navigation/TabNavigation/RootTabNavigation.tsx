import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import React from "react";
import {
  HomeStackNavigation,
  PlayStackNavigation,
  ProfileStackNavigation,
} from "../StackNavigation";
import FontAwesome from "react-native-vector-icons/FontAwesome";
import Feather from "react-native-vector-icons/Feather";
const Tab = createBottomTabNavigator();

export const RootTabNavigation = () => {
  return (
    <Tab.Navigator screenOptions={{ headerShown: false }}>
      <Tab.Screen
        name='Home'
        component={HomeStackNavigation}
        options={{
          // tabBarIcon: ({ focused }) => {
          //   return (
          //     <FontAwesome
          //       name='home'
          //       size={20}
          //       color={focused ? "black" : "grey"}
          //     />
          //   );
          // },
          tabBarIconStyle: { display: "none" },
          tabBarLabelStyle: {
            fontSize: 18,
            marginTop: 6,
            fontWeight: "bold",
            textAlign: "center",
          },
          tabBarActiveTintColor: "black",
          tabBarInactiveTintColor: "gray",
          tabBarStyle: {
            justifyContent: "center",
            alignItems: "center",
          },
        }}
      />
      <Tab.Screen
        name='Play'
        component={PlayStackNavigation}
        options={{
          tabBarLabelStyle: {
            fontSize: 22,
            fontWeight: "bold",
            borderWidth: 1,
            borderColor: "black",
            width: 160,
            // height: 100,
            borderRadius: 50,
          },
          tabBarActiveTintColor: "black",
          tabBarInactiveTintColor: "gray",
          tabBarIconStyle: { display: "none" },
        }}
      />
      <Tab.Screen
        name='Profile'
        component={ProfileStackNavigation}
        options={{
          tabBarLabelStyle: {
            fontSize: 18,
            fontWeight: "bold",
            marginTop: 6,
          },
          tabBarActiveTintColor: "black",
          tabBarInactiveTintColor: "gray",
          tabBarIconStyle: { display: "none" },
        }}
      />
    </Tab.Navigator>
  );
};
