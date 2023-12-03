import { StatusBar } from "expo-status-bar";
import React, { useState } from "react";
import { Button, StyleSheet, Text, View } from "react-native";
import { Input } from "react-native-elements";
import { useAppSelector } from "../store/store";

export default function MyProfile() {
  const { user } = useAppSelector((state) => state.user);

  return (
    <View style={styles.container}>
      {user ? <Text>{user.firstName}</Text> : null}
      <StatusBar style="auto" />
    </View>
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
