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
        <a href="#" className="text-2xl font-bold text-purple-600">
          {SITE_NAME}
        </a>
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
          <a href="#" className="text-gray-700 hover:text-purple-600">Popüler Kurslar</a>
          <a href="#" className="text-gray-700 hover:text-purple-600">Hakkımızda</a>
        </div>
        <div className="hidden md:flex space-x-4">
          <a href="#" className="text-gray-700 hover:text-purple-600 px-4 py-2 flex items-center">
            Giriş Yap
          </a>
          <a href="#" className="bg-purple-600 text-white px-4 py-2 rounded-md hover:bg-purple-700">
            Kayıt Ol
          </a>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;