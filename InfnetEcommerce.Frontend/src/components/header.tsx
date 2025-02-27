import { Menubar, MenubarMenu, MenubarTrigger } from "@/components/ui/menubar";
import { Badge } from "@/components/ui/badge";
import useCartStore from "@/store/usecartStore";
import { useEffect } from "react";
import { CartService } from "@/services/cartService";
import { Label } from "@/components/ui/label";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import Product from "@/models/Product";
import { NavLink } from "react-router-dom";

export default function Header() {
  const productInCart = useCartStore((s) => s.cartProductsCount);
  const products = useCartStore().cartProducts;
  const cartStore = useCartStore();
  useEffect(() => {
    CartService.getUserCart()
      .then((cart) => cartStore.setCartProducts(cart?.products))
      .catch((e) => alert("Error on Loading Cart Products qantity: " + e));
  }, []);
  return (
    <Menubar className="h-30">
      <MenubarMenu>
        <div className="flex items-center w-full">
          <div className="flex items-center gap-2">
            <MenubarTrigger className="p-2">
              <NavLink to="/" end>
                Home
              </NavLink>
            </MenubarTrigger>
          </div>
          <div className="ml-auto flex items-center gap-2">
            <MenubarTrigger>
              <Popover>
                <PopoverTrigger asChild>
                  <button className="">
                    <svg
                      className="text-gray-800 dark:text-white h-[24px] w-[24px]"
                      aria-hidden="true"
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 24 24"
                    >
                      <path
                        stroke="currentColor"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth="2"
                        d="M5 4h1.5L9 16m0 0h8m-8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm-8.5-3h9.25L19 7H7.312"
                      />
                    </svg>
                    <Badge>{productInCart}</Badge>
                  </button>
                </PopoverTrigger>
                <PopoverContent className="w-80" align="end">
                  <div className="grid gap-1">
                    <div className="space-y-2">
                      <h4 className="font-medium leading-none">Products</h4>
                      <p className="text-sm text-muted-foreground">
                        Products in your cart.
                      </p>
                    </div>
                    <div className="grid gap-2">
                      {products.map((product: Product) => (
                        <div className="grid grid-cols-1 items-center gap-4">
                          <div key={product.id}>
                            <Label>{`${product.name} - R$ ${product.price}`}</Label>
                          </div>
                        </div>
                      ))}
                    </div>
                  </div>
                </PopoverContent>
              </Popover>
            </MenubarTrigger>
          </div>
        </div>
      </MenubarMenu>
    </Menubar>
  );
}
