export type GetProductsRequest = {
  searchTerm?: string;
  orderBy: string;
  brands: string[];
  types: string[];
  descending: boolean;
  pageNumber: number;
  pageSize: number;
};
