import { View, Text } from "react-native";
import React from "react";
import { useState } from "react";
import Network from "react-native-vis-network";

export const HomeScreen = () => {
  const nodes = [];
  const edges = [];

  // Generáljunk 20 csomópontot
  for (let i = 1; i <= 20; i++) {
    nodes.push({ id: i, label: `Node ${i}` });

    // Ha nem az utolsó csomópont, adjuk hozzá az összeköttetést az előzőhöz
    if (i < 20) {
      edges.push({ from: i, to: i + 1 });
    }
  }

  return (
    <View style={{ flex: 1 }}>
      <Text>HomeScreen</Text>
      <Network
        data={{ nodes, edges }}
        options={{
          nodes: {
            shape: "dot",
            size: 20,
            color: {
              border: "black",
              background: "white",
            },
          },
          edges: {
            color: "gray",
          },
        }}
      />
    </View>
  );
};
