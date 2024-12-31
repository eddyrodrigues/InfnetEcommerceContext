import CartEntityResponse from "@/models/CartEntityResponse";
import instance from "./apiService";

export const CartService = {
  getUserCart: async (): Promise<CartEntityResponse> => {
    const response = await instance.get<CartEntityResponse>(
      `http://localhost:3003/api/cart`,
      {
        method: "GET",
      }
    );

    if (response.status == 200) {
      return response.data;
    } else {
      throw Error("Error on gettings Cart Products");
    }
  },
};
