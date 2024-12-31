import { ReactNode, useState } from "react";
import { GlobalContext } from "./GlobalContext";
import { CartService } from "@/services/cartService";

export const GlobalContextProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  const [cartProducts, setCartProducts] = useState(0);
  const globalContextValue = { cartProducts, setCartProducts };
  CartService.getUserCart().then((c) => setCartProducts(c?.productQty ?? 0));
  return (
    <GlobalContext.Provider value={globalContextValue}>
      {children}
    </GlobalContext.Provider>
  );
};
