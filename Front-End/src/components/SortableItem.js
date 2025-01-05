import React from 'react';
import { useSortable } from '@dnd-kit/sortable';
import { CSS } from '@dnd-kit/utilities';
import QuestionModal from './QuestionModal';
import { useState } from 'react';

const SortableItem = ({ id, title, onDelete, onWatch, onUpdate }) => {
  const { attributes, listeners, setNodeRef, transform, transition } = useSortable({ id });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
  };

  const [isModalOpen, setIsModalOpen] = useState(false);

  const handleConfirmDelete = () => {
    onDelete(); 
    setIsModalOpen(false); 
  };

  return (
    <div
      ref={setNodeRef}
      style={style}
      className="bg-white p-4 rounded-lg mb-4 flex justify-between items-center border"
    >
      <div {...attributes} {...listeners} className="cursor-move flex-grow text-gray-800 font-medium">
        {title}
      </div>

      <div className="flex space-x-4">
        <button
          onClick={(e) => {
            e.stopPropagation(); 
            onWatch(); 
          }}
          draggable="false"
          className="text-green-800 hover:text-green-900"
        >
          Önizleme
        </button>

        <button
          onClick={(e) => {
            e.stopPropagation(); 
            onUpdate(); 
          }}
          draggable="false"
          className="text-orange-800 hover:text-orange-900"
        >
          Başlığı Düzenle
        </button>

        

        <button
          onClick={(e) => {
            e.stopPropagation(); 
            setIsModalOpen(true); 
          }}
          draggable="false"
          className="text-red-800 hover:text-red-900"
        >
          Sil
        </button>
      </div>

      <QuestionModal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onConfirm={handleConfirmDelete}
        message="Bu videoyu silmek istediğinize emin misiniz?"
      />
    </div>
  );
};

export default SortableItem;