import { StatusBar } from "expo-status-bar";
import { StyleSheet, Text, View } from "react-native";
import { NavigationContainer } from "@react-navigation/native";
import { RootNavigation } from "./src/navigation/RootNavigation";
import { AuthProvider } from "./src/contexts/AuthContext";
import { UserMemoryProvider } from "./src/contexts/UserMemoryContext";

export default function App() {
  return (
    <AuthProvider>
      <UserMemoryProvider>
        <NavigationContainer>
          <RootNavigation />
        </NavigationContainer>
      </UserMemoryProvider>
    </AuthProvider>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    alignItems: "center",
    justifyContent: "center",
  },
});
