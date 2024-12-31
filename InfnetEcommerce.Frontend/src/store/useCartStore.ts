import Product from "@/models/Product";
import { create } from "zustand";

interface CartState {
  cartProducts: Product[];
  cartProductsCount: number;
  setCartProducts: (products: Product[]) => Promise<void>;
}

const useCartStore = create<CartState>()((set) => ({
  cartProducts: [],
  cartProductsCount: 0,
  setCartProducts: async (products: Product[]) => {
    set({ cartProducts: products, cartProductsCount: products.length });
  },
}));

export default useCartStore;
