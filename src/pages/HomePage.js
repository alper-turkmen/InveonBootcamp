import React from 'react';
import { Link } from 'react-router-dom';
import Footer from '../components/Footer';
import CourseCard from '../components/CourseCard';
import FeatureCard from '../components/FeatureCard';
import { SITE_NAME } from '../consts/consts';

const HomePage = () => {
  return (
    <div className="bg-gray-50 text-gray-800">
      {/* Hero Section */}
      <section className="bg-purple-600 text-white py-16 bg-gradient-to-b from-purple-400 to-purple-700">
        <div className="container mx-auto text-center px-4">
          <h1 className="text-4xl font-bold mb-4">Hayallerine Ulaşmak İçin Kurslarını Keşfet</h1>
          <p className="text-lg mb-6">Binlerce kurs arasından sana uygun olanı bul ve öğrenmeye başla</p>
          <div className="flex justify-center items-center">
            <input
              type="text"
              placeholder="Kurs ara..."
              className="p-3 rounded-l-lg w-64 text-gray-800 focus:outline-none"
            />
            <button className="bg-white text-purple-600 px-6 py-3 rounded-r-lg hover:bg-gray-100">
              Ara
            </button>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section className="py-10 bg-gray-100 mb-4">
        <div className="container mx-auto text-center px-4">
          <h2 className="text-3xl text-gray-700 font-bold mb-10">Neden {SITE_NAME}?</h2>
          <div className="grid md:grid-cols-3 gap-8">
            <FeatureCard
              icon="🎓"
              title="Kaliteli Eğitim"
              description="Uzman eğitmenlerden kaliteli içeriklerle öğren"
            />
            <FeatureCard
              icon="🏆"
              title="Sertifikalar"
              description="Başarıyla tamamladığın her kurs için sertifika al"
            />
            <FeatureCard
              icon="⏰"
              title="Esnek Program"
              description="Her zaman ve her yerde öğrenme fırsatı"
            />
          </div>
        </div>
      </section>

      {/* Courses Section */}
      <section className="py-10 bg-white">
        <div className="container mx-auto text-center px-4">
          <h2 className="text-3xl font-bold mb-2">Popüler Kurslar</h2>
          <div className="grid md:grid-cols-3 gap-8">
            <CourseCard
              image="https://img-c.udemycdn.com/course/480x270/5465452_41c0_2.jpg"
              title=".NET Core ile Web API Geliştirme"
              description="Web API geliştirme üzerine temel bilgiler"
              instructor="Ahmet Mehmet"
            />
            <CourseCard
              image="https://img-c.udemycdn.com/course/480x270/5512768_ffa4_29.jpg"
              title="React Programlama"
              description="React ile modern web uygulamaları geliştirme"
              instructor="Mehmet Ahmet"
            />
            <CourseCard
              image="https://img-c.udemycdn.com/course/750x422/1565838_e54e_18.jpg"
              title="Dijital Pazarlama"
              description="Pazarlama teknikleri ile ürünlerinizi tanıtın"
              instructor="Ahmet Mehmet"
            />
          </div>
        </div>
      </section>
    </div>
  );
};

export default HomePage;