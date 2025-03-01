import {
  View,
  Text,
  StyleProp,
  ViewStyle,
  Pressable,
  StyleSheet,
  TextStyle,
} from "react-native";
import React from "react";
import { Row } from "../Row/Row";
// import FontAwesome from "@react-native-vector-icons/fontawesome";

interface CustomButtonProps {
  text?: string;
  onPress?: () => void;
  buttonStyle?: StyleProp<ViewStyle>;
  textStyle?: StyleProp<TextStyle>;
  disabled?: boolean;
  icon?: any;
  backGroundColor?: string;
}

export const CustomButton: React.FC<CustomButtonProps> = ({
  text,
  onPress,
  buttonStyle,
  textStyle,
  disabled,
  backGroundColor,
  icon,
}) => {
  return (
    <Pressable
      onPress={onPress}
      style={[
        styles.button,
        buttonStyle,
        { backgroundColor: disabled ? "#d3d3d3" : backGroundColor },
      ]}
      disabled={disabled}
    >
      <Row>
        {/* {icon && (
          <FontAwesome
            name={icon}
            size={20}
            color='white'
            style={styles.icon}
          />
        )} */}
        <Text>{text}</Text>
      </Row>
    </Pressable>
  );
};

const styles = StyleSheet.create({
  button: {
    alignItems: "center",
    justifyContent: "center",
    borderRadius: 8,
    paddingVertical: 12,
    paddingHorizontal: 20,
  },
  buttonContent: {
    alignItems: "center",
    justifyContent: "center",
  },
  icon: {
    marginRight: 8,
  },
});
