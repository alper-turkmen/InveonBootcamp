import React, { useState } from "react";
import { useCart } from "../../contexts/CartContext";
import Header from "../../components/Header";
import MiniButton from "../../components/MiniButton";
import { useSnackbar } from "../../contexts/AlertContext";
import axios from "../../utils/axiosconf";

const UserCart = () => {
  const { cart, removeFromCart, clearCart, cartSize } = useCart();
  const [isOrderModalOpen, setIsOrderModalOpen] = useState(false);
  const [cardNumber, setCardNumber] = useState("");
  const [expiryDate, setExpiryDate] = useState("");
  const [cvv, setCvv] = useState("");
  const [error, setError] = useState("");
  const { addSnackbar } = useSnackbar();

  const toggleOrderModal = () => {
    setIsOrderModalOpen(!isOrderModalOpen);
    setError("");
  };

  const handleConfirmOrder = async () => {
    if (!cardNumber || !expiryDate || !cvv) {
      setError("Lütfen tüm alanları doldurun!");
      return;
    }

    const courseIds = cart.map((item) => item.id);
    const orderData = { courseIds };

    try {
      const response = await axios.post("/Orders", orderData, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      });

      addSnackbar("Sipariş başarıyla alındı!", "success");
      clearCart();
      toggleOrderModal();
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Sipariş sırasında bir hata oluştu.";
      setError(errorMessage);
      addSnackbar(errorMessage, "error");
    }
  };

  return (
    <div className="bg-gray-50 text-gray-800 min-h-screen">
      <Header
        title="Sepetim"
        subtitle="Sepetinizdeki ürünleri görüntüleyin ve yönetin."
      />

      <div className="container mx-auto py-8 px-4">
        <div className="bg-white rounded-lg shadow-md p-6">
          <h2 className="text-2xl font-semibold text-gray-700 mb-4">
            Sepetim ({cartSize} Ürün)
          </h2>

          {cartSize === 0 ? (
            <p className="text-gray-500">Sepetinizde ürün bulunmamaktadır.</p>
          ) : (
            <table className="w-full text-left border-collapse">
              <thead>
                <tr className="text-gray-600 uppercase text-sm bg-gray-100">
                  <th className="py-3 px-6">Kapak Resmi</th>
                  <th className="py-3 px-6">Başlık</th>
                  <th className="py-3 px-6">Eğitmen</th>
                  <th className="py-3 px-6">Fiyat</th>
                  <th className="py-3 px-6">İşlemler</th>
                </tr>
              </thead>
              <tbody className="text-gray-700 text-sm font-light">
                {cart.map((item) => (
                  <tr
                    key={item.id}
                    className="border-b border-gray-200 hover:bg-gray-50"
                  >
                    <td className="py-3 px-6">
                      <img
                        src={item.coverImage}
                        alt={item.title}
                        className="w-20 h-16 object-cover rounded-lg"
                      />
                    </td>
                    <td className="py-3 px-6 font-semibold">{item.title}</td>
                    <td className="py-3 px-6">{item.teacher}</td>
                    <td className="py-3 px-6 font-medium">{item.price} TL</td>
                    <td className="py-3 px-6">
                      <MiniButton
                        text="Sil"
                        color="red"
                        onClick={() => removeFromCart(item.id)}
                      />
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
          {cartSize > 0 && (
            <div className="mt-6 flex justify-between">
              <MiniButton
                text="Sepeti Temizle"
                color="red"
                onClick={clearCart}
              />
              <MiniButton
                text="Sipariş Ver"
                color="purple"
                onClick={toggleOrderModal}
              />
            </div>
          )}
        </div>
      </div>

      {isOrderModalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-white rounded-lg shadow-lg overflow-hidden w-full max-w-md">
            <div className="p-4 border-b">
              <h3 className="text-lg font-semibold">Sipariş Bilgileri</h3>
            </div>
            <div className="p-4">
              <label className="block font-medium mb-2">Kart Numarası</label>
              <input
                type="text"
                value={cardNumber}
                onChange={(e) => setCardNumber(e.target.value)}
                placeholder="1234 5678 9101 1121"
                className="w-full border p-2 rounded-md mb-4"
              />
              <label className="block font-medium mb-2">
                Son Kullanma Tarihi
              </label>
              <input
                type="text"
                value={expiryDate}
                onChange={(e) => setExpiryDate(e.target.value)}
                placeholder="MM/YY"
                className="w-full border p-2 rounded-md mb-4"
              />
              <label className="block font-medium mb-2">CVV</label>
              <input
                type="text"
                value={cvv}
                onChange={(e) => setCvv(e.target.value)}
                placeholder="123"
                className="w-full border p-2 rounded-md mb-4"
              />
              {error && <p className="text-red-500 mb-4">{error}</p>}
            </div>
            <div className="flex justify-end space-x-4 p-4 border-t">
              <MiniButton text="İptal" onClick={toggleOrderModal} />
              <MiniButton
                text="Onayla"
                color="green"
                onClick={handleConfirmOrder}
              />
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default UserCart;
