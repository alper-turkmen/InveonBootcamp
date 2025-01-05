import React from 'react';
import { Link } from 'react-router-dom';

const NotFound = () => {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 px-4 sm:px-6 lg:px-8">
      <div className="max-w-md w-full text-center space-y-8 bg-white p-8 rounded-lg shadow-md">
        <div>
          <h1 className="text-9xl font-bold text-purple-600">404</h1>
          <h2 className="text-2xl font-semibold text-gray-900 mt-4">
            Sayfa Bulunamadı
          </h2>
          <p className="mt-2 text-gray-600">
            Aradığınız sayfa mevcut değil
          </p>
        </div>
        
        <div className="mt-6">
          <Link
            to="/"
            className="inline-flex items-center justify-center px-5 py-3 border border-transparent text-base font-medium rounded-md text-white bg-purple-600 hover:bg-purple-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-purple-500"
          >
            Ana Sayfaya Dön
          </Link>
        </div>
      </div>
    </div>
  );
};

export default NotFound;
