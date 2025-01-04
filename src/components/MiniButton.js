import React from 'react';

const MiniButton = ({
  type = 'button',
  text,
  onClick,
  color = 'purple' 
}) => {
  const baseColor = color === 'purple' ? 'bg-purple-600 hover:bg-purple-700 focus:ring-purple-500' :
                    color === 'blue' ? 'bg-blue-600 hover:bg-blue-700 focus:ring-blue-500' :
                    color === 'green' ? 'bg-green-600 hover:bg-green-700 focus:ring-green-500' :
                    color === 'red' ? 'bg-red-600 hover:bg-red-700 focus:ring-red-500' :
                    'bg-gray-600 hover:bg-gray-700 focus:ring-gray-500';

  return (
    <button
      type={type}
      onClick={onClick}
      className={`group  flex justify-center py-2 px-4 border border-transparent 
      text-sm font-medium rounded-md text-white ${baseColor} 
      focus:outline-none focus:ring-2 focus:ring-offset-2`}
    >
      {text}
    </button>
  );
};

export default MiniButton;