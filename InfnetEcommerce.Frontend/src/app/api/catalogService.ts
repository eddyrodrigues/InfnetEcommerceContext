export class Product {
  public name!: string;
  public description!: string;
  public price!: number;
  public id!: string;
  public imageBase64!: string;
}

const getAllProducts = async (): Promise<Product[]> => {
  try {
    const responseApi = await fetch("http://localhost:3002/products");
    return responseApi.json();
  } catch (error) {
    throw error;
  }
};

const catalogService = {
  getAllProducts,
};

export default catalogService;
