import { Box, Grid, IconButton, Paper, Typography } from "@mui/material";
import { Item } from "../../app/models/baskets/basket";
import { Add, Close, Remove } from "@mui/icons-material";
import {
  useAddItemToBasketMutation,
  useRemoveItemFromBasketMutation,
} from "./basketApi";
import { currencyFormat } from "../../lib/util";

type Props = {
  item: Item;
};

export default function BasketItem({ item }: Props) {
  const [removeItemFromBasket] = useRemoveItemFromBasketMutation();
  const [addItemToBasket] = useAddItemToBasketMutation();

  return (
    <Paper
      sx={{
        height: 140,
        borderRadius: 3,
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        mb: 2,
      }}
    >
      <Box display="flex" alignItems="center">
        <Box
          component="img"
          src={item.product.pictureUrl}
          alt={item.product.name}
          sx={{
            width: 100,
            height: 100,
            objectFit: "cover",
            borderRadius: "4px",
            mr: 8,
            ml: 4,
          }}
        />

        <Box display="flex" flexDirection="column" gap={1}>
          <Typography variant="h6">{item.product.name}</Typography>

          <Box display="flex" alignItems="center" gap={3}>
            <Typography sx={{ fontSize: "1.1rem" }}>
              {currencyFormat(item.product.price)} x {item.quantity}
            </Typography>
            <Typography sx={{ fontSize: "1.1rem" }} color="primary">
              {currencyFormat(item.product.price * item.quantity)}
            </Typography>
          </Box>

          <Grid container spacing={1} alignItems="center">
            <IconButton
              onClick={() =>
                removeItemFromBasket({
                  basketId: 3,
                  productId: item.product.id,
                  quantity: 1,
                })
              }
              color="error"
              size="small"
              sx={{ border: 1, borderRadius: 1, minWidth: 0 }}
            >
              <Remove />
            </IconButton>
            <Typography variant="h6">{item.quantity}</Typography>
            <IconButton
              onClick={() =>
                addItemToBasket({
                  basketId: 3,
                  productId: item.product.id,
                  quantity: 1,
                })
              }
              color="success"
              size="small"
              sx={{ border: 1, borderRadius: 1, minWidth: 0 }}
            >
              <Add />
            </IconButton>
          </Grid>
        </Box>
      </Box>
      <IconButton
        onClick={() =>
          removeItemFromBasket({
            basketId: 3,
            productId: item.product.id,
            quantity: item.quantity,
          })
        }
        color="error"
        size="small"
        sx={{
          border: 1,
          borderRadius: 1,
          minWidth: 0,
          alignSelf: "start",
          mr: 1,
          mt: 1,
        }}
      >
        <Close />
      </IconButton>
    </Paper>
  );
}
