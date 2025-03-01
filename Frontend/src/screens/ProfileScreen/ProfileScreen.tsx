import { View, Text, StyleSheet } from "react-native";
import React, { useEffect } from "react";
import Ionicons from "react-native-vector-icons/Ionicons";
import { useProfile } from "./useProfile";

export const ProfileScreen = () => {
  const { statistics, setStatistics, getUserStaistics, username } =
    useProfile();

  useEffect(() => {
    getUserStaistics();
  }, []);

  const getLastWeekPointsIndicator = () => {
    if (!statistics?.lastWeekPoints) return null;

    if (statistics.lastWeekPoints > 0) {
      return <Ionicons name="arrow-up-circle" size={20} color="green" />;
    } else if (statistics.lastWeekPoints < 0) {
      return <Ionicons name="arrow-down-circle" size={20} color="red" />;
    } else {
      return <Ionicons name="remove-circle" size={20} color="gray" />;
    }
  };

  return (
    <View style={styles.container}>
      <Ionicons
        name="person-circle-outline"
        size={100}
        color="#777"
        style={styles.profileIcon}
      />
      <View style={styles.profileSection}>
        <Text style={styles.name}>{username || "User Name"}</Text>
        <Text style={styles.score}>Score: {statistics?.score}</Text>
        <Text style={styles.streak}>
          🔥 Streak: {statistics?.streakNumber} days
        </Text>
      </View>
      
      <View style={styles.cardContainer}>
        <View style={styles.card}>
          <Text style={styles.cardTitle}>Last Week's Points</Text>
          <View style={styles.row}>
            <Text style={styles.cardValue}>{statistics?.lastWeekPoints}</Text>
            {getLastWeekPointsIndicator()}
          </View>
        </View>

        <View style={styles.card}>
          <Text style={styles.cardTitle}>Last Week's Activities</Text>
          <Text style={styles.cardValue}>
            {statistics?.lastWeekActivityCount} 🚀
          </Text>
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f4f4f4",
    alignItems: "center",
    padding: 20,
  },
  profileIcon: {
    marginTop: 40,
    marginBottom: 10,
  },
  profileSection: {
    alignItems: "center",
    marginBottom: 30,
  },
  name: {
    fontSize: 26,
    fontWeight: "bold",
    marginBottom: 10,
  },
  score: {
    fontSize: 32,
    fontWeight: "bold",
    color: "#ff8c00",
    marginBottom: 10,
  },
  streak: {
    fontSize: 20,
    fontWeight: "600",
    color: "#ff4500",
    marginBottom: 20,
  },
  cardContainer: {
    width: "100%",
    alignItems: "center",
  },
  card: {
    width: "90%",
    backgroundColor: "#fff",
    padding: 15,
    borderRadius: 12,
    shadowColor: "#000",
    shadowOpacity: 0.1,
    shadowRadius: 5,
    elevation: 4,
    alignItems: "center",
    marginBottom: 10,
  },
  cardTitle: {
    fontSize: 16,
    color: "#777",
    marginBottom: 5,
  },
  cardValue: {
    fontSize: 18,
    fontWeight: "bold",
  },
  row: {
    flexDirection: "row",
    alignItems: "center",
    gap: 6,
  },
});
