import { TextInput, StyleSheet, View } from "react-native";
import React from "react";
import { Row } from "../Row/Row";
import { Column } from "../Column/Column";
import FontAwesome from "@react-native-vector-icons/fontawesome";
import { BodyText } from "../Headers/BodyText";
import { SmallText } from "../Headers/SmallText";

interface CustomInputProps {
  placeholder?: string;
  value?: string;
  customStyle?: object;
  onChangeText?: (text: string) => void;
  secureTextEntry?: boolean;
  keyboardType?: "numeric" | "email-address" | "phone-pad" | "default";
  inputError?: string;
  icon?: any;
  iconColor?: string;
}

export const CustomInput: React.FC<CustomInputProps> = ({
  placeholder = "",
  value,
  customStyle,
  onChangeText,
  secureTextEntry,
  keyboardType, // 'numeric' | 'email-address' | 'phone-pad' | 'default'
  inputError = null,
  icon,
}) => {
  return (
    <Column>
      <Row
        style={[
          styles.container,
          { borderColor: inputError ? "red" : "black" },
        ]}
      >
        {icon && (
          <FontAwesome
            name={icon}
            size={20}
            color='#000000'
          />
        )}
        <TextInput
          style={[customStyle, styles.input]}
          placeholder={placeholder ? placeholder : ""}
          value={value}
          onChangeText={onChangeText}
          secureTextEntry={secureTextEntry ? secureTextEntry : false}
          keyboardType={keyboardType ? keyboardType : "default"}
        />
      </Row>
      {inputError && (
        <View style={styles.inputError}>
          <SmallText
            text={inputError}
            textColor='#ff0000'
          />
        </View>
      )}
    </Column>
  );
};

const styles = StyleSheet.create({
  container: {
    borderWidth: 1,
    justifyContent: "flex-start",
    alignItems: "center",
    minWidth: 150,
    paddingLeft: 8,
    borderRadius: 8,
  },
  input: {
    flex: 1,
    // height: 40,
    // margin: 12,
    // borderWidth: 1,
  },
  inputError: {
    marginBottom: 5,
    paddingLeft: 8,
  },
});
