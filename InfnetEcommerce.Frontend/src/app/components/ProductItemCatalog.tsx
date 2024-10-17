"use client";
import { Product } from "../api/catalogService";
export default function ProductItemCatalog({
  productItem,
}: {
  productItem: Product;
}) {
  function AddProductToCart() {
    fetch(
      `http://localhost:3003/api/cart/user-id/28345528-4467-452c-ab29-58e020a7fbb0/product/${productItem.id}`,
      {
        method: "PUT",
      }
    )
      .then((r) => console.log(r))
      .catch((c) => console.log(c));
  }

  return (
    <div className="text-zinc-700 shadow-xl mx-2 bg-white h-96 ">
      <div className="h-3/4 border-b-2 border-b-slate-300">
        <img
          src={"data:image/png;base64, " + productItem.imageBase64}
          className="object-contain h-48 w-96"
        ></img>
      </div>
      <div className="p-1">
        <div className="columns-2 border-b-2 border-zinc-700 mb-1">
          <div>{productItem.name}</div>
          <div className="text-right font-black text-purple-800">
            R$ {productItem.price}
          </div>
        </div>
        <div className="text-wrap">{productItem.description}</div>
        <a className="font-bold cursor-pointer" onClick={AddProductToCart}>
          Add to Cart
        </a>
      </div>
    </div>
  );
}
