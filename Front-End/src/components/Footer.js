import React from 'react';
import { Link } from 'react-router-dom';
import { SITE_NAME } from '../consts/consts';


const Footer = () => {

  return (
    <footer className="bg-gray-800 text-white py-6">
     

    <div className="flex justify-center items-center container mx-auto">
    <div className="text-xl font-bold text-purple-600 container mx-auto text-center
            bg-clip-text text-transparent bg-gradient-to-b from-purple-400 to-purple-900">
      &copy; 2025 {SITE_NAME}
    </div>
    </div>


     
    </footer>
  );
};

export default Footer;



