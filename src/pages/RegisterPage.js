import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import InputField from '../components/InputField';
import Button from '../components/Button';
import ErrorMessage from '../components/ErrorMessage';
import axios from '../utils/axiosconf';
import { useSnackbar } from '../contexts/AlertContext';
import { useAuth } from '../contexts/AuthContext';
import { SITE_NAME } from '../consts/consts';


const RegisterPage = () => {

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  
  const [error, setError] = useState('');
  const { addSnackbar } = useSnackbar();

  const { user, token, login, logout } = useAuth();

  const [showPassword, setShowPassword] = useState(false);

  const navigate = useNavigate();

    const handleLogin = async (e) => {
      e.preventDefault();
  
      setLoading(true);
      setError('');
  
      try {
        const response = await axios.post('/Auth/register', {
          username: email,
          email: email,
          password: password,
          name: name,
          surname: surname
        });
  

        addSnackbar(SITE_NAME + '\'ye hoşgeldiniz. Yönlendiriliyorsunuz...', 'success');


        setTimeout(() => {
          navigate('/login');
        }, 3000);

  
      } catch (err) {
        setError(err.response.data.message);
        
        addSnackbar('Kayıt olma işlemi başarısız', 'error');

      } finally {
        setLoading(false);
      }
    };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-md w-full space-y-8 bg-white p-8 rounded-lg shadow-md">
        <div>
          <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-700">
            Kayıt Ol
          </h2>
        </div>
        <form className="mt-8 space-y-6" onSubmit={handleLogin}>
          <ErrorMessage message={error} />

          <div className="rounded-md shadow-sm -space-y-px">

            <InputField
              id="name"
              type="text"
              placeholder="Ad"
              value={name}
              onChange={(e) => setName(e.target.value)}
              rounded="md"
            />
<br/>
            <InputField
              id="surname"
              type="text"
              placeholder="Soyad"
              value={surname}
              onChange={(e) => setSurname(e.target.value)}
              rounded="md"
            />
<br/>

            <InputField
              id="email"
              type="text"
              placeholder="E-posta Adresi"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              rounded="md"
            />
<br/>

            <InputField
              id="password"
              type="password"
              placeholder="Şifre"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              showPassword={showPassword}
              setObsecure={() => setShowPassword(!showPassword)}
              rounded="md"
            />

          </div>

          <Button type="submit" text="Giriş Yap" />

          <div className="text-sm text-center">
            <Link to="/login" className="font-medium text-purple-600 hover:text-purple-500">
              Hesabınız var mı? Giriş yapın
            </Link>
          </div>
        </form>
      </div>
    </div>
  );
};

export default RegisterPage;