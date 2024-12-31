export type UserDataAndAccessTokenResponse = {
  accessToken: string;
  expiresIn: number;
  refreshToken: string;
  user: UserData;
};
export type UserData = {
  userId: number;
  userName: string;
  userEmail: string;
};
