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


const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const { addSnackbar } = useSnackbar();

  const { user, token, login, logout } = useAuth();

  const [showPassword, setShowPassword] = useState(false);

  const navigate = useNavigate();

    const handleLogin = async (e) => {
      e.preventDefault();
  
      setLoading(true);
      setError('');
  
      try {
        const response = await axios.post('/Auth/login', {
          email,
          password,
        });
  
        login(response.data.user, response.data.token);

        addSnackbar(SITE_NAME + '\'ye hoşgeldiniz. Yönlendiriliyorsunuz...', 'success');


        setTimeout(() => {
          navigate('/');
        }, 3000);

  
      } catch (err) {
        setError(err.response.data.message);
        if (err.response.status === 401) {
          addSnackbar('E-posta veya şifre hatalı', 'error');
        }

      } finally {
        setLoading(false);
      }
    };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-md w-full space-y-8 bg-white p-8 rounded-lg shadow-md">
        <div>
          <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-700">
            Hesabınıza Giriş Yapın
          </h2>
        </div>
        <form className="mt-8 space-y-6" onSubmit={handleLogin}>
          <ErrorMessage message={error} />

          <div className="rounded-md shadow-sm -space-y-px">
            <InputField
              id="email"
              type="text"
              placeholder="E-posta Adresi"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              rounded="t-md"
            />

            <InputField
              id="password"
              type="password"
              placeholder="Şifre"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              showPassword={showPassword}
              setObsecure={() => setShowPassword(!showPassword)}
              rounded="b-md"
            />

          </div>

          <Button type="submit" text="Giriş Yap" />

          <div className="text-sm text-center">
            <Link to="/register" className="font-medium text-purple-600 hover:text-purple-500">
              Hesabınız yok mu? Kayıt olun
            </Link>
          </div>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;