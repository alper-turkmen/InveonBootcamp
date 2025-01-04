import React from 'react';

const QuestionModal = ({ isOpen, onClose, onConfirm, message }) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div className="bg-white rounded-lg shadow-lg overflow-hidden w-full max-w-md">
        <div className="p-4 border-b">
          <h3 className="text-lg font-semibold">Onayla</h3>
        </div>
        <div className="p-4">
          <p>{message}</p>
        </div>
        <div className="flex justify-end space-x-4 p-4 border-t">
          <button
            onClick={onClose}
            className="px-4 py-2 bg-gray-300 rounded-md hover:bg-gray-400"
          >
            İptal
          </button>
          <button
            onClick={onConfirm}
            className="px-4 py-2 bg-red-600 text-white rounded-md hover:bg-red-700"
          >
            Onayla
          </button>
        </div>
      </div>
    </div>
  );
};

export default QuestionModal;