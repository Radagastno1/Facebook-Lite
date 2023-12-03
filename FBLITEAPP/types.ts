export interface AppState {
  isLoading: boolean;
}

export interface SignInOutgoing {
  email: string;
  password: string;
}

export interface SignInIncoming {
  email: string;
  jwt: string;
}

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  birthDate: string;
  gender: string;
}
