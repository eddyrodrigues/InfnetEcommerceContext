import Product from "../models/Product";
import instance from "@/services/apiService";

export const CatalogService = {
  getAllProducts: async (): Promise<Product[]> => {
    const response = await instance.get<Product[]>(
      "http://localhost:3002/products"
    );
    if (response.status == 200) {
      return response.data;
    } else {
      throw response;
    }
  },
};
