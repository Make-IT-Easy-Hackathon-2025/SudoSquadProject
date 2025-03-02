import { View, Text, StyleSheet, SafeAreaView } from "react-native";
import React, { useEffect, useRef, useState } from "react";
import { Column, Header1, Header3 } from "../../components/atoms";
import { useHome } from "./useHome";
import Network, { VisNetworkRef } from "react-native-vis-network";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { ScreenTypes } from "../../navigation/ScreenTypes";

export const HomeScreen = () => {
  const { nodes, edges, getMemories } = useHome();
  const [loading, setLoading] = useState(false);
  const visNetworkRef = useRef<VisNetworkRef>(null);

  const navigation = useNavigation<NativeStackNavigationProp<ScreenTypes>>();

  const handleClickEvent = (event: any) => {
    console.log("Node clicked:", JSON.stringify(event, null, 2));

    if (event.nodes && event.nodes.length > 0) {
      const nodeId = event.nodes[0];
      const nodeData = nodes?.find((node: any) => node.id === nodeId);
      if (nodeData) {
        navigation.push("MemoryScreen", { memory: nodeData });
      }
    }
  };

  useEffect(() => {
    getMemories();
  }, []);

  useEffect(() => {
    if (!loading || !visNetworkRef.current) {
      return;
    }

    const subscription = visNetworkRef.current.addEventListener(
      "click",
      handleClickEvent
    );

    return subscription.remove;
  }, [loading, nodes]);

  console.log("asdasdasd", {
    nodes,
    edges,
  });

  return (
    <SafeAreaView style={styles.container}>
      <Column style={styles.topContainer}>
        <Header1 text="Hello, User" />
        <Header3 text="See your achievements below 🦾" />
      </Column>
      <Column style={styles.bottomContainer}>
        <Network
          data={{ nodes, edges }}
          onLoad={() => setLoading(true)}
          ref={visNetworkRef}
          options={{
            nodes: {
              shape: "dot",
              size: 25,
              color: {
                border: "#007AFF",
                background: "#EAF2FF",
                highlight: {
                  border: "#004F9E",
                  background: "#A3C4FF",
                },
              },
              font: {
                size: 14,
                color: "#333",
              },
            },
            edges: {
              color: {
                color: "#C0C0C0",
                highlight: "#007AFF",
              },
              width: 2,
            },
            interaction: {
              hover: true,
              tooltipDelay: 200,
            },
            physics: {
              enabled: true,
              stabilization: {
                iterations: 100,
              },
            },
          }}
        />
      </Column>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#F8F9FB",
  },
  topContainer: {
    alignItems: "flex-start",
    height: "10%",
    justifyContent: "center",
    paddingHorizontal: 18,
    backgroundColor: "#ffffff",
  },
  bottomContainer: {
    flex: 1,
    width: "100%",
  },
});
