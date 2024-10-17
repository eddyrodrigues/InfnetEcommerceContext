import { Product } from "../api/catalogService";
import ProductItemCatalog from "./ProductItemCatalog";

export default function CatalogSection({ products }: { products: Product[] }) {
  return (
    <div className="grid sm:grid-cols-1 md:grid-cols-3 lg:grid-cols-4">
      {products.map((product: Product) => (
        <div key={product.id}>
          <ProductItemCatalog productItem={product} />
          <br />
        </div>
      ))}
    </div>
  );
}
