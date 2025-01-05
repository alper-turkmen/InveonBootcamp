import React, { useState, useEffect } from 'react';

const VideoTitle = ({
  currentVideo,
  setCurrentVideo,
  setOpenTitleModal,
  openTitleModal,
  updateVideoTitle,
}) => {
  const [newTitle, setNewTitle] = useState('');
  const [error, setError] = useState(''); 

  useEffect(() => {
    if (currentVideo) {
      setNewTitle(currentVideo.title || ''); 
      setError(''); 
    }
  }, [currentVideo]); 

  if (!openTitleModal) return null; 

  const handleSave = () => {
    if (!newTitle.trim()) {
      setError('Lütfen video başlığını giriniz');
      return; 
    }

    updateVideoTitle(currentVideo.id, newTitle);
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div className="bg-white rounded-lg shadow-lg overflow-hidden w-full max-w-md">
        <div className="p-4 border-b">
          <h3 className="text-lg font-semibold">Video Başlığını Güncelle</h3>
          <input
            type="text"
            value={newTitle}
            onChange={(e) => setNewTitle(e.target.value)}
            className="w-full border p-2 rounded-md mt-4"
          />
          {error && <p className="text-red-600 text-sm mt-2">{error}</p>}
        </div>

        <div className="flex justify-end space-x-4 p-4 border-t">
          <button
            onClick={() => setOpenTitleModal(false)}
            className="px-4 py-2 bg-gray-300 rounded-md hover:bg-gray-400"
          >
            İptal
          </button>
          <button
            onClick={handleSave} 
            className="px-4 py-2 bg-green-600 text-white rounded-md hover:bg-green-700"
          >
            Kaydet
          </button>
        </div>
      </div>
    </div>
  );
};

export default VideoTitle;