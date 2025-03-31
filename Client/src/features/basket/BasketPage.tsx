import { Typography, Grid } from "@mui/material";
import { useFetchBasketQuery } from "./basketApi";
import BasketItem from "./BasketItem";

export default function BasketPage() {
  const { data, isLoading } = useFetchBasketQuery(3);

  if (isLoading) return;
  if (data?.basket?.items.length === 0)
    return <Typography variant="h3">Your Basket is Empty</Typography>;
  return (
    <Grid container spacing={2}>
      <Grid size={8}>
        {data?.basket.items.map((item) => (
          <BasketItem item={item} key={item.product.id} />
        ))}
      </Grid>
    </Grid>
  );
}
