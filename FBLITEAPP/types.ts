interface SignInOutgoing {
  email: string;
  password: string;
}

interface SignInIncoming {
  email: string;
  jwt: string;
}

interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  birthDate: string;
  gender: string;
}
