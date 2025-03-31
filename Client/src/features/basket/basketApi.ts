import { createApi } from "@reduxjs/toolkit/query/react";
import { baseQueryWithErrorHandling } from "../../app/api/baseApi";
import { Basket } from "../../app/models/baskets/basket";
import { AddItemRequest } from "../../app/models/baskets/addItemRequest";
import { RemoveItemRequest } from "../../app/models/baskets/removeItemRequest";
import { Product } from "../../app/models/products/product";
import { RootState } from "../../app/store/store";

export const basketApi = createApi({
  reducerPath: "basketApi",
  baseQuery: baseQueryWithErrorHandling,
  tagTypes: ["Basket"],
  endpoints: (builder) => ({
    fetchBasket: builder.query<{ basket: Basket; message: string }, number>({
      query: (basketId) => `Basket/GetBasket?basketId=${basketId}`,
      transformResponse: (response: { data: Basket; message: string }) => ({
        basket: response.data,
        message: response.message,
      }),
    }),
    addItemToBasket: builder.mutation<{ message: string }, AddItemRequest>({
      query: (body) => ({
        url: "Basket/AddItemToBasket",
        method: "POST",
        body,
      }),
      transformResponse: (response: { message: string }) => ({
        message: response.message,
      }),
      onQueryStarted: async (
        { productId, basketId, quantity },
        { dispatch, queryFulfilled, getState }
      ) => {
        let isNewBasket = false;

        const state = getState() as RootState;
        const cachedProducts = state.catalogApi.queries[
          "fetchProducts(undefined)"
        ]?.data as Product[] | undefined;

        const cachedProduct = cachedProducts?.find(
          (product: Product) => product.id === productId
        );

        const patchResult = dispatch(
          basketApi.util.updateQueryData("fetchBasket", basketId, (draft) => {
            if (!draft.basket.id) isNewBasket = true;

            if (!isNewBasket) {
              const existingItem = draft.basket.items.find(
                (item) => item.product.id === productId
              );
              if (existingItem) existingItem.quantity += quantity;
              else {
                draft.basket.items.push({
                  id: 1,
                  product: cachedProduct || {
                    id: productId,
                    brand: "",
                    description: "",
                    name: "",
                    pictureUrl: "",
                    price: 0,
                    quantityInStock: 0,
                    type: "",
                  },
                  quantity: quantity,
                });
              }
            }
          })
        );
        try {
          await queryFulfilled;

          if (isNewBasket) dispatch(basketApi.util.invalidateTags(["Basket"]));
        } catch {
          patchResult.undo();
        }
      },
    }),
    removeItemFromBasket: builder.mutation<
      { message: string },
      RemoveItemRequest
    >({
      query: (body) => ({
        url: "Basket/RemoveItemFromBasket",
        method: "DELETE",
        body,
      }),
      transformResponse: (response: { message: string }) => ({
        message: response.message,
      }),
      onQueryStarted: async (
        { basketId, productId, quantity },
        { dispatch, queryFulfilled }
      ) => {
        const patchResult = dispatch(
          basketApi.util.updateQueryData("fetchBasket", basketId, (draft) => {
            const itemIndex = draft.basket.items.findIndex(
              (item) => item.product.id === productId
            );
            if (itemIndex >= 0) {
              draft.basket.items[itemIndex].quantity -= quantity;
              if (draft.basket.items[itemIndex].quantity <= 0) {
                draft.basket.items.splice(itemIndex, 1);
              }
            }
          })
        );
        try {
          await queryFulfilled;
        } catch {
          patchResult.undo();
        }
      },
    }),
  }),
});

export const {
  useFetchBasketQuery,
  useAddItemToBasketMutation,
  useRemoveItemFromBasketMutation,
} = basketApi;
