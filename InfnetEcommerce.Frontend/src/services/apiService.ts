import { retrieveUser } from "@/lib/helpers";
import axios from "axios";
// import { useNavigate } from "react-router-dom";

const instance = axios.create({});

// Add a request interceptor
instance.interceptors.request.use(
  function (config) {
    // const navigate = useNavigate();
    // Do something before request is sent
    const accessTokenInformation = retrieveUser();

    // if (accessTokenInformation == null) {
    //   navigate({ pathname: "/login" });
    // }
    if (
      accessTokenInformation?.accessToken &&
      accessTokenInformation?.expiresIn > Date.now()
    ) {
      config.headers.Authorization =
        "Bearer " + accessTokenInformation.accessToken;
    }
    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

instance.interceptors.response.use(
  function (response) {
    // Do something before request is sent
    console.log(response);
    if (response.status == 401) {
      // const navigate = useNavigate();
      // navigate("/login");
    }
    return response;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

export default instance;
