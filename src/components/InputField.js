import React from 'react';

const InputField = ({
  id,
  type = 'text',
  placeholder,
  value,
  onChange,
  rounded = 'none',
}) => {
  return (
    <input
      id={id}
      name={id}
      type={type}
      required
      className={`appearance-none rounded-${rounded} relative block w-full px-3 py-3 border 
      border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none 
      focus:ring-purple-500 focus:border-purple-500 focus:z-10 sm:text-sm hover:bg-gray-50`}
      placeholder={placeholder}
      value={value}
      onChange={onChange}
    />
  );
};

export default InputField;