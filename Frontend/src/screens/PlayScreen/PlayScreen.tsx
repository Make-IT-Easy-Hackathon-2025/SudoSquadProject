import { View, Text, SafeAreaView, StyleSheet } from "react-native";
import React from "react";
import {
  BodyText,
  Column,
  CustomButton,
  Header3,
} from "../../components/atoms";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { ScreenTypes } from "../../navigation/ScreenTypes";
import { usePlay } from "./usePlay";

export const PlayScreen: React.FC = () => {
  const navigation = useNavigation<NativeStackNavigationProp<ScreenTypes>>();

  const buttonEffect = (type: string) => {
    navigation.push("MiniGameScreen", { gameType: type });
  };

  return (
    <SafeAreaView style={styles.container}>
      <Column gap={45}>
        <CustomButton
          text='Road Map'
          buttonStyle={styles.buttonStyle}
          onPress={() => buttonEffect("roadmap")}
        />
        <CustomButton
          text='Quiz'
          buttonStyle={styles.buttonStyle}
          onPress={() => buttonEffect("quiz")}
        />
        <CustomButton
          text='Reaction Game'
          buttonStyle={styles.buttonStyle}
          onPress={() => buttonEffect("reactionGame")}
        />
      </Column>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    gap: 30,
  },
  buttonStyle: {
    borderWidth: 1,
    borderColor: "black",
    borderRadius: 8,
    width: 300,
  },
  custom: {
    width: 300,
  },
});
