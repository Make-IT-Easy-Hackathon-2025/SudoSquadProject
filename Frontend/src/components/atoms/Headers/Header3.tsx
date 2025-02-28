import React from "react";
import { Text } from "react-native";
import { StyleSheet } from "react-native";

interface Header3Props {
  text: string;
}

export const Header3: React.FC<Header3Props> = ({ text }) => {
  return <Text style={styles.header3}>{text}</Text>;
};

const styles = StyleSheet.create({
  header3: {
    fontSize: 20,
    fontWeight: "bold",
  },
});
