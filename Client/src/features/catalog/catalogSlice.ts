import { createSlice } from "@reduxjs/toolkit";
import { GetProductsRequest } from "../../app/models/products/getProductsRequest";

const initialState: GetProductsRequest = {
  pageNumber: 1,
  pageSize: 8,
  types: [],
  brands: [],
  searchTerm: "",
  orderBy: "name",
  descending: false,
};

export const catalogSlice = createSlice({
  name: "catalogSlice",
  initialState,
  reducers: {
    setPageNumber(state, action) {
      state.pageNumber = action.payload;
    },
    setPageSize(state, action) {
      state.pageSize = action.payload;
    },
    setOrderBy(state, action) {
      state.orderBy = action.payload;
      state.pageNumber = 1;
    },
    setTypes(state, action) {
      state.types = action.payload;
      state.pageNumber = 1;
    },
    setBrands(state, action) {
      state.brands = action.payload;
      state.pageNumber = 1;
    },
    setSearchTerm(state, action) {
      state.searchTerm = action.payload;
      state.pageNumber = 1;
    },
    resetParams() {
      return initialState;
    },
  },
});

export const {
  setBrands,
  setOrderBy,
  setPageNumber,
  setSearchTerm,
  setPageSize,
  setTypes,
  resetParams,
} = catalogSlice.actions;
