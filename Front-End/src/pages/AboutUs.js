import React from "react";
import Header from "../components/Header";
import { SITE_NAME } from "../consts/consts";

const AboutUs = () => {
  return (
    <div className="bg-gray-50 text-gray-800 min-h-screen">
      <Header title="Hakkımızda" subtitle="Biz Kimiz ve Ne Yapıyoruz?" />

      <div className="container mx-auto py-8 px-4">
        <section className="bg-white shadow-md rounded-lg p-6 mb-8">
          <h2 className="text-2xl font-semibold text-gray-700 mb-4">
            {SITE_NAME} Hakkında
          </h2>
          <p className="text-gray-600 leading-relaxed">
            {SITE_NAME}, kaliteli eğitim içerikleri sunarak öğrencilerin ve
            profesyonellerin kariyerlerinde ilerlemelerine yardımcı olmayı
            hedefleyen bir eğitim platformu. Amacımız, öğrenmeyi daha
            erişilebilir, eğlenceli ve etkili hale getirmektir.
          </p>
        </section>

        <section className="grid md:grid-cols-2 gap-8">
          <div className="bg-white shadow-md rounded-lg p-6">
            <h3 className="text-xl font-semibold text-gray-700 mb-3">
              Misyonumuz
            </h3>
            <p className="text-gray-600 leading-relaxed">
              Eğitimde fırsat eşitliği sağlamak ve herkesin potansiyelini ortaya
              çıkarması için yüksek kaliteli eğitim kaynakları sunmak.
            </p>
          </div>

          <div className="bg-white shadow-md rounded-lg p-6">
            <h3 className="text-xl font-semibold text-gray-700 mb-3">
              Vizyonumuz
            </h3>
            <p className="text-gray-600 leading-relaxed">
              Teknolojiyi kullanarak öğrenme süreçlerini dönüştürmek ve dünya
              çapında milyonlarca insanın hayatına dokunmak.
            </p>
          </div>
        </section>

        <section className="bg-white shadow-md rounded-lg p-6 mt-8">
          <h3 className="text-2xl font-semibold text-gray-700 mb-4">
            Bize Ulaşın
          </h3>
          <p className="text-gray-600 leading-relaxed">
            Sorularınız veya önerileriniz için bizimle iletişime geçebilirsiniz.
          </p>
          <p className="text-gray-600 mt-4">
            📧 E-posta:{" "}
            <a className="text-purple-600 hover:underline">
              bilgi@acikakademi.com
            </a>
          </p>
          <p className="text-gray-600">
            📞 Telefon:{" "}
            <a
              href="tel:+900123456789"
              className="text-purple-600 hover:underline"
            >
              +90 123 456 78 90
            </a>
          </p>
        </section>
      </div>
    </div>
  );
};

export default AboutUs;
