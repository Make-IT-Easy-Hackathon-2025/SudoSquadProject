import { View, Text, StyleSheet, SafeAreaView, FlatList } from "react-native";
import React, { useEffect, useRef, useState } from "react";
import { Column, Header1, Header3 } from "../../components/atoms";
import { useHome } from "./useHome";
import { UserMemory } from "../../utils";
import Network, { VisNetworkRef } from "react-native-vis-network";

export const HomeScreen = () => {
  const { nodes, edges, user } = useHome();
  const [loading, setLoading] = useState<boolean>(false);
  const visNetworkRef = useRef<VisNetworkRef>(null);

  const handleClickEvent = (event: any) => {
    console.log(JSON.stringify(event, null, 2));
  };

  useEffect(() => {
    if (!loading || !visNetworkRef.current) {
      return;
    }

    const subscription = visNetworkRef.current.addEventListener(
      "click",
      (event: any) => handleClickEvent(event)
    );

    return subscription.remove;
  }, [loading]);

  return (
    <SafeAreaView style={{ flex: 1, marginTop: 26, width: "100%" }}>
      <Column style={styles.topContainer}>
        <Header1
          /*text={user?.username ? user?.username : ""}*/ text='Hello, User'
        />
        <Header3 text='See your achievements below🦾' />
      </Column>
      <Column style={styles.bottomContainer}>
        <Network
          data={{ nodes, edges }}
          onLoad={() => setLoading(true)}
          ref={visNetworkRef}
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
      </Column>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
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
