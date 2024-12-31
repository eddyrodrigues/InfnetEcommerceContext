import Product from "./Product";

interface CartEntityResponse {
  userId: string;
  id: string;
  products: Product[];
  productQty: number;
}

export default CartEntityResponse;
