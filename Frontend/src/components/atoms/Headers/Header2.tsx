import React from "react";
import { Text } from "react-native";
import { StyleSheet } from "react-native";

interface Header2Props {
  text: string;
}

export const Header2: React.FC<Header2Props> = ({ text }) => {
  return <Text style={styles.header2}>{text}</Text>;
};

const styles = StyleSheet.create({
  header2: {
    fontSize: 24,
    fontWeight: "bold",
  },
});
