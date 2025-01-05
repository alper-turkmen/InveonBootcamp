import React, { createContext, useState, useContext, useEffect } from "react";
import { useSnackbar } from "./AlertContext";

const CartContext = createContext();

export const CartProvider = ({ children }) => {
  const [cart, setCart] = useState([]);
  const { addSnackbar } = useSnackbar();

  useEffect(() => {
    const savedCart = localStorage.getItem("cart");
    if (savedCart) {
      setCart(JSON.parse(savedCart));
    }
  }, []);

  useEffect(() => {
    if (cart.length > 0) {
      localStorage.setItem("cart", JSON.stringify(cart));
    } else {
      localStorage.removeItem("cart");
    }
  }, [cart]);

  const addToCart = (item) => {
    const isItemInCart = cart.some((cartItem) => cartItem.id === item.id);

    if (isItemInCart) {
      addSnackbar("Bu ürün zaten sepetinizde", "error");
    } else {
      const updatedCart = [...cart, item];
      setCart(updatedCart);
      addSnackbar("Ürün sepete eklendi", "success");
    }
  };

  const removeFromCart = (id) => {
    const updatedCart = cart.filter((item) => item.id !== id);
    setCart(updatedCart);
    addSnackbar("Ürün sepetten kaldırıldı", "info");
  };

  const clearCart = () => {
    setCart([]);
    addSnackbar("Sepet temizlendi", "success");
  };

  return (
    <CartContext.Provider
      value={{
        cart,
        addToCart,
        removeFromCart,
        clearCart,
        cartSize: cart.length,
      }}
    >
      {children}
    </CartContext.Provider>
  );
};

export const useCart = () => useContext(CartContext);
