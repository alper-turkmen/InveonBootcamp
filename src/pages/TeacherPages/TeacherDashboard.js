import React, { useState } from "react";
import TabButton from "../../components/TabButton";
import Header from "../../components/Header";
import axios from "../../utils/axiosconf";
import { useAuth } from "../../contexts/AuthContext";
import { useEffect } from "react";
import { addSnackbar, useSnackbar } from "../../contexts/AlertContext";
import { API_URL } from "../../consts/consts";
import MiniButton from "../../components/MiniButton";
import { useNavigate } from "react-router-dom";
import QuestionModal from "../../components/QuestionModal";
import CourseAddModal from "../../components/CourseAddModal";

const TeacherDashboard = () => {
  const [activeTab, setActiveTab] = useState(1);
  const { token } = useAuth();
  const { user } = useAuth();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [selectedCourseId, setSelectedCourseId] = useState(null);

  const navigate = useNavigate();

  const { addSnackbar } = useSnackbar();

  const [myCourses, setMyCourses] = useState([]);

  const getMyCourses = async () => {
    setLoading(true);
    setError("");

    try {
      const response = await axios.get("/Course", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      setMyCourses(response.data);
    } catch (err) {
      setError(err.response.data.message);

      if (err.response.status === 401) {
        addSnackbar("E-posta veya şifre hatalı", "error");
      }
    }
  };

  const [orders, setOrders] = useState([]);
  const [loadingOrders, setLoadingOrders] = useState(false);
  const [errorOrders, setErrorOrders] = useState("");

  const getOrders = async () => {
    setLoadingOrders(true);
    setErrorOrders("");

    try {
      const response = await axios.get("/Orders/teacher", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      setOrders(response.data);
    } catch (err) {
      setErrorOrders(err.response?.data?.message || "Siparişler yüklenemedi.");
      addSnackbar("Siparişler yüklenirken bir hata oluştu", "error");
    } finally {
      setLoadingOrders(false);
    }
  };

  useEffect(() => {
    if (activeTab === 2) {
      getOrders();
    }
  }, [activeTab]);

  useEffect(() => {
    getMyCourses();
  }, []);

  const handleConfirmDelete = async () => {
    if (!selectedCourseId) return;

    try {
      await axios.delete(`/Course/${selectedCourseId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      addSnackbar("Kurs başarıyla silindi", "success");
      getMyCourses();
      setIsModalOpen(false);
    } catch (err) {
      addSnackbar("Kurs silinirken bir hata oluştu", "error");
    } finally {
      setSelectedCourseId(null);
    }
    setIsDeleteModalOpen(false);
  };

  const handleAddCourse = async (
    title,
    description,
    coverImage,
    price,
    coverImageName
  ) => {
    const response = await axios.post(
      "/Course",
      {
        title: title,
        description: description,
        coverImage: coverImage,
        coverImageName: coverImageName,
        price: price,
        videos: [],
      },
      {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );

    if (response.status === 201) {
      addSnackbar("Kurs başarıyla eklendi", "success");
      getMyCourses();
    } else {
      addSnackbar("Kurs eklenirken bir hata oluştu", "error");
    }
  };

  return (
    <div className="bg-gray-50 text-gray-800 min-h-screen">
      <Header
        title="Eğitmen Alanı"
        subtitle="Kurslarınızı ve kazançlarınızı yönetin"
      />

      <div className="container mx-auto py-8 px-4">
        <div className="flex border-b mb-6">
          <TabButton
            label="Kurslarım"
            isActive={activeTab === 1}
            onClick={() => setActiveTab(1)}
          />

          <TabButton
            label="Kurs Siparişleri"
            isActive={activeTab === 2}
            onClick={() => setActiveTab(2)}
          />
        </div>

        {activeTab === 1 ? (
          <div>
            <div className="bg-white rounded-lg shadow-md p-6">
              <div className="flex justify-between items-center mb-4">
                <h2 className="text-2xl font-semibold text-gray-700">
                  Kurslarım
                </h2>
                <button
                  className="bg-purple-600 text-white px-4 py-2 rounded-md hover:bg-purple-700"
                  onClick={() => setIsModalOpen(true)}
                >
                  Kurs Ekle
                </button>
              </div>
              <table className="w-full text-left border-collapse">
                <thead>
                  <tr className="text-gray-600 uppercase text-sm bg-gray-100">
                    <th className="py-3 px-6">Kapak Resmi</th>
                    <th className="py-3 px-6">Kurs Adı</th>
                    <th className="py-3 px-6">Açıklama</th>
                    <th className="py-3 px-6">Ücret</th>
                    <th className="py-3 px-6">İşlemler</th>
                  </tr>
                </thead>
                <tbody className="text-gray-700 text-sm font-light">
                  {myCourses.map((course) => (
                    <tr
                      key={course.id}
                      className="border-b border-gray-200 hover:bg-gray-50"
                    >
                      <td className="py-3">
                        <img
                          src={API_URL + course.coverImage}
                          alt={course.title}
                          className="w-60 h-30 object-cover rounded-lg"
                        />
                      </td>
                      <td
                        className="py-3 px-6
                      font-semibold
                      "
                      >
                        {course.title}
                      </td>
                      <td className="py-3 px-6">
                        {course.description.substring(0, 100)}...
                      </td>

                      <td className="py-3 px-6">{course.price} TL</td>
                      <td className="py-10 px-6">
                        <div className="flex space-x-2">
                          <MiniButton
                            text="Düzenle"
                            onClick={() =>
                              navigate(`/edit-course/${course.id}`)
                            }
                          />
                          <MiniButton
                            text="Sil"
                            onClick={() => {
                              setSelectedCourseId(course.id);
                              setIsDeleteModalOpen(true);
                            }}
                            color="red"
                          />
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
            <QuestionModal
              isOpen={isDeleteModalOpen}
              onClose={() => setIsDeleteModalOpen(false)}
              onConfirm={handleConfirmDelete}
              message="Bu kursu silmek istediğinize emin misiniz?"
            />
            <CourseAddModal
              isOpen={isModalOpen}
              onClose={() => setIsModalOpen(false)}
              onAddCourse={handleAddCourse}
            />
          </div>
        ) : (
          <div>
            <div className="bg-white rounded-lg shadow-md p-6">
              <h2 className="text-2xl font-semibold text-gray-700 mb-4">
                Kurs Siparişleri
              </h2>
              {loadingOrders ? (
                <p>Yükleniyor...</p>
              ) : errorOrders ? (
                <p className="text-red-500">{errorOrders}</p>
              ) : orders.length === 0 ? (
                <p>Hiç sipariş bulunamadı.</p>
              ) : (
                <table className="w-full text-left border-collapse">
                  <thead>
                    <tr className="text-gray-600 uppercase text-sm bg-gray-100">
                      <th className="py-3 px-6">Kapak Resmi</th>
                      <th className="py-3 px-6">Kurs Adı</th>
                      <th className="py-3 px-6">Açıklama</th>
                      <th className="py-3 px-6">Fiyat</th>
                      <th className="py-3 px-6">Ödeme Durumu</th>
                      <th className="py-3 px-6">Sipariş Tarihi</th>
                    </tr>
                  </thead>
                  <tbody className="text-gray-700 text-sm font-light">
                    {orders.map((order) => (
                      <tr
                        key={order.id}
                        className="border-b border-gray-200 hover:bg-gray-50"
                      >
                        <td className="py-3">
                          <img
                            src={API_URL + order.courseCoverImage}
                            alt={order.courseTitle}
                            className="w-60 h-30 object-cover rounded-lg"
                          />
                        </td>
                        <td className="py-3 px-6 font-semibold">
                          {order.courseTitle}
                        </td>
                        <td className="py-3 px-6">
                          {order.courseDescription.substring(0, 50)}...
                        </td>
                        <td className="py-3 px-6">{order.price} TL</td>
                        <td className="py-3 px-6">{order.paymentStatus}</td>
                        <td className="py-3 px-6">
                          {new Date(order.orderDate).toLocaleDateString()}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              )}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default TeacherDashboard;
