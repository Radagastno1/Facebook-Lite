import { createSlice } from "@reduxjs/toolkit";
import { AppState } from "../types";

const initialState: AppState = {
  isLoading: true,
};

const appSlice = createSlice({
  name: "app",
  initialState,
  reducers: {
    setLoading: (state, action) => {
      state.isLoading = action.payload;
    },
  },
});

export const { setLoading } = appSlice.actions;
export default appSlice.reducer;
