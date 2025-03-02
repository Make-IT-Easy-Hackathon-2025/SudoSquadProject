import { ActivityIndicator, StyleSheet, View } from "react-native";
import React, { useEffect, useMemo, useState } from "react";
import {
  BodyText,
  Column,
  CustomButton,
  CustomInput,
  Header1,
  Header3,
} from "../atoms";
import { Ionicons } from "@expo/vector-icons";
import { useMiniGame } from "../../screens/MiniGameScreen/useMiniGame";
import { QuestionWidgets } from "./QuestionWidgets";
// import Feather from "@expo/vector-icons/Feather";

export const QuizWidget: React.FC<{
  miniGameLogic: ReturnType<typeof useMiniGame>;
}> = ({ miniGameLogic }) => {
  const renderQuiz = useMemo(() => {
    if (miniGameLogic.loading) {
      return (
        <Column>
          <ActivityIndicator
            size='large'
            color='#000fff'
          />
          <BodyText text='Loading...' />
        </Column>
      );
    }
    if (!miniGameLogic.quiz) {
      return (
        <Column
          style={styles.inputContainer}
          gap={24}
        >
          <View style={styles.headerContainer}>
            <Ionicons
              name='school'
              size={24}
              color='#6C63FF'
            />
            <View style={styles.headerTextContainer}>
              <Column gap={4}>
                <Header1
                  style={styles.headerTitle}
                  text='Quiz Generator"'
                />
                <Header3
                  style={styles.headerSubtitle}
                  text='Create custom quizzes instantly'
                />
              </Column>
            </View>
          </View>
          <View style={{ width: "92%" }}>
            <CustomInput
              placeholder='What would you like to learn about?'
              value={miniGameLogic.topic}
              onChangeText={miniGameLogic.setTopic}
              customStyle={styles.inputStyle}
              icon='search'
            />
            {/* <Checkbox /> */}
          </View>
        </Column>
      );
    }

    return (
      <Column
        style={styles.inputContainer}
        gap={24}
      >
        <View style={styles.headerContainer}>
          {/* <Ionicons
            name='school'
            size={24}
            color='#6C63FF'
          /> */}
          <View style={styles.headerTextContainer}>
            <Column gap={4}>
              <Header1
                style={styles.headerTitle}
                text={"Your quiz topic is: " + miniGameLogic.topic}
              />

              <QuestionWidgets
                quiz={miniGameLogic.quiz}
                selectAnswer={miniGameLogic.selectAnswer}
                selectedAnswers={miniGameLogic.selectedAnswers}
              />
            </Column>
          </View>
        </View>
      </Column>
    );
  }, [miniGameLogic.quiz]);

  return <View style={{ width: "100%" }}>{renderQuiz}</View>;
};

const styles = StyleSheet.create({
  inputContainer: {
    width: "100%",
    height: "100%",
    alignItems: "center",
    paddingVertical: 30,
    paddingHorizontal: 24,
    backgroundColor: "#ffffff",
    borderRadius: 16,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.1,
    shadowRadius: 8,
    elevation: 5,
  },
  headerContainer: {
    flexDirection: "row",
    alignItems: "center",
    width: "100%",
    marginBottom: 10,
  },
  headerTextContainer: {
    marginLeft: 12,
  },
  headerTitle: {
    fontSize: 22,
    fontWeight: "700",
    color: "#333",
  },
  headerSubtitle: {
    fontSize: 14,
    color: "#666",
  },
  inputStyle: {
    height: 56,
    backgroundColor: "#f8f8f8",
    borderRadius: 12,
    fontSize: 16,
    borderColor: "#e0e0e0",
    shadowColor: "#000",
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

  quizContainer: {
    width: "100%",
    height: "100%",
    // paddingVertical: 30,
    // paddingHorizontal: 24,
    backgroundColor: "#ffffff",
    borderRadius: 16,
    shadowColor: "#000",
    // shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.1,
    shadowRadius: 8,
    elevation: 2,
  },
});
