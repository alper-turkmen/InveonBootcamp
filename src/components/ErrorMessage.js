import React from 'react';

const ErrorMessage = ({ message }) => {
  if (!message) return null;

  return <div className="text-red-500 text-center text-sm">{message}</div>;
};

export default ErrorMessage;