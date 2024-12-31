import React from "react";
import Product from "../models/Product";

const ProductItemDetail = ({
  productSelected,
}: {
  productSelected: Product;
}) => {
  if (productSelected === null) {
    return;
  }
  return (
    <div>
      {productSelected.name}
      {productSelected.description}
      R$ {productSelected.price}
    </div>
  );
};

export default ProductItemDetail;
