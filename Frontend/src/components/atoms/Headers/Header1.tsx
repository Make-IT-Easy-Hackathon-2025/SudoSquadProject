import React from "react";
import { Text } from "react-native";
import { StyleSheet } from "react-native";

interface Header1Props {
  text?: string;
}

export const Header1: React.FC<Header1Props> = ({ text }) => {
  return <Text style={styles.header1}>{text}</Text>;
};

const styles = StyleSheet.create({
  header1: {
    fontSize: 32,
    fontWeight: "bold",
  },
});
