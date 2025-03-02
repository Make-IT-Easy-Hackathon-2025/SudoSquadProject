import React from "react";
import {
  SafeAreaView,
  ScrollView,
  View,
  Text,
  Image,
  StyleSheet,
} from "react-native";
import { useMemory } from "./useMemory";
import { UserMemory } from "../../utils";
import { RouteProp } from "@react-navigation/native";

export const MemoryScreen: React.FC = ({ route }: any) => {
  const memory = route.params.memory;

  return (
    <SafeAreaView style={styles.container}>
      <ScrollView contentContainerStyle={styles.scrollContainer}>
        <Image
          source={{ uri: memory?.imageUrl }}
          style={styles.image}
          resizeMode="cover"
        />

        <Text style={styles.title}>{memory?.title}</Text>

        <Text style={styles.date}>
          {memory?.date ? new Date(memory.date).toLocaleDateString() : ""}
        </Text>

        <Text style={styles.description}>{memory?.description}</Text>
      </ScrollView>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f0f4f7",
  },
  scrollContainer: {
    padding: 20,
    alignItems: "center",
    paddingBottom: 50,
  },
  image: {
    width: "100%",
    height: 250,
    borderRadius: 12,
    marginBottom: 30,
    marginTop: 50,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    elevation: 5,
  },
  title: {
    fontSize: 32,
    fontWeight: "700",
    color: "#333",
    textAlign: "center",
    marginTop: 30,
    marginBottom: 15,
  },
  date: {
    fontSize: 16,
    color: "#666",
    fontStyle: "italic",
    marginBottom: 20,
    textAlign: "center",
  },
  description: {
    fontSize: 18,
    color: "#444",
    lineHeight: 26,
    textAlign: "justify",
    marginTop: 10,
    marginBottom: 30,
  },
});
