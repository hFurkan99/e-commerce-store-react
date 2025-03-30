import { useState, useEffect } from "react";

import ProductList from "./ProductList";
import { Product } from "../../app/models/product";

export default function Catalog() {
  const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    fetch("https://localhost:5000/api/Products/GetAll")
      .then((response) => response.json())
      .then((data) => setProducts(data.data));
  }, []);
  return (
    <>
      <ProductList products={products} />
    </>
  );
}
