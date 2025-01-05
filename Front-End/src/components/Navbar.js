import React from "react";
import { Link } from "react-router-dom";
import { SITE_NAME, API_URL } from "../consts/consts";
import { useAuth } from "../contexts/AuthContext";
import { useCart } from "../contexts/CartContext";
import { FaShoppingCart } from "react-icons/fa";

const Navbar = () => {
  const { user, logout } = useAuth();
  const { cart, cartSize } = useCart();

  const categories = [];

  {
    /*
    
    <div className="relative group">
              <a href="#" className="text-gray-700 hover:text-purple-600">
                Kategoriler
              </a>
              <div
                className="absolute left-0 mt-2 w-48 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 
                                invisible group-hover:visible transition-all duration-200 opacity-0 group-hover:opacity-100"
              >
                <div className="py-1">
                  {categories.map((category, index) => (
                    <a
                      key={index}
                      href="#"
                      className="block px-4 py-2 text-sm text-gray-700 hover:bg-purple-50 hover:text-purple-600"
                    >
                      {category}
                    </a>
                  ))}
                </div>
              </div>
            </div>
            <Link to="/courses" className="text-gray-700 hover:text-purple-600">
              Popüler Kurslar
            </Link>
    */
  }
  return (
    <nav className="bg-white shadow-md">
      <div className="container mx-auto px-6 py-4 flex justify-between items-center">
        <div className="flex items-center space-x-6">
          <Link
            to="/"
            className="text-2xl font-bold text-purple-600 
          bg-clip-text text-transparent bg-gradient-to-b from-purple-400 to-purple-900"
          >
            {SITE_NAME}
          </Link>

          <div className="hidden md:flex space-x-6">
            <Link to="/about" className="text-gray-700 hover:text-purple-600">
              Hakkımızda
            </Link>
          </div>
        </div>

        <div className="hidden md:flex space-x-2 items-center">
          {user ? (
            <>
              <div className="flex items-center space-x-2">
                <Link
                  to="/profile"
                  className="flex items-center space-x-3 text-gray-700 hover:text-purple-600 px-2 py-2"
                >
                  {user.profilePicture && (
                    <img
                      src={API_URL + user.profilePicture}
                      alt="Profil Resmi"
                      className="w-10 h-10 rounded-full border-2 border-purple-500 object-cover"
                    />
                  )}
                  <span>Merhaba, {user.firstName}</span>
                </Link>
              </div>

              {user.roles && user.roles.includes("Teacher") && (
                <Link
                  to="/teacher-dashboard"
                  className="text-gray-700 hover:text-purple-600 px-2 py-2"
                >
                  Eğitmen Alanı
                </Link>
              )}

              {user.roles && user.roles.includes("User") && (
                <>
                  <Link
                    to="/user-dashboard"
                    className="text-gray-700 hover:text-purple-600 px-2 py-2"
                  >
                    Kurslarım
                  </Link>

                  <div className="relative">
                    <Link
                      to="/cart"
                      className="text-gray-700 hover:text-purple-600 py-0.5 mx-3 relative"
                    >
                      <FaShoppingCart size={26} />
                    </Link>
                    {cartSize > 0 && (
                      <span className="absolute top-0.5 -right-4 bg-red-500 text-white text-xs rounded-full px-1.5 py-0.5 flex items-center justify-center">
                        {cartSize}
                      </span>
                    )}
                  </div>
                </>
              )}

              <button
                onClick={logout}
                className="text-gray-700 hover:text-purple-600 px-2 py-4 ml-2"
              >
                Çıkış Yap
              </button>
            </>
          ) : (
            <>
              <Link
                to="/login"
                className="text-gray-700 hover:text-purple-600 px-2 py-2 flex items-center"
              >
                Giriş Yap
              </Link>
              <Link
                to="/register"
                className="bg-purple-600 text-white px-4 py-2 rounded-md hover:bg-purple-700 
                bg-gradient-to-b from-purple-400 to-purple-700"
              >
                Kayıt Ol
              </Link>
            </>
          )}
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
