import { View, Text, StyleSheet, SafeAreaView } from "react-native";
import React from "react";
import { CustomButton } from "../../components/atoms";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { ScreenTypes } from "../../navigation/ScreenTypes";
import { useMiniGame } from "./useMiniGame";
import { RoadMap, QuizWidget } from "../../components/widgets";

export const MiniGameScreen: React.FC = ({ route }: any) => {
  const navigation = useNavigation<NativeStackNavigationProp<ScreenTypes>>();
  const gameType = route.params.gameType;
  const miniGameLogic = useMiniGame();

  const backButtonEffect = () => {
    navigation.pop();
  };

  const RenderItem = () => {
    if (gameType === "roadmap") {
      return <RoadMap />;
    } else if (gameType === "quiz") {
      return <QuizWidget miniGameLogic={miniGameLogic} />;
    } else {
      return <Text style={styles.customGameText}>Custom Game</Text>;
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.topContainer}>
        <CustomButton
          text='Back'
          onPress={backButtonEffect}
          buttonStyle={styles.backButtonStyle}
          textStyle={{ fontSize: 22, fontWeight: "bold" }}
        />
      </View>
      <View style={styles.bottomContainer}>
        <RenderItem />
      </View>
      <View style={styles.buttonContainer}>
        <CustomButton
          text={"Generate Quiz"}
          buttonStyle={styles.buttonStyle}
          textStyle={styles.buttonText}
          onPress={miniGameLogic.generate}
        />
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "light-green",
    paddingTop: 32,
    paddingBottom: 22,
  },
  topContainer: {
    flex: 0.15,
    justifyContent: "center",
    alignItems: "flex-start",
    paddingHorizontal: 20,
  },
  bottomContainer: {
    width: "100%",
    flex: 0.85,
    justifyContent: "flex-start",
    alignItems: "center",
    marginBottom: 10,
  },
  backButtonStyle: {
    borderRadius: 15,
    backgroundColor: "#3498db",
    paddingVertical: 6,
    paddingHorizontal: 25,
    width: 150,
    alignItems: "center",
    justifyContent: "center",
    borderWidth: 1,
  },
  customGameText: {
    fontSize: 24,
    fontWeight: "bold",
    color: "#333",
  },
  buttonContainer: {
    width: "100%",
    alignItems: "center",
  },
  buttonStyle: {
    width: "70%",
    paddingVertical: 15,
    borderRadius: 30,
    backgroundColor: "#6C63FF",
    alignItems: "center",
    justifyContent: "center",
    flexDirection: "row",
    elevation: 3,
    shadowColor: "#6C63FF",
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.3,
    shadowRadius: 6,
  },
  buttonText: {
    color: "#000000",
    fontSize: 16,
    fontWeight: "600",
    marginLeft: 8,
  },
});
