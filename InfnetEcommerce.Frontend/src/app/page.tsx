import catalogService from "./api/catalogService";
import CatalogSection from "./components/CatalogSection";
import Header from "./header";

// eslint-disable-next-line @next/next/no-async-client-component
export default async function Home() {
  const products = await catalogService.getAllProducts();
  return (
    <div className="bg-gray-50">
      <Header />
      <main>
        <div className="mb-3">
          <div className="text-zinc-700">Product List</div>
        </div>
        <CatalogSection products={products} />
      </main>
    </div>
  );
}
