import { UserDataAndAccessTokenResponse } from "@/models/UserDataAndAccessTokenResponse";

export const retrieveUser = () => {
  const user = localStorage.getItem("user");
  if (!user) {
    // set(() => ({ user: null, isAuthenticated: false }));
    localStorage.removeItem("user");
    return null;
  }

  const userData = <UserDataAndAccessTokenResponse>JSON.parse(user);

  const expirationData = new Date(userData.expiresIn);

  if (isNaN(expirationData.getTime())) {
    // set(() => ({ user: null, isAuthenticated: false }));
    localStorage.removeItem("user");
    return null;
  }

  if (expirationData > new Date()) {
    // set(() => ({ user: userData, isAuthenticated: true }));
    return userData;
  }
  // set(() => ({ user: null, isAuthenticated: false }));
  localStorage.removeItem("user");
  return null;
};
