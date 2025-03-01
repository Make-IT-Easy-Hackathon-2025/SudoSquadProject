import { View, Text, StyleSheet } from "react-native";
import React from "react";
import { Column } from "../../components/atoms";
import { Header1 } from "../../components/atoms";

export const LoginScreen = () => {
  return (
    <View style={styles.container}>
      <Column>
        <Header1 text='Login' />
      </Column>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    alignItems: "center",
    justifyContent: "center",
  },
});
