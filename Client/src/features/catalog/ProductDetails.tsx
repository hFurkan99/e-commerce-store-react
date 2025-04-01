import { useParams } from "react-router-dom";
import {
  Button,
  Divider,
  Grid,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  TextField,
  Typography,
} from "@mui/material";
import { useFetchProductDetailQuery } from "./catalogApi";
import {
  useAddItemToBasketMutation,
  useFetchBasketQuery,
  useRemoveItemFromBasketMutation,
} from "../basket/basketApi";
import { ChangeEvent, useEffect, useState } from "react";
export default function ProductDetails() {
  const { id } = useParams();

  const [addItemToBasket] = useAddItemToBasketMutation();
  const [removeItemFromBasket] = useRemoveItemFromBasketMutation();
  const { data } = useFetchBasketQuery(3);
  const item = data?.basket.items.find((x) => x.product.id === parseInt(id!));
  const [quantity, setQuantity] = useState(0);

  useEffect(() => {
    if (item) setQuantity(item.quantity);
  }, [item]);

  const { data: product, isLoading } = useFetchProductDetailQuery(
    id ? parseInt(id) : 0
  );

  if (!product || isLoading) return;

  const handleUpdateBasket = () => {
    const updatedQuantity = item
      ? Math.abs(quantity - item.quantity)
      : quantity;

    if (!item || quantity > item.quantity)
      addItemToBasket({
        basketId: 3,
        productId: product.id,
        quantity: updatedQuantity,
      });
    else
      removeItemFromBasket({
        basketId: 3,
        productId: product.id,
        quantity: updatedQuantity,
      });
  };

  const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
    const value = parseInt(event?.currentTarget.value);
    if (value >= 0) setQuantity(value);
  };

  const productDetails = [
    { label: "Name", value: product.name },
    { label: "Description", value: product.description },
    { label: "Type", value: product.type },
    { label: "Brand", value: product.brand },
    { label: "Quantity in stock", value: product.quantityInStock },
  ];

  return (
    <Grid container spacing={6} maxWidth="lg" sx={{ mx: "auto" }}>
      <Grid size={6}>
        <img
          src={product?.pictureUrl}
          alt={product.name}
          style={{ width: "100%" }}
        />
      </Grid>
      <Grid size={6}>
        <Typography variant="h3">{product.name}</Typography>
        <Divider sx={{ mb: 2 }} />
        <Typography variant="h4" color="secondary">
          ${(product.price / 100).toFixed(2)}
        </Typography>
        <TableContainer>
          <Table
            sx={{
              "& td": { fontSize: "1rem" },
            }}
          >
            <TableBody>
              {productDetails.map((detail, index) => (
                <TableRow key={index}>
                  <TableCell sx={{ fontWeight: "bold" }}>
                    {detail.label}
                  </TableCell>
                  <TableCell>{detail.value}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
        <Grid container spacing={2} marginTop={3}>
          <Grid size={6}>
            <TextField
              variant="outlined"
              type="number"
              label="Quantity in basket"
              fullWidth
              value={quantity}
              onChange={handleInputChange}
            />
          </Grid>
          <Grid size={6}>
            <Button
              onClick={handleUpdateBasket}
              disabled={
                quantity === item?.quantity || (!item && quantity === 0)
              }
              color="primary"
              size="large"
              variant="contained"
              fullWidth
              sx={{ height: "55px" }}
            >
              {item ? "Update quantity" : "Add to basket"}
            </Button>
          </Grid>
        </Grid>
      </Grid>
    </Grid>
  );
}
