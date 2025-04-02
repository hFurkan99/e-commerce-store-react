import { Grid, Typography } from "@mui/material";
import {
  useFetchProductFiltersQuery,
  useFetchProductsQuery,
} from "./catalogApi";
import ProductList from "./ProductList";
import ProductFilters from "./ProductFilters";
import { useAppDispatch, useAppSelector } from "../../app/store/store";
import AppPagination from "../../app/shared/components/AppPagination";
import { setPageNumber } from "./catalogSlice";

export default function Catalog() {
  const getProductsRequest = useAppSelector((state) => state.catalog);
  const { data, isLoading } = useFetchProductsQuery(getProductsRequest);
  const { data: productFiltersData, isLoading: productFiltersLoading } =
    useFetchProductFiltersQuery();

  const dispatch = useAppDispatch();

  if (isLoading || !data || productFiltersLoading || !productFiltersData)
    return;

  return (
    <Grid container spacing={4}>
      <Grid size={3}>
        <ProductFilters productFiltersData={productFiltersData} />
      </Grid>
      <Grid size={9}>
        {data.items && data.items.length > 0 ? (
          <>
            <ProductList products={data.items} />
            <AppPagination
              metadata={data.pagination}
              onPageChange={(page: number) => {
                dispatch(setPageNumber(page));
                window.scrollTo({ top: 0, behavior: "smooth" });
              }}
            />
          </>
        ) : (
          <Typography variant="h5">
            There are no results for this filter
          </Typography>
        )}
      </Grid>
    </Grid>
  );
}
