import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { signInAsync } from "../api/signin";
import { SignInIncoming, SignInOutgoing, User } from "../types";

interface UserState {
  user: User | undefined;
  error: string | null;
}

export const initialState: UserState = {
  user: undefined,
  error: null,
};

export const logInUserAsync = createAsyncThunk<
  User, // returvärder
  SignInOutgoing, //inkommande värde
  { rejectValue: string }
>("user/logInUser", async (incomingUser, thunkAPI) => {
  try {
    const response = await signInAsync(incomingUser);
    return response as User;
  } catch (error) {
    throw new Error("Användarnamn eller lösenord var felaktigt.");
  }
});

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    logOutUser: (state) => {
      state.user = undefined;
    },
    setActiveUser: (state, action: PayloadAction<User | undefined>) => {
      if (action.payload) {
        state.user = {
          id: action.payload.id,
          firstName: action.payload.firstName,
          lastName: action.payload.lastName,
          gender: action.payload.gender,
          email: action.payload.email,
          password: action.payload.password,
          birthDate: action.payload.birthDate,
        };
      }
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(logInUserAsync.fulfilled, (state, action) => {
        if (action.payload) {
          state.user = {
            id: action.payload.id,
            firstName: action.payload.firstName,
            lastName: action.payload.lastName,
            gender: action.payload.gender,
            email: action.payload.email,
            password: action.payload.password,
            birthDate: action.payload.birthDate,
          };
          state.error = null;
        }
      })
      .addCase(logInUserAsync.rejected, (state, action) => {
        state.error = "Användarnamn eller lösenord är felaktigt.";
      });
  },
});

export const userReducer = userSlice.reducer;
export const { logOutUser, setActiveUser } = userSlice.actions;
