import { UserDataAndAccessTokenResponse } from "@/models/UserDataAndAccessTokenResponse";
import instance from "@/services/apiService";
import useLoginStore from "@/store/useLoginStore";
import { ReactNode, useEffect, useRef, useState } from "react";
import { Navigate } from "react-router-dom";
type Props = {
  children: ReactNode;
};

type User = {
  userName?: string;
  userId?: number;
  email: string;
  permissions: string[];
  roles: string[];
};

function AuthProvider({ children }: Props) {
  // const [user, setUser] = useState<User>();
  // console.log("ativou authprovider");
  // // const { pathname } = useLocation();
  // const loginStore = useLoginStore();
  // const token = useRef<UserDataAndAccessTokenResponse | null>(null);
  // async function signIn(params: SignInCredentials) {
  //   const { email, password } = params;
  //   try {
  //     const response = await api.post("/sessions", { email, password });
  //     const { token, refreshToken, permissions, roles } = response.data;
  //     createSessionCookies({ token, refreshToken });
  //     setUser({ email, permissions, roles });
  //     setAuthorizationHeader({ request: api.defaults, token });
  //   } catch (error) {
  //     const err = error as AxiosError;
  //     return err;
  //   }
  // }
  // function signOut() {
  //   removeSessionCookies();
  //   setUser(undefined);
  //   setLoadingUserData(false);
  //   navigate(paths.LOGIN_PATH);
  // }
  // useEffect(() => {
  //   token.current = loginStore.RetrieveUser();
  //   if (!token) {
  //     setUser(undefined);
  //     // setLoadingUserData(false);
  //   }
  // }, [token]);
  // useEffect(() => {
  //   const token = loginStore.RetrieveUser();
  //   async function getUserData() {
  //     // setLoadingUserData(true);
  //     try {
  //       const response = await instance.get("http://localhost:3001/user/info");
  //       if (response?.data) {
  //         const { email, permissions, roles } = response.data;
  //         setUser({ email, permissions, roles });
  //       }
  //     } catch (error) {
  //       /**
  //        * an error handler can be added here
  //        */
  //       return error;
  //     } finally {
  //       // setLoadingUserData(false);
  //     }
  //   }
  //   if (token) {
  //     // setAuthorizationHeader({ request: api.defaults, token });
  //     // getUserData();
  //   }
  // }, []);
  // return children;
}
export default AuthProvider;
