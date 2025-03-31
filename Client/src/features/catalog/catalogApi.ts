import { createApi } from "@reduxjs/toolkit/query/react";
import { Product } from "../../app/models/products/product";
import { baseQueryWithErrorHandling } from "../../app/api/baseApi";

export const catalogApi = createApi({
  reducerPath: "catalogApi",
  baseQuery: baseQueryWithErrorHandling,
  endpoints: (builder) => ({
    fetchProducts: builder.query<Product[], void>({
      query: () => ({ url: "Products/GetAll" }),
      transformResponse: (response: { data: Product[] }) => response.data,
    }),
    fetchProductDetail: builder.query<Product, number>({
      query: (productId) => `Products/GetById?id=${productId}`,
      transformResponse: (response: { data: Product }) => response.data,
    }),
  }),
});

export const { useFetchProductsQuery, useFetchProductDetailQuery } = catalogApi;
