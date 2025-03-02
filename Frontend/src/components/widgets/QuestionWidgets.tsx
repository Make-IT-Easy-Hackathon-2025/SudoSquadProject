import { View, Text, FlatList, StyleSheet } from "react-native";
import React, { useEffect, useMemo } from "react";
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

export const QuestionWidgets: React.FC<{
  quiz?: Quiz;
  selectedAnswers: { [key: number]: number | null };
  selectAnswer: (questionId: number, optionId: number) => void;
}> = ({ quiz, selectedAnswers, selectAnswer }) => {
  const [answers, setAnswers] = React.useState<{ [key: number]: number }>({});

  const renderAnswers = useMemo(
    () => (questionId: number, answers?: QuizOption[]) => {
      return (
        <Column>
          {answers?.map((option) => {
            const isSelected = selectedAnswers[questionId] === option.id;
            console.log("isSelected", isSelected, questionId);

            return (
              <CustomButton
                key={option.id}
                text={option.optionValue}
                buttonStyle={[
                  styles.buttonStyle,
                  isSelected && styles.selectedButton,
                ]}
                onPress={() => {
                  selectAnswer(questionId, option.id);
                  console.log("kerdes es valasz", questionId, option.id);
                  // setAnswers({ ...answers, [questionId]: option.id });
                  setAnswers((prev) => ({ ...prev, [questionId]: option.id }));
                }}
              />
            );
          })}
        </Column>
      );
    },
    [selectedAnswers, selectAnswer]
  );

  return (
    <FlatList
      data={quiz?.questions || []}
      renderItem={({ item }) => {
        return (
          <Column>
            <Row style={{ alignItems: "flex-start" }}>
              <Header2 text={item.id.toString() + "."} />
              <BodyText text={item.value} />
            </Row>

            <Column style={{ paddingHorizontal: 26 }}>
              {renderAnswers(item.id, item.options || [])}
            </Column>
          </Column>
        );
      }}
      keyExtractor={(item) => item.id.toString()}
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
