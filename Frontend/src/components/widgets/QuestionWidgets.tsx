import { View, Text, FlatList, StyleSheet } from "react-native";
import React from "react";
import { Quiz, QuizOption, QuizQuestion } from "../../utils";
import {
  BodyText,
  Column,
  CustomButton,
  Header1,
  Header2,
  Header3,
  Row,
  SmallText,
} from "../atoms";
import Icon from "@react-native-vector-icons/fontawesome";

export const QuestionWidgets: React.FC<{
  quiz?: Quiz;
  selectedAnswers: { [key: number]: number | null };
  selectAnswer: (questionId: number, optionId: number) => void;
}> = ({ quiz, selectedAnswers, selectAnswer }) => {
  console.log("quiz", quiz);
  if (
    !quiz ||
    !quiz.questions ||
    !Array.isArray(quiz.questions) ||
    quiz.questions.length === 0
  ) {
    return <BodyText text='No quiz questions available' />;
  }
  console.log("answers", quiz.questions[0].options);

  const renderAnswers = (questionId: number, answers: QuizOption[]) => {
    if (!answers || !Array.isArray(answers) || answers.length === 0) {
      return <BodyText text='No answer options available' />;
    }

    return (
      <Column>
        {/* <CustomButton
          text={answers[0].optionValue}
          buttonStyle={styles.buttonStyle}
        />
        <CustomButton
          text={answers[1].optionValue}
          buttonStyle={styles.buttonStyle}
        />
        <CustomButton
          text={answers[2].optionValue}
          buttonStyle={styles.buttonStyle}
        />
        <CustomButton
          text={answers[3].optionValue}
          buttonStyle={styles.buttonStyle}
        /> */}

        {answers.map((option) => {
          if (!option || option.id === undefined) return null;

          const isSelected = selectedAnswers[questionId] === option.id;
          return (
            <CustomButton
              key={option.id}
              text={option.optionValue}
              buttonStyle={[
                styles.buttonStyle,
                isSelected && styles.selectedButton,
              ]}
              onPress={() => selectAnswer(questionId, option.id)}
            />
          );
        })}
      </Column>
    );
  };

  return (
    <FlatList
      data={quiz.questions}
      renderItem={({ item }) => {
        if (!item) return null;
        return (
          <Column>
            <Row style={{ alignItems: "flex-start" }}>
              <Header2 text={item.id.toString() + "."} />
              <BodyText text={item.value} />
            </Row>

            <Column style={{ paddingHorizontal: 26 }}>
              {renderAnswers(item.id, item.options)}
            </Column>
          </Column>
        );
      }}
      keyExtractor={(item, index) =>
        item.id ? item.id.toString() : index.toString()
      }
      style={{ height: 550 }}
    />
  );
};

const styles = StyleSheet.create({
  container: {
    width: "100%",
    height: "100%",
    // alignItems: "center",
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

  buttonStyle: {
    width: "70%",
    paddingVertical: 15,
    borderRadius: 18,
    backgroundColor: "#6C63FF",
    alignItems: "center",
    justifyContent: "center",
    borderWidth: 2,
    borderColor: "#6C63FF",
  },
  selectedButton: {
    backgroundColor: "#4CAF50", // Kijelölt válasz más színű
    borderColor: "#388E3C",
  },
});
