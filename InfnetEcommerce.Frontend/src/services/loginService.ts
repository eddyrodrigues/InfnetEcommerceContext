import { UserDataAndAccessTokenResponse } from "@/models/UserDataAndAccessTokenResponse";
import instance from "./apiService";

interface LoginService {
  getAccessToken: (
    login: string,
    password: string
  ) => Promise<UserDataAndAccessTokenResponse | null>;
}

const loginServiceFunctions: LoginService = {
  async getAccessToken(
    login,
    password
  ): Promise<UserDataAndAccessTokenResponse | null> {
    try {
      const response = await instance.post("http://localhost:3001/user/login", {
        userName: login,
        password: password,
      });

      if (response.status !== 200) return null;

      const jsonResponse = await response.data;
      return <UserDataAndAccessTokenResponse>jsonResponse;
    } catch (e) {
      console.log(e);
      return null;
    }
  },
};

export default {
  ...loginServiceFunctions,
};
