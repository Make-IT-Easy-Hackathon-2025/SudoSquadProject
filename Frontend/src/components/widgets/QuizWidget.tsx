import { StyleSheet, View } from "react-native";
import React, { useState } from "react";
import { Column, CustomButton, CustomInput, Header1, Header3 } from "../atoms";
import { Ionicons } from "@expo/vector-icons";
import Feather from "@expo/vector-icons/Feather";

export const QuizWidget: React.FC = () => {
  const [inputText, setInputText] = useState("");
  // const { topic, setTopic }

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
          value={inputText}
          onChangeText={setInputText}
          customStyle={styles.inputStyle}
          icon='search'
        />
      </View>
    </Column>
  );
};

// This component might not exist in your atoms folder, so I'm defining it here
// const CustomText = ({ style, children }) => {
//   return <Text style={style}>{children}</Text>;
// };

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
    // borderWidth: 1,
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
    // width: 250,
    backgroundColor: "#f8f8f8",
    borderRadius: 12,
    fontSize: 16,
    // borderWidth: 1,
    borderColor: "#e0e0e0",
    shadowColor: "#000",
    // shadowOffset: { width: 0, height: 1 },
    // shadowOpacity: 0.05,
    // shadowRadius: 2,
    // elevation: 1,
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
