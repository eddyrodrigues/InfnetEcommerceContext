import { retrieveUser } from "@/lib/helpers";
import { UserDataAndAccessTokenResponse } from "@/models/UserDataAndAccessTokenResponse";
import { create } from "zustand";
// import { devtools, persist } from "zustand/middleware";
// import type {} from '@redux-devtools/extension' // required for devtools typing

interface LoginState {
  user: UserDataAndAccessTokenResponse | null;
  isAuthenticated: boolean;
  SetUser: (accessToken: UserDataAndAccessTokenResponse) => void;
  RetrieveUser: () => UserDataAndAccessTokenResponse | null;
  Logout: () => void;
}

const useLoginStore = create<LoginState>()((set) => ({
  user: null,
  isAuthenticated: false,
  Logout: () => {
    localStorage.removeItem("user");
  },
  SetUser: (UserDataAndAccessTokenResponse) => {
    console.log("set user", UserDataAndAccessTokenResponse);
    localStorage.setItem(
      "user",
      JSON.stringify(UserDataAndAccessTokenResponse)
    );
    set(() => ({
      user: UserDataAndAccessTokenResponse,
      isAuthenticated: true,
    }));
  },
  RetrieveUser: () => {
    const userData = retrieveUser();

    if (userData) {
      set(() => ({ user: userData, isAuthenticated: true }));
      return userData;
    }
    return null;
  },
}));

export default useLoginStore;
