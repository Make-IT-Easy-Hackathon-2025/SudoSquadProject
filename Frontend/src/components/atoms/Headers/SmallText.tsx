import React from "react";
import { Text } from "react-native";
import { StyleSheet } from "react-native";

interface SmallTextProps {
  text: string;
  textColor?: string;
}

export const SmallText: React.FC<SmallTextProps> = ({ text, textColor }) => {
  return (
    <Text
      style={[styles.smallText, { color: textColor ? textColor : "#000000" }]}
    >
      {text}
    </Text>
  );
};

const styles = StyleSheet.create({
  smallText: {
    fontSize: 12,
    color: "gray",
  },
});
