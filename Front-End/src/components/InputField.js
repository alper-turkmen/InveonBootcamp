import React from 'react';



const InputField = ({
  id,
  type = 'text',
  placeholder,
  value,
  onChange,
  rounded = 'none',
  showPassword = true,      
  setObsecure = () => {},
  disabled = false
}) => {
  const roundedClass =
    rounded === 't-md'
      ? 'rounded-t-md'
      : rounded === 'b-md'
      ? 'rounded-b-md'
      : rounded === 'md'
      ? 'rounded-md'
      : 'rounded-none';

  return (
    <div className="relative w-full">
      <input
        disabled={disabled}
        id={id}
        name={id}
        type={!showPassword ? 'password' : 'text'} 
        required
        className={`appearance-none ${roundedClass} relative block w-full px-3 py-3 border 
        border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none 
        focus:ring-purple-500 focus:border-purple-500 focus:z-10 sm:text-sm hover:bg-gray-50`}
        placeholder={placeholder}
        value={value}
        onChange={onChange}
      />

      {type=='password' && (
        <button
          type="button"
          onClick={() => setObsecure(!showPassword)} 
          className="absolute inset-y-0 right-0 flex items-center px-3 text-gray-500 hover:text-gray-700"
        >
          {!showPassword ? (
            <svg
              className="w-5 h-5"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M13.875 18.825A10.05 10.05 0 0112 19c-5.523 0-10-6-10-6s1.628-2.437 4.533-4.5M9.75 15a3 3 0 004.5-4.5M12 3C6.477 3 2 9 2 9s1.628 2.437 4.533 4.5"
              />
            </svg>
          ) : (
            <svg
              className="w-5 h-5"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M15 12A3 3 0 119 12a3 3 0 016 0zM2.458 12C3.732 7.943 7.523 5 12 5c4.477 0 8.268 2.943 9.542 7-.274 1.002-1.615 3.556-4.542 5.5-2.193 1.465-4.796 2-7.542 2-2.746 0-5.35-.535-7.542-2-2.927-1.944-4.268-4.498-4.542-5.5z"
              />
            </svg>
          )}
        </button>
      )}
    </div>
  );
};

export default InputField;