import React from 'react';
import { Link } from 'react-router-dom';
import { SITE_NAME } from '../consts/consts';

const Navbar = () => {
  const categories = [
    "Yazılım Geliştirme",
    "Veri Bilimi",
    "Tasarım",
    "Pazarlama",
    "İş Yönetimi"
  ];

  return (
    <nav className="bg-white shadow-md">
      <div className="container mx-auto px-6 py-4 flex justify-between items-center">
        <Link to="/" className="text-2xl font-bold text-purple-600 
        bg-clip-text text-transparent bg-gradient-to-b from-purple-400 to-purple-900">
          {SITE_NAME}
        </Link>
        <div className="hidden md:flex space-x-6">
          <div className="relative group">
            <a href="#" className="text-gray-700 hover:text-purple-600">
              Kategoriler
            </a>
            <div className="absolute left-0 mt-2 w-48 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 
                          invisible group-hover:visible transition-all duration-200 opacity-0 group-hover:opacity-100">
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
          <Link to="/courses" className="text-gray-700 hover:text-purple-600">Popüler Kurslar</Link>
          <Link to="/about" className="text-gray-700 hover:text-purple-600">Hakkımızda</Link>
        </div>
        <div className="hidden md:flex space-x-4">
          <Link 
            to="/login" 
            className="text-gray-700 hover:text-purple-600 px-4 py-2 flex items-center"
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
        </div>
      </div>
    </nav>
  );
};

export default Navbar;