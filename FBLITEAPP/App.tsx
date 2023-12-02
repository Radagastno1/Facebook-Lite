import { StatusBar } from "expo-status-bar";
import React, { useState } from "react";
import { Button, StyleSheet, View, Text } from "react-native";
import { Input } from "react-native-elements";
import { signInAsync } from "./api/signin";

export default function App() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [user, setUser] = useState<User>();

  const signIn = async () => {
    if (email && password) {
      const signInOutgoing: SignInOutgoing = {
        email: email,
        password: password,
      };
      try {
        const signedInUser = await signInAsync(signInOutgoing);
        if (signedInUser) {
          setUser(signedInUser);
        }
      } catch (error) {
        console.error("Sign-in error:", error);
      }
    } else {
      return;
    }
  };

  return (
    <View style={styles.container}>
      <Input
        placeholder="Email"
        onChangeText={(text) => setEmail(text)}
      ></Input>
      <Input
        placeholder="Password"
        onChangeText={(text) => setPassword(text)}
      ></Input>
      <Button
        title="Sign in"
        onPress={() => signIn()}
        color="rgba(79,44,84,255)"
      ></Button>
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
