import React from "react";
import { FaCartShopping } from "react-icons/fa6";
import { useCart } from "../contexts/CartContext";
import { useAuth } from "../contexts/AuthContext";

const CourseCard = ({
  id,
  coverImage,
  title,
  description,
  teacher,
  price,
  onDetailClick,
}) => {
  const { addToCart } = useCart();
  const { user } = useAuth();

  const handleAddToCart = () => {
    addToCart({ id, title, price, teacher, coverImage });
  };

  return (
    <div className="bg-white rounded-lg mx-5 shadow-lg p-6">
      <div className="relative">
        <img src={coverImage} alt={title} className="rounded-lg mb-4 w-full" />
        {user && user.roles.includes("User") && (
          <button
            onClick={handleAddToCart}
            className="absolute top-2 right-2 bg-purple-500 text-white p-2 rounded-lg hover:bg-purple-700 flex items-center gap-2"
          >
            <FaCartShopping size={20} />
            Sepete Ekle
          </button>
        )}
      </div>
      <h3 className="text-lg font-bold">{title}</h3>
      <p className="text-gray-600 mt-2">{description}</p>
      <p className="text-gray-600 mt-2">Eğitmen: {teacher}</p>
      <p className="text-gray-800 font-semibold mt-2">{price} TL</p>
      <div className="mt-4">
        <button
          onClick={onDetailClick}
          className="w-full bg-purple-500 text-white py-2 px-4 rounded-lg hover:bg-purple-700"
        >
          Detayları Gör
        </button>
      </div>
    </div>
  );
};

export default CourseCard;
