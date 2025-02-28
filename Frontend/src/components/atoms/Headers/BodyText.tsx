import React from "react";
import { StyleProp, Text, TextStyle } from "react-native";
import { StyleSheet } from "react-native";

interface BodyTextProps {
  text: string;
  style?: StyleProp<TextStyle>;
  textColor?: string;
}

export const BodyText: React.FC<BodyTextProps> = ({
  text,
  style,
  textColor,
}) => {
  return (
    <Text
      style={[
        styles.bodyText,
        style,
        { color: textColor ? textColor : "#000000" },
      ]}
    >
      {text}
    </Text>
  );
};

const styles = StyleSheet.create({
  bodyText: {
    fontSize: 16,
    lineHeight: 24,
  },
});
