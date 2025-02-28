import { View, ViewStyle, StyleSheet } from "react-native";
import React from "react";
import { StyleProp } from "react-native";

interface RowProps {
  children: React.ReactNode;
  style?: StyleProp<ViewStyle>;
  gap?: number;
}

export const Row: React.FC<RowProps> = ({ children, style, gap }) => {
  return (
    <View style={[style, styles.container, { gap: gap ? gap : 8 }]}>
      {children}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flexDirection: "row",
  },
});
