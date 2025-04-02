import { createApi } from "@reduxjs/toolkit/query/react";
import { Product } from "../../app/models/products/product";
import { baseQueryWithErrorHandling } from "../../app/api/baseApi";
import { GetProductsRequest } from "../../app/models/products/getProductsRequest";
import { filterEmptyValues } from "../../lib/util";
import { Pagination } from "../../app/models/common/pagination";

export const catalogApi = createApi({
  reducerPath: "catalogApi",
  baseQuery: baseQueryWithErrorHandling,
  endpoints: (builder) => ({
    fetchProducts: builder.query<
      { items: Product[]; pagination: Pagination },
      GetProductsRequest
    >({
      query: (getProductsRequest) => {
        return {
          url: "Products/GetProducts",
          params: filterEmptyValues(getProductsRequest),
        };
      },
      transformResponse: (response: { data: Product[] }, meta) => {
        const paginationHeader = meta?.response?.headers.get("Pagination");
        const pagination = paginationHeader
          ? JSON.parse(paginationHeader)
          : null;
        return { items: response.data, pagination };
      },
    }),
    fetchProductDetail: builder.query<Product, number>({
      query: (productId) => `Products/GetById?id=${productId}`,
      transformResponse: (response: { data: Product }) => response.data,
    }),
    fetchProductFilters: builder.query<
      { brands: string[]; types: string[] },
      void
    >({
      query: () => "/Products/GetProductFilters",
      transformResponse: (response: {
        data: { brands: string[]; types: string[] };
      }) => response.data,
    }),
  }),
});

export const {
  useFetchProductsQuery,
  useFetchProductDetailQuery,
  useFetchProductFiltersQuery,
} = catalogApi;
