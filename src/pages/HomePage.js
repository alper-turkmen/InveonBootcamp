import React from 'react';
import { Link } from 'react-router-dom';
import Footer from '../components/Footer';
import CourseCard from '../components/CourseCard';
import FeatureCard from '../components/FeatureCard';

const HomePage = () => {
  return (
    <div className="bg-gray-50 text-gray-800">
      <section className="bg-purple-600 text-white py-20 bg-gradient-to-b from-purple-400 to-purple-900
      ">
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
              image="https://media.licdn.com/dms/image/D5612AQGRK8Flx6IFKQ/article-cover_image-shrink_600_2000/0/1710255368732?e=2147483647&v=beta&t=qhz-UCUAFHJ6Ha0cL7CAHk-DkswbwlreVOSo-Xn35Ik"
              title="Web Geliştirme"
              description="HTML, CSS ve JavaScript öğrenin."
              instructor="Ahmet Mehmet"
            />
            <CourseCard
              image="https://i.ytimg.com/vi/Ho11V8WOrkw/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLAfIGnWZNyV79vk7TEUXIDV3SSv3g"
              title="Python Programlama"
              description="Python ile programlamaya başlayın."
              instructor="Mehmet Ahmet"
            />
            <CourseCard
              image="https://img-c.udemycdn.com/course/750x422/1565838_e54e_18.jpg"
              title="Dijital Pazarlama"
              description="Pazarlama teknikleri ile ürünlerinizi tanıtın."
              instructor="Ahmet Mehmet"
            />
          </div>
        </div>
      </section>

    </div>
  );
};



  
export default HomePage;