import React from 'react';
import { Link } from 'react-router-dom';
import Footer from '../components/Footer';

const HomePage = () => {
  return (
    <div className="bg-gray-50 text-gray-800">
  


      <section className="bg-purple-600 text-white py-20">
        <div className="container mx-auto text-center">
          <h1 className="text-4xl font-bold">Hayallerine Ulaşmak İçin Kurslarını Keşfet</h1>
          <p className="mt-4 text-lg">Binlerce kurs arasından sana uygun olanı bul ve öğrenmeye başla.</p>
          <div className="mt-6 flex justify-center">
            <input
              type="text"
              placeholder="Kurs ara..."
              className="p-3 rounded-l-lg w-64 text-gray-800"
            />
            <button className="bg-white text-purple-600 px-6 py-3 rounded-r-lg hover:bg-gray-100">
              Ara
            </button>
          </div>
        </div>
      </section>

      <section className="py-16 bg-gray-100">
        <div className="container mx-auto text-center">
          <h2 className="text-3xl font-bold mb-12">Neden Bizi Seçmelisin?</h2>
          <div className="grid md:grid-cols-3 gap-8">
            <FeatureCard
              icon="🎓"
              title="Kaliteli Eğitim"
              description="Uzman eğitmenlerden kaliteli içeriklerle öğren."
            />
            <FeatureCard
              icon="🏆"
              title="Sertifikalar"
              description="Başarıyla tamamladığınız her kurs için sertifika alın."
            />
            <FeatureCard
              icon="⏰"
              title="Esnek Program"
              description="Her zaman ve her yerde öğrenme fırsatı."
            />
          </div>
        </div>
      </section>

      <section className="py-16">
        <div className="container mx-auto text-center">
          <h2 className="text-3xl font-bold mb-12">Popüler Kurslar</h2>
          <div className="grid md:grid-cols-3 gap-8">
            <CourseCard
              image="https://via.placeholder.com/400x300"
              title="Web Geliştirme"
              description="HTML, CSS ve JavaScript öğrenin."
            />
            <CourseCard
              image="https://via.placeholder.com/400x300"
              title="Python Programlama"
              description="Python ile programlamaya başlayın."
            />
            <CourseCard
              image="https://via.placeholder.com/400x300"
              title="Dijital Pazarlama"
              description="Pazarlama teknikleri ile ürünlerinizi tanıtın."
            />
          </div>
        </div>
      </section>

<Footer />
    </div>
  );
};


const FeatureCard = ({ icon, title, description }) => {
    return (
      <div className="p-8 bg-white rounded-lg shadow-lg">
        <div className="text-4xl">{icon}</div>
        <h3 className="text-xl font-bold mt-4">{title}</h3>
        <p className="mt-4 text-gray-600">{description}</p>
      </div>
    );
  };
  
  const CourseCard = ({ image, title, description }) => {
    return (
      <div className="bg-white rounded-lg shadow-lg p-6">
        <img src={image} alt={title} className="rounded-lg mb-4" />
        <h3 className="text-lg font-bold">{title}</h3>
        <p className="text-gray-600 mt-2">{description}</p>
        <button className="mt-4 bg-purple-600 text-white py-2 rounded-lg w-full">
          Detayları Gör
        </button>
      </div>
    );
  };
  
export default HomePage;