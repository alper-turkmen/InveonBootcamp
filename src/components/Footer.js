import React from 'react';
import { Link } from 'react-router-dom';
import { SITE_NAME } from '../consts/consts';


const Footer = () => {

  return (
    <footer className="bg-gray-900 text-white py-6">
    <div className="container mx-auto text-center">
      &copy; 2025 {SITE_NAME}. Tüm Hakları Saklıdır.
    </div>
    </footer>
  );
};

export default Footer;



