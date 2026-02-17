export interface User {
  userId: number;
  fullName: string;
  email: string;
  role: string;
}

export interface UserInterface{
    data:User[];
}