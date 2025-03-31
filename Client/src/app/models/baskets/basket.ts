import { Product } from "../products/product";

export type Basket = {
  id: number;
  items: Item[];
};

export type Item = {
  id: number;
  quantity: number;
  product: Product;
};
