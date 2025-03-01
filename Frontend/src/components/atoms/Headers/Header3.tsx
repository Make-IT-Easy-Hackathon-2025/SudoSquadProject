import React from "react";
import { StyleProp, Text, TextStyle } from "react-native";
import { StyleSheet } from "react-native";

interface Header3Props {
  text: string;
  style?: StyleProp<TextStyle>;
}

export const Header3: React.FC<Header3Props> = ({ text, style }) => {
  return <Text style={[styles.header3, style]}>{text}</Text>;
};

const styles = StyleSheet.create({
  header3: {
    fontSize: 20,
    fontWeight: "bold",
  },
});
