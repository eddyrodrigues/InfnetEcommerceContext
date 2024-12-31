import "./App.css";

import { createBrowserRouter, RouterProvider } from "react-router-dom";

import AuthenticationPage from "./pages/Login/page";
import PrivateRouter from "./router/PrivateRouter";
import CatalogSection from "./components/CatalogSection";
import Header from "./components/header";
import { GlobalContextProvider } from "./contexts/GlobalContextProvider";
import { CatalogService } from "./services/catalogService";
import useCartStore from "./store/usecartStore";
import { useEffect } from "react";
import { CartService } from "./services/cartService";

const router = createBrowserRouter([
  {
    path: "/",
    element: (
      <PrivateRouter>
        <Header></Header>
        <CatalogSection></CatalogSection>
      </PrivateRouter>
    ),
  },
  {
    path: "/login",
    element: <AuthenticationPage />,
  },
]);
function App() {
  const cartStore = useCartStore();
  useEffect(() => {
    CartService.getUserCart()
      .then((cart) => cartStore.setCartProducts(cart?.products))
      .catch((e) => alert("Error on Loading Products: " + e));
  }, []);
  return <RouterProvider router={router} />;
}

export default App;
