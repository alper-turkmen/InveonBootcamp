import React, { createContext, useContext, useState } from 'react';

const SnackbarContext = createContext();

export const SnackbarProvider = ({ children }) => {
  const [snackbars, setSnackbars] = useState([]);

  const addSnackbar = (message, type = 'info', duration = 3000) => {
    const id = Math.random().toString(36).substr(2, 9); 
    setSnackbars((prev) => [...prev, { id, message, type }]);

    setTimeout(() => removeSnackbar(id), duration);
  };

  const removeSnackbar = (id) => {
    setSnackbars((prev) => prev.filter((snackbar) => snackbar.id !== id));
  };

  return (
    <SnackbarContext.Provider value={{ addSnackbar }}>
      {children}
      <div className="fixed inset-x-0  z-50 flex flex-col items-center space-y-0"
        style={{ top: '10%' }}

      >
        {snackbars.map((snackbar) => (
          <div
            key={snackbar.id}
            className={`flex items-center p-4 m-4 text-sm rounded-lg shadow-lg ${
              snackbar.type === 'success'
                ? 'bg-green-200 text-green-800'
                : snackbar.type === 'error'
                ? 'bg-red-200 text-red-800'
                : snackbar.type === 'warning'
                ? 'bg-yellow-200 text-yellow-800'
                : 'bg-blue-200 text-blue-800'
            }`}
          >
            <span className="mr-3">{snackbar.message}</span>
            <button
              className="ml-auto p-1 rounded-full bg-transparent hover:bg-gray-200"
              onClick={() => removeSnackbar(snackbar.id)}
            >
              <svg
                className="w-4 h-4"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 14 14"
              >
                <path
                  stroke="currentColor"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"
                />
              </svg>
            </button>
          </div>
        ))}
      </div>
    </SnackbarContext.Provider>
  );
};

export const useSnackbar = () => {
  return useContext(SnackbarContext);
};