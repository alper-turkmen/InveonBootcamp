import React from 'react';

const VideoWindow = ({ isOpen, onClose, videoUrl, title }) => {
  if (!isOpen) return null;


  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div className="bg-white rounded-lg shadow-lg overflow-hidden w-full max-w-6xl">
        <div className="flex justify-between items-center p-4 border-b">
          <h3 className="text-lg font-semibold">{title}</h3>
          <button onClick={onClose} className="text-red-600 hover:text-red-800">
            Kapat
          </button>
        </div>

        <div className="p-4">
          <video controls className="w-full rounded-lg">
            <source src={videoUrl} type="video/mp4" />
            Tarayıcınız video elementini desteklemiyor.
          </video>
        </div>
      </div>
    </div>
  );
};

export default VideoWindow;