import { View, Text, StyleSheet } from "react-native";
import React, { useState } from "react";
import { Column, CustomButton, CustomInput } from "../../components/atoms";
import { Header1 } from "../../components/atoms";
import { useLogin } from "./useLogin";
import { useAuth } from "../../contexts/AuthContext";

export const LoginScreen: React.FC = () => {
  const { email, setEmail, password, setPassword, userLogin, error } =
    useLogin();

  return (
    <View style={styles.container}>
      <Column>
        <Header1 text='Login' />
        <CustomInput
          placeholder='Email'
          icon='mail'
          onChangeText={(prev) => setEmail(prev)}
          value={email}
          // inputError={error.email}
        />
        <CustomInput
          placeholder='Password'
          icon='lock'
          secureTextEntry
          onChangeText={(prev) => setPassword(prev)}
          value={password}
          // inputError={error.password}
        />
        <CustomButton
          text='Login'
          buttonStyle={styles.buttonStyle}
          onPress={userLogin}
        />
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
  buttonStyle: {
    borderWidth: 1,
    borderColor: "black",
    borderRadius: 8,
  },
});
