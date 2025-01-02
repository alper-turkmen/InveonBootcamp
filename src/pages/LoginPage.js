import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();


  return (
    <div>
      <h1>Giriş Yap</h1>
      <input type="email" placeholder="E-posta" value={email} onChange={(e) => setEmail(e.target.value)} />
      <input type="password" placeholder="Şifre" value={password} onChange={(e) => setPassword(e.target.value)} />
      <button onClick={null}>Giriş Yap</button>
    </div>
  );
};

export default LoginPage;