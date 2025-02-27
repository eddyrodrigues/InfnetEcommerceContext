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
  return <RouterProvider router={router} />;
}

export default App;
