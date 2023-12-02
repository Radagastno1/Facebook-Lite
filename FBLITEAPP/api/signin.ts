import AsyncStorage from "@react-native-async-storage/async-storage";
import Constants from "expo-constants";
import jwt from "jsonwebtoken";

//vet ej om detta är korrekt sätt att se om produktion eller ej
const isProduction = !__DEV__;
let uri = "";

if (isProduction) {
  //lägg till offentliga ip adressen när det är i produktion
  uri = "http://35.173.198.228/users/";
} else {
  //annars används ipadressen då för att prata med den lokala
  const hostUri = Constants?.expoConfig?.hostUri;
  if (hostUri) {
    const parts = hostUri.split(":");
    if (parts.length >= 2) {
      uri = `http://${parts[0]}:5290`;
      console.log("uri är: " + uri);
    } else {
      console.warn("Invalid hostUri format. Using default URI.");
    }
  }
}

export async function signInAsync(
  signInOutgoing: SignInOutgoing
): Promise<User | null> {
  try {
    console.log("det som ska skickas iväg är: ", signInOutgoing);
    const signInIncoming = await fetchLogInUser(signInOutgoing);
    if (!signInIncoming) {
      console.log("no sign in incoming");
      return null;
    }
    if (signInIncoming) {
      const jwtToken = signInIncoming.jwt;
      console.log("Signin incoming: ", signInIncoming);
      try {
        // const decodedToken: any = jwt.verify(
        //   jwtToken,
        //   "76b5b9aec7f5ecda17da19a64223a47d996a6bf44cc522c1c5db0d3107fc0ed8"
        // );
        // const userId = decodedToken.sub || decodedToken.UserId;

        // Sparar användar-ID i AsyncStorage
        await AsyncStorage.setItem("jwt", jwtToken.toString());

        // Hämta användaren med det nyligen sparade användar-ID:et
        const fetchedUser = await fetchGetUser();
        console.log(fetchedUser);
        return fetchedUser;
      } catch (error) {
        console.error("Error decoding JWT token:", error);
      }
    }
    return null;
  } catch (error) {
    console.error("An error occurred during sign in:", error);
    return null;
  }
}

// export async function createAccountAsync(user: User): Promise<User | null> {
//   try {
//     const createdUser = await fetchCreateUserReal(user);
//     if (createdUser != null) {
//       return createdUser;
//     }
//     return null;
//   } catch (error) {
//     console.error("An error occurred during sign in:", error);
//     return null;
//   }
// }

async function fetchLogInUser(signInOutgoing: SignInOutgoing) {
  console.log("ska anropa logoin");
  const logInUserUri = uri + "/auth";
  const requestBody = {
    email: signInOutgoing.email,
    password: signInOutgoing.password,
  };

  return fetch(logInUserUri, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(requestBody),
  })
    .then((response) => {
      console.log("response:", JSON.stringify(response));
      if (!response.ok) {
        throw new Error(`Nätverksfel - ${response.status}`);
      }
      return response.json();
    })
    .then((data) => {
      //här kommer liksom jwtn första gången ihop med id och mail som är inbakade då
      console.log("DATA FROM AUTH endpointen: ", data);
      return data as SignInIncoming;
    })
    .catch((error) => {
      console.error(error);
      return null;
    });
}

export const emptyAsyncStorage = async () => {
  await AsyncStorage.setItem("UserId", "");
};

export async function fetchGetUser() {
  console.log("ska köra get user");

  const jwt = await AsyncStorage.getItem("jwt");
  if (!jwt) {
    // Handle the case where JWT is not available
    console.error("JWT not found in AsyncStorage");
    return null;
  }

  const headers = {
    "Content-Type": "application/json",
    Authorization: `Bearer ${jwt}`,
  };

  const getUserUri = uri + `/user`;
  console.log("USER URI: ", getUserUri);

  return fetch(getUserUri, {
    method: "POST",
    headers: headers,
  })
    .then((response) => {
      console.log("response from get user:", JSON.stringify(response));
      return response.json();
    })
    .then((user) => {
      console.log("response from get user: ", user);
      return user as User;
    })
    .catch((error) => {
      console.error("ERROR:", error);
      emptyAsyncStorage();
      return null;
    });
}

// function fetchCreateUserReal(user: User) {
//   const createUserUri = uri + "/create";
//   const requestBody = {
//     user,
//   };

//   const headers = {
//     "Content-Type": "application/json",
//   };

//   return fetch(createUserUri, {
//     method: "POST",
//     headers,
//     body: JSON.stringify(user),
//   })
//     .then((response) => {
//       if (!response.ok) {
//         throw new Error("Network response was not ok");
//       }
//       return response.json() as Promise<User>;
//     })
//     .then((userCreated) => {
//       console.log("user created:", userCreated);
//     })
//     .catch((error) => {
//       console.error("error creating user:", error);
//     });
// }

// export function fetchEditUser(user: User, id: number) {
//   const editUserUri = uri + `/${id}`;

//   const requestBody = { ...user, id };

//   const headers = {
//     "Content-Type": "application/json",
//   };

//   return fetch(editUserUri, {
//     method: "PUT",
//     headers,
//     body: JSON.stringify(requestBody),
//   })
//     .then((response) => {
//       console.log("requestbody: ", requestBody);
//       console.log("response when updating:", response);
//       if (!response.ok) {
//         throw new Error("Network response was not ok");
//       }
//       return response.json() as Promise<User>;
//     })
//     .then((userUpdated) => {
//       return userUpdated as User;
//     })
//     .catch((error) => {
//       console.error("error updating user:", error);
//     });
// }
