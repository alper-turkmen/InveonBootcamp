import React, { useState } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import Header from '../components/Header';
import Button from '../components/Button';
import TabButton from '../components/TabButton';
import MiniButton from '../components/MiniButton';
import InputField from '../components/InputField'; 
import axios from '../utils/axiosconf';
import { useSnackbar } from '../contexts/AlertContext';
import { API_URL } from '../consts/consts';
import { Link } from 'react-router-dom';

const ProfilePage = () => {
  const { user, logout, updateProfile } = useAuth(); 
  const [activeTab, setActiveTab] = useState(1); 

  const [firstName, setFirstName] = useState(user?.firstName || '');
  const [lastName, setLastName] = useState(user?.lastName || '');
  const [about, setAbout] = useState(user?.about || '');
  const [email, setEmail] = useState(user?.email || '');
  const [profileImage, setProfileImage] = useState(user?.profilePicture || '/default-profile.png');
  const [isEditing, setIsEditing] = useState(false); 
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const { addSnackbar } = useSnackbar();

  const isTeacher = user?.roles?.includes('Teacher'); 



  const handleImageUpload = (e) => {
    const file = e.target.files[0];

    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = async () => {
      const base64 = reader.result.split(',')[1];

      try {
        const response = await axios.post('/Account/profile/picture', {
          fileName: file.name,
          fileBase64: base64,
        });

        if (response.status === 200) {
          setProfileImage(response.data.fileUrl);
          addSnackbar('Profil resmi yüklendi', 'success');
        }
      } catch (err) {
        addSnackbar('Resim yüklenirken bir hata oluştu', 'error');
      }

    };
  };


  const handleSaveProfile = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      const response = await axios.put('/Account/profile', {
        name: firstName,
        surname: lastName,
        about: about,
        profileImage: profileImage
      });

      if (response.status === 200) {
        addSnackbar('Profil bilgileriniz güncellendi', 'success');
        setIsEditing(false);
        updateProfile(firstName, lastName, about);
      }
    } catch (err) {
      addSnackbar('Profil güncellenirken bir hata oluştu', 'error');
    } finally {
      setLoading(false);
    }
  };

  if (!user) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50 px-4 sm:px-6 lg:px-8">
        <div className="max-w-md w-full text-center space-y-8 bg-white p-8 rounded-lg shadow-md">
          <div>
            <h1 className="text-9xl font-bold text-purple-600">404</h1>
            <h2 className="text-2xl font-semibold text-gray-900 mt-4">
              Sayfa Bulunamadı
            </h2>
            <p className="mt-2 text-gray-600">
              Aradığınız sayfa mevcut değil
            </p>
          </div>
          
          <div className="mt-6">
            <Link
              to="/"
              className="inline-flex items-center justify-center px-5 py-3 border border-transparent text-base font-medium rounded-md text-white bg-purple-600 hover:bg-purple-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-purple-500"
            >
              Ana Sayfaya Dön
            </Link>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="bg-gray-50 text-gray-800 min-h-screen">
      <Header title="Profilim" subtitle="Profilinizi yönetin ve bilgilerinizi güncelleyin" />

      <div className="container mx-auto py-8 px-4">
        <div className="flex border-b mb-6">
          <TabButton onClick={() => setActiveTab(1)} isActive={activeTab === 1} label="Profil Bilgileri" />
        </div>

        <div className="bg-white rounded-lg shadow-md p-6">
          {activeTab === 1 && (
            <div>
              <h2 className="text-2xl font-semibold mb-4">Profil Bilgileri</h2>

              <div className="flex items-center space-x-4 mb-6">
                <div className="relative">
                  <img 
                    src={API_URL + profileImage}
                    alt="Profil Resmi" 
                    className="w-32 h-32 rounded-full border-4 border-purple-500 object-cover" 
                  />
                    <label htmlFor="file-upload" className="absolute bottom-0 right-0 bg-purple-600 p-2 rounded-full shadow-lg cursor-pointer">
                      <input 
                        id="file-upload" 
                        type="file" 
                        className="hidden" 
                        onChange={handleImageUpload}
                      />
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        strokeWidth={1.5}
                        stroke="white"
                        className="w-5 h-5"
                      >
                        <path strokeLinecap="round" strokeLinejoin="round" d="M15.232 5.232a3 3 0 014.243 4.243L7.5 21H3v-4.5l11.732-11.732z" />
                      </svg>
                    </label>
                </div>
              </div>

              <div className="space-y-4">
                <InputField
                  id="firstName"
                  type="text"
                  placeholder="Ad"
                  value={firstName}
                  onChange={(e) => setFirstName(e.target.value)}
                  rounded="md"
                  disabled={!isEditing} 
                />
                <InputField
                  id="lastName"
                  type="text"
                  placeholder="Soyad"
                  value={lastName}
                  onChange={(e) => setLastName(e.target.value)}
                  rounded="md"
                  disabled={!isEditing}
                />
                <InputField
                  id="email"
                  type="text"
                  placeholder="E-posta Adresi"
                  value={email}
                  rounded="md"
                  disabled
                />
                <textarea
                  id="about"
                  placeholder="Hakkımda"
                  value={about}
                  onChange={(e) => setAbout(e.target.value)}
                  disabled={!isEditing}
                  rows="4"
                  className={`mt-1 block w-full p-2 border rounded-md ${isEditing ? 'border-purple-500' : 'bg-gray-100'}`}
                ></textarea>

                <div className="flex space-x-4 mt-4">
                  {isEditing ? (
                    <>
                      <MiniButton text="Kaydet" onClick={handleSaveProfile} color="green" />
                      <MiniButton text="İptal" onClick={() => setIsEditing(false)} color="gray" />
                    </>
                  ) : (
                    <MiniButton text="Düzenle" onClick={() => setIsEditing(true)} />
                  )}
                </div>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default ProfilePage;