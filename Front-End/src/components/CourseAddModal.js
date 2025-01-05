import React, { useState } from "react";

const CourseAddModal = ({ isOpen, onClose, onAddCourse }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");
  const [coverImage, setCoverImage] = useState(null);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  if (!isOpen) return null;

  const handleFileChange = (e) => {
    const selectedFile = e.target.files[0];

    setCoverImage(selectedFile);
    setError("");
  };

  const handleAdd = () => {
    if (!title.trim()) {
      setError("Başlık boş olamaz!");
      return;
    }

    if (!description.trim()) {
      setError("Açıklama boş olamaz!");
      return;
    }

    if (!price || isNaN(price)) {
      setError("Geçerli bir fiyat giriniz!");
      return;
    }

    if (!coverImage) {
      setError("Kapak resmi seçilmelidir!");
      return;
    }

    setLoading(true);

    const reader = new FileReader();
    reader.onload = () => {
      const base64String = reader.result.split(",")[1];

      onAddCourse(title, description, base64String, price, coverImage.name)
        .then(() => {
          setLoading(false);
          onClose();
        })
        .catch(() => {
          setLoading(false);
          setError("Kurs eklenirken bir hata oluştu!");
        });
    };

    reader.readAsDataURL(coverImage);
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div className="bg-white rounded-lg shadow-lg overflow-hidden w-full max-w-md">
        <div className="p-4 border-b">
          <h3 className="text-lg font-semibold">Yeni Kurs Ekle</h3>

          <label className="block mt-4">Başlık</label>
          <input
            type="text"
            placeholder="Kurs Başlığı"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            className="w-full border p-2 rounded-md mt-1"
          />

          <label className="block mt-4">Açıklama</label>
          <textarea
            placeholder="Kurs Açıklaması"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            className="w-full border p-2 rounded-md mt-1"
            rows="3"
          ></textarea>

          <label className="block mt-4">Fiyat</label>
          <input
            type="number"
            placeholder="Fiyat"
            value={price}
            onChange={(e) => setPrice(e.target.value)}
            className="w-full border p-2 rounded-md mt-1"
          />

          <label className="block mt-4">Kapak Resmi</label>
          <input
            type="file"
            accept="image/*"
            onChange={handleFileChange}
            className="w-full border p-2 rounded-md mt-1"
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
            onClick={handleAdd}
            className={`px-4 py-2 text-white rounded-md ${
              loading
                ? "bg-gray-400 cursor-not-allowed"
                : "bg-green-600 hover:bg-green-700"
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
              "Ekle"
            )}
          </button>
        </div>
      </div>
    </div>
  );
};

export default CourseAddModal;
