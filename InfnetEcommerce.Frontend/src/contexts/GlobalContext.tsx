import React from "react";
import IGlobalContext from "./IGlobalContext";

export const GlobalContext = React.createContext<IGlobalContext>({
  cartProducts: 0,
  setCartProducts: () => {},
});
