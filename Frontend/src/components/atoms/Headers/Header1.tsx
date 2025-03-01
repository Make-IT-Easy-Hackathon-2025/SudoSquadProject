import React from "react";
import { StyleProp, Text, TextStyle } from "react-native";
import { StyleSheet } from "react-native";

interface Header1Props {
  text?: string;
  style?: StyleProp<TextStyle>;
}

export const Header1: React.FC<Header1Props> = ({ text, style }) => {
  return <Text style={[styles.header1, style]}>{text}</Text>;
};

const styles = StyleSheet.create({
  header1: {
    fontSize: 32,
    fontWeight: "bold",
  },
});
