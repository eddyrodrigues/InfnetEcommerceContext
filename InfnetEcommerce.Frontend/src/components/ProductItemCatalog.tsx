import Product from "../models/Product";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Button } from "./ui/button";
import { cn } from "@/lib/utils";
import { useCallback } from "react";
import instance from "@/services/apiService";
import useCartStore from "@/store/usecartStore";
import CartEntityResponse from "@/models/CartEntityResponse";

export default function ProductItemCatalog({
  productItem,
  setAddingProductToCart,
  addingProductToCart,
}: {
  productItem: Product;
  setAddingProductToCart: (AddingProductToCart: boolean) => void;
  addingProductToCart: boolean;
}) {
  const setCartProducts = useCartStore((state) => state.setCartProducts);

  async function AddProductToCart() {
    return await instance.put<CartEntityResponse>(
      `http://localhost:3003/api/cart/product/${productItem.id}`
    );
  }

  async function handleAddProduct() {
    try {
      setAddingProductToCart(true);
      const response = await AddProductToCart();
      if (response.status === 200) {
        setCartProducts(response.data.products);
      }
    } finally {
      setAddingProductToCart(false);
    }
  }

  const productImage64Src = useCallback((productItem: Product) => {
    return "data:image/png;base64, " + productItem.imageBase64;
  }, []);

  return (
    <>
      <Card
        // className="h-full"
        className={cn("h-full grid", "")}
      >
        <CardHeader className="row-start-1">
          <CardTitle aria-label={productItem.name}>
            {productItem?.name && productItem.name?.length > 20
              ? productItem.name?.substring(0, 40) + "..."
              : productItem.name}
          </CardTitle>
          <CardDescription>Card Description</CardDescription>
        </CardHeader>
        <CardContent className="pb-0 flex justify-center">
          <div className="">
            <img
              className="object-contain h-32 w-60"
              src={productImage64Src(productItem)}
              alt="product image"
            />
          </div>
        </CardContent>
        <CardFooter className="flex items-center flex-col justify-end">
          <div className="">
            <p className="text-slate-950 font-extrabold text-xl">
              R$ {productItem.price}
            </p>
          </div>
          <Button variant="outline" className="w-full">
            View more
          </Button>
          <Button
            onClick={handleAddProduct}
            className="w-full"
            disabled={addingProductToCart}
          >
            Add to Cart
          </Button>
        </CardFooter>
      </Card>
    </>
  );
}
