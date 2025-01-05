import React, { useState } from 'react';

const VideoUploadModal = ({ isOpen, onClose, onUpload }) => {
  const [title, setTitle] = useState('');
  const [file, setFile] = useState(null);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false); 

  if (!isOpen) return null;

  const handleFileChange = (e) => {
    const selectedFile = e.target.files[0];
    setFile(selectedFile);
    setError('');
  };

  const handleUpload = () => {
    if (!title.trim()) {
      setError('Başlık boş olamaz!');
      return;
    }

    if (!file) {
      setError('Dosya seçilmelidir!');
      return;
    }

    setLoading(true);

    const reader = new FileReader();
    const fileName = file.name;
    reader.onload = () => {
      const base64String = reader.result.split(',')[1];
      onUpload(title, base64String, fileName)
        .then(() => {
          setLoading(false);
          onClose(); 
        })
        .catch(() => {
          setLoading(false);
          setError('Dosya yüklenirken bir hata oluştu!');
        });
    };

    reader.readAsDataURL(file);
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div className="bg-white rounded-lg shadow-lg overflow-hidden w-full max-w-md">
        <div className="p-4 border-b">
          <h3 className="text-lg font-semibold">Yeni Video Ekle</h3>
          <br />
          <label>Video Başlığı</label>
          <input
            type="text"
            placeholder="Video Başlığı"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            className="w-full border p-2 rounded-md mt-4"
          />
          <br />
          <br />
          <label>Video Dosyası</label>
          <br />
          <input
            type="file"
            accept="video/*"
            onChange={handleFileChange}
            className="w-full border p-2 rounded-md mt-4"
          />

          {error && <p className="text-red-600 text-sm mt-2">{error}</p>}
        </div>

        <div className="flex justify-end space-x-4 p-4 border-t">
          <button
            onClick={onClose}
            className="px-4 py-2 bg-gray-300 rounded-md hover:bg-gray-400"
            disabled={loading} 
          >
            İptal
          </button>
          <button
            onClick={handleUpload}
            className={`px-4 py-2 text-white rounded-md ${
              loading
                ? 'bg-gray-400 cursor-not-allowed' 
                : 'bg-green-600 hover:bg-green-700' 
            }`}
            disabled={loading} 
          >
            {loading ? ( 
              <div className="flex items-center space-x-2">
                <svg
                  className="animate-spin h-5 w-5 text-white"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                >
                  <circle
                    className="opacity-25"
                    cx="12"
                    cy="12"
                    r="10"
                    stroke="currentColor"
                    strokeWidth="4"
                  ></circle>
                  <path
                    className="opacity-75"
                    fill="currentColor"
                    d="M4 12a8 8 0 018-8v8z"
                  ></path>
                </svg>
                <span>Yükleniyor...</span>
              </div>
            ) : (
              'Ekle' 
            )}
          </button>
        </div>
      </div>
    </div>
  );
};

export default VideoUploadModal;