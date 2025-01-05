import React, { useState, useEffect } from "react";
import TabButton from "../../components/TabButton";
import Header from "../../components/Header";
import axios from "../../utils/axiosconf";
import { useAuth } from "../../contexts/AuthContext";
import { useSnackbar } from "../../contexts/AlertContext";
import { API_URL } from "../../consts/consts";
import MiniButton from "../../components/MiniButton";
import { useNavigate } from "react-router-dom";

const UserDashboard = () => {
  const [activeTab, setActiveTab] = useState(1);
  const { token } = useAuth();
  const [loading, setLoading] = useState(false);
  const [orders, setOrders] = useState([]);
  const [error, setError] = useState("");
  const { addSnackbar } = useSnackbar();
  const navigate = useNavigate();

  const getUserOrders = async () => {
    setLoading(true);
    setError("");

    try {
      const response = await axios.get("/Orders", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      setOrders(response.data);
    } catch (err) {
      setError("Siparişler alınırken bir hata oluştu.");
      addSnackbar("Siparişler alınırken bir hata oluştu.", "error");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    getUserOrders();
  }, []);

  return (
    <div className="bg-gray-50 text-gray-800 min-h-screen">
      <Header
        title="Kurslarım"
        subtitle="Satın aldığınız kurslarınızı görüntüleyin"
      />

      <div className="container mx-auto py-8 px-4">
        <div className="flex border-b mb-6">
          <TabButton
            label="Kurslarım"
            isActive={activeTab === 1}
            onClick={() => setActiveTab(1)}
          />
        </div>

        {activeTab === 1 && (
          <div>
            <div className="bg-white rounded-lg shadow-md p-6">
              <h2 className="text-2xl font-semibold text-gray-700 mb-4">
                Kurslarım
              </h2>

              {loading ? (
                <p className="text-gray-500">Yükleniyor...</p>
              ) : orders.length === 0 ? (
                <p className="text-gray-500">Henüz kurs satın almadınız.</p>
              ) : (
                <table className="w-full text-left border-collapse">
                  <thead>
                    <tr className="text-gray-600 uppercase text-sm bg-gray-100">
                      <th className="py-3 px-6">Kapak Resmi</th>
                      <th className="py-3 px-6">Kurs Adı</th>
                      <th className="py-3 px-6">Açıklama</th>
                      <th className="py-3 px-6">Fiyat</th>
                      <th className="py-3 px-6">Durum</th>
                      <th className="py-3 px-6">İşlemler</th>
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
                            className="w-32 h-20 object-cover rounded-lg"
                          />
                        </td>
                        <td className="py-3 px-6 font-semibold">
                          {order.courseTitle}
                        </td>
                        <td className="py-3 px-6">
                          {order.courseDescription.substring(0, 100)}...
                        </td>
                        <td className="py-3 px-6">{order.price} TL</td>
                        <td className="py-3 px-6">{order.paymentStatus}</td>
                        <td className="py-3 px-6">
                          <div className="flex space-x-2">
                            <MiniButton
                              text="Videolar"
                              onClick={() => navigate(`/watch/${order.id}`)}
                            />
                          </div>
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

export default UserDashboard;
