import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import React, { useState } from "react";
import MyProfile from "../screens/MyProfile";
import SignIn from "../screens/Signin";
import SplashScreen from "../screens/SplashScreen";
import { useAppDispatch, useAppSelector } from "../store/store";

export type RootStackParamList = {
  SplashScreen: undefined;
  SignIn: undefined;
  MyProfile: undefined;
};

const Stack = createNativeStackNavigator<RootStackParamList>();

export default function RootNavigator() {
  //   const [isUserFetched, setUserFetched] = useState(false);
  const user = useAppSelector((state) => state.user.user);

  return (
    <NavigationContainer>
      <Stack.Navigator>
        {user ? (
          <>
            <Stack.Screen
              name="MyProfile"
              options={{ headerShown: false }}
              component={MyProfile}
            />
          </>
        ) : (
          <>
            <Stack.Screen
              name="SignIn"
              component={SignIn}
              options={{ headerShown: false }}
            />
          </>
        )}
      </Stack.Navigator>
    </NavigationContainer>
  );
}
