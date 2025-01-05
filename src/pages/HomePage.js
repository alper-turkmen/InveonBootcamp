import React, { useState, useEffect, useRef } from "react";
import axios from "../utils/axiosconf";
import { API_URL, SITE_NAME } from "../consts/consts";
import CourseCard from "../components/CourseCard";
import FeatureCard from "../components/FeatureCard";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import { FaChevronLeft, FaChevronRight } from "react-icons/fa";
import kepIcon from "../kep.png";
import alarmIcon from "../alarm.png";
import kupaIcon from "../kupa.png";
import CourseModal from "../components/CourseModal";

const HomePage = () => {
  const [searchTerm, setSearchTerm] = useState("");
  const [courses, setCourses] = useState([]);
  const [popularCourses, setPopularCourses] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [searchPerformed, setSearchPerformed] = useState(false);

  const [selectedCourse, setSelectedCourse] = useState(null);

  const sliderRef = useRef();

  useEffect(() => {
    const fetchPopularCourses = async () => {
      setLoading(true);
      try {
        const response = await axios.get("/Course/all?size=20&page=1");
        setPopularCourses(response.data.data);
      } catch (err) {
        setError("Popüler kurslar yüklenirken bir hata oluştu.");
      } finally {
        setLoading(false);
      }
    };
    fetchPopularCourses();
  }, []);

  const handleSearch = async () => {
    if (searchPerformed && !searchTerm.trim()) {
      setSearchPerformed(false);
      return;
    }
    if (!searchTerm.trim()) {
      return;
    }

    setLoading(true);
    setError("");
    try {
      const response = await axios.get(`/Course/all?name=${searchTerm}`);
      setCourses(response.data.data);
      setSearchPerformed(true);
    } catch (err) {
      setError("Arama sırasında bir hata oluştu.");
    } finally {
      setLoading(false);
    }
  };

  const sliderSettings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 3,
    slidesToScroll: 1,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 2,
        },
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
        },
      },
    ],
  };

  return (
    <div className="bg-gray-50 text-gray-800">
      <section className="bg-purple-600 text-white py-16 bg-gradient-to-b from-purple-400 to-purple-700">
        <div className="container mx-auto text-center px-4">
          <h1 className="text-4xl font-bold mb-4">
            Hayallerine Ulaşmak İçin Kurslarını Keşfet
          </h1>
          <p className="text-lg mb-6">
            Binlerce kurs arasından sana uygun olanı bul ve öğrenmeye başla
          </p>
          <div className="flex justify-center items-center">
            <input
              type="text"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              placeholder="Kurs ara..."
              className="p-3 rounded-l-lg w-64 text-gray-800 focus:outline-none"
            />
            <button
              onClick={handleSearch}
              className="bg-white text-purple-600 px-6 py-3 rounded-r-lg hover:bg-gray-100"
            >
              Ara
            </button>
          </div>
        </div>
      </section>

      <section className="py-12 bg-gray-100">
        <div className="container mx-auto text-center px-4">
          <h2 className="text-3xl text-gray-700 font-bold mb-10">
            Neden {SITE_NAME}?
          </h2>
          <div className="grid md:grid-cols-3 gap-8">
            <FeatureCard
              icon={kepIcon}
              title="Kaliteli Eğitim"
              description="Uzman eğitmenler tarafından hazırlanan kaliteli içerikler"
            />
            <FeatureCard
              icon={kupaIcon}
              title="Sertifikalar"
              description="Tamamladığınız kurslar için sertifika alma imkanı"
            />
            <FeatureCard
              icon={alarmIcon}
              title="Esnek Program"
              description="İstediğiniz zaman, istediğiniz yerde öğrenme imkanı"
            />
          </div>
        </div>
      </section>

      {!searchPerformed && (
        <section className="py-10 px-10 bg-white relative">
          <div className="container mx-auto text-center px-4">
            <h2 className="text-3xl font-bold mb-2">Popüler Kurslar</h2>
            {loading ? (
              <p className="text-gray-500">Yükleniyor...</p>
            ) : error ? (
              <p className="text-red-500">{error}</p>
            ) : (
              <div className="relative py-10 px-10">
                <FaChevronLeft
                  onClick={() => sliderRef.current.slickPrev()}
                  className="absolute left-0 top-1/2 transform -translate-y-1/2 z-10 text-purple-600 hover:text-purple-800 cursor-pointer pr-4"
                  size={32}
                />
                <Slider ref={sliderRef} {...sliderSettings}>
                  {popularCourses.map((course) => (
                    <CourseCard
                      key={course.id}
                      id={course.id}
                      price={course.price}
                      coverImage={API_URL + course.coverImage}
                      title={course.title}
                      description={course.description}
                      teacher={course.teacher}
                      videos={course.videos}
                      onDetailClick={() => setSelectedCourse(course)}
                    />
                  ))}
                </Slider>
                <FaChevronRight
                  onClick={() => sliderRef.current.slickNext()}
                  className="absolute right-0 top-1/2 transform -translate-y-1/2 z-10  text-purple-600 hover:text-purple-800 cursor-pointer pl-4"
                  size={32}
                />
              </div>
            )}
          </div>
        </section>
      )}

      {searchPerformed && (
        <section className="py-10 bg-gray-100">
          <div className="container mx-auto text-center px-4">
            <h2 className="text-3xl font-bold mb-2">Arama Sonuçları</h2>
            {loading ? (
              <p className="text-gray-500">Yükleniyor...</p>
            ) : error ? (
              <p className="text-red-500">{error}</p>
            ) : courses.length === 0 ? (
              <p className="text-gray-500">Arama sonucu bulunamadı.</p>
            ) : (
              <div className="grid md:grid-cols-3 gap-8">
                {courses.map((course) => (
                  <CourseCard
                    key={course.id}
                    id={course.id}
                    price={course.price}
                    coverImage={API_URL + course.coverImage}
                    title={course.title}
                    description={course.description}
                    teacher={course.teacher}
                    videos={course.videos}
                    onDetailClick={() => setSelectedCourse(course)}
                  />
                ))}
              </div>
            )}
          </div>
        </section>
      )}
      {selectedCourse && (
        <CourseModal
          course={selectedCourse}
          onClose={() => setSelectedCourse(null)}
        />
      )}
    </div>
  );
};

export default HomePage;
