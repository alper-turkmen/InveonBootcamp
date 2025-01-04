import React, { useState } from 'react';
import TabButton from '../../components/TabButton';
import Header from '../../components/Header';

/*
const TabButton = ({ label, isActive, onClick }) => {
    return (
      <button
        onClick={onClick}
        className={`py-2 px-6 text-lg font-medium focus:outline-none ${
          isActive
            ? "border-b-4 border-purple-600 text-purple-600"
            : "text-gray-600 hover:text-purple-600"
        }`}
      >
        {label}
      </button>
    );
  };
  
  export default TabButton;
*/
const TeacherDashboard = () => {
  const [activeTab, setActiveTab] = useState(1); 

  const myCourses = [
    {
      id: 1,
      title: "React Programlama",
      instructor: "Mehmet Ahmet",
      date: "2024-01-01",
      status: "Devam Ediyor",
    },
    {
      id: 2,
      title: ".NET Core ile Web API Geliştirme",
      instructor: "Ahmet Mehmet",
      date: "2024-01-15",
      status: "Tamamlandı",
    },
    {
      id: 3,
      title: "Dijital Pazarlama",
      instructor: "Ahmet Mehmet",
      date: "2024-02-10",
      status: "Başlamadı",
    },
  ];

  return (
    <div className="bg-gray-50 text-gray-800 min-h-screen">

       <Header title="Kurslarım" subtitle="Kurslarınızı ve kazançlarınızı yönetin" />
   
      <div className="container mx-auto py-8 px-4">
        <div className="flex border-b mb-6">
        
            
            <TabButton
                label="Kurslarım"
                isActive={activeTab === 1}
                onClick={() => setActiveTab(1)}
            />

          <TabButton
          label="Kazançlarım"
            isActive={activeTab === 2}
            onClick={() => setActiveTab(2)}
            />

        </div>

        {activeTab === 1 ? (
          <div>
            <div className="bg-white rounded-lg shadow-md p-6">
              <div className="flex justify-between items-center mb-4">
                <h2 className="text-2xl font-semibold text-gray-700">Kurslarım</h2>
                <button className="bg-purple-600 text-white px-4 py-2 rounded-md hover:bg-purple-700">
                  Kurs Ekle
                </button>
              </div>
              <table className="w-full text-left border-collapse">
                <thead>
                  <tr className="text-gray-600 uppercase text-sm leading-normal bg-gray-100">
                    <th className="py-3 px-6">Kurs Adı</th>
                    <th className="py-3 px-6">Eğitmen</th>
                    <th className="py-3 px-6">Tarih</th>
                    <th className="py-3 px-6">Durum</th>
                  </tr>
                </thead>
                <tbody className="text-gray-700 text-sm font-light">
                  {myCourses.map((course) => (
                    <tr
                      key={course.id}
                      className="border-b border-gray-200 hover:bg-gray-50"
                    >
                      <td className="py-3 px-6">{course.title}</td>
                      <td className="py-3 px-6">{course.instructor}</td>
                      <td className="py-3 px-6">{course.date}</td>
                      <td
                        className={`py-3 px-6 ${
                          course.status === "Tamamlandı"
                            ? "text-green-600"
                            : course.status === "Devam Ediyor"
                            ? "text-yellow-600"
                            : "text-red-600"
                        }`}
                      >
                        {course.status}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        ) : (
          <div className="bg-white rounded-lg shadow-md p-6 text-center text-gray-500">
            <h2 className="text-xl font-medium">Bu sekme henüz boş!</h2>
            <p className="mt-2">Buraya içerik eklemek için daha fazla geliştirme yapabilirsiniz.</p>
          </div>
        )}

      </div>
    </div>
  );
};

export default TeacherDashboard;