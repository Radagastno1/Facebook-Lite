import { StatusBar } from "expo-status-bar";
import React, { useState } from "react";
import { Button, StyleSheet, Text, View } from "react-native";
import { Input } from "react-native-elements";
import { useAppDispatch, useAppSelector } from "../store/store";
import { logInUserAsync } from "../store/userSlice";
import { SignInIncoming, SignInOutgoing } from "../types";

export default function SignIn() {
  const { user } = useAppSelector((state) => state.user);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const error = useAppSelector((state) => state.user.error);
  const [errorPopup, setErrorPopup] = useState(false);
  const dispatch = useAppDispatch();

  const signIn = async () => {
    if (email && password) {
      const signInOutgoing: SignInOutgoing = {
        email: email,
        password: password,
      };
      dispatch(logInUserAsync(signInOutgoing)).then(() => {
        if (error) {
          setErrorPopup(true);
          setEmail("");
          setPassword("");
        } else {
          return;
        }
      });
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
