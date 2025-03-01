import { View, Text, FlatList } from "react-native";
import React from "react";
import { Quiz, QuizOption } from "../../utils";
import { BodyText, Column, SmallText } from "../atoms";

export const QuestionWidgets: React.FC<{ quiz: Quiz[] }> = ({ quiz }) => {
  const renderQuestion = () => {
    return (
      <FlatList
        data={quiz}
        renderItem={({ item }) => (
          <Column>
            <BodyText text={item.title} />
            {/* <Column>
              <RenderAnswers answers={item.questions} />
            </Column> */}
          </Column>
        )}
        keyExtractor={(item, index) =>
          item.id ? item.id.toString() : index.toString()
        }
      />
    );
  };

  // const RenderAnswers = ({ answers }: QuizOption[]) => {
  //   return (
  //     <View>
  //       <SmallText text={answers[0].title} />
  //       <SmallText text={answers[1].title} />
  //       <SmallText text={answers[2].title} />
  //       <SmallText text={answers[3].title} />
  //     </View>
  //   );
  // };

  return <View></View>;
};
