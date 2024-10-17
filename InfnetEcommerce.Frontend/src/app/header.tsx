"use client";
import { useEffect, useState } from "react";
import { Product } from "./api/catalogService";

export default function Header() {
  const [productItems, setProductItems] = useState(0);
  useEffect(() => {
    fetch(
      `http://localhost:3003/api/cart/2c66d441-0b36-4c2f-9e90-7b0c571e5834`,
      {
        method: "GET",
      }
    )
      .then((r) =>
        r.json().then(({ products }: { products: Product[] }) => {
          setProductItems(products.length);
          console.log(productItems);
        })
      )
      .catch((c) => console.log(c));
  });
  return (
    <nav className="">
      <ol>
        <li className="text-blue-300">Home</li>
        <li className="text-blue-300">About</li>
        <li className="text-blue-300">Catalog</li>
        <li className="text-blue-300">
          Shopping Cart (
          <span className="font-bold text-orange-500">{productItems}</span>)
        </li>
      </ol>
    </nav>
  );
}
