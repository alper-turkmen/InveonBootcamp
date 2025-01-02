import React from 'react';

const Button = ({ type = 'button', text, onClick }) => {
  return (
    <button
      type={type}
      onClick={onClick}
      className="group relative w-full flex justify-center py-2 px-4 border border-transparent 
      text-sm font-medium rounded-md text-white bg-purple-600 hover:bg-purple-700 
      focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-purple-500"
    >
      {text}
    </button>
  );
};

export default Button;