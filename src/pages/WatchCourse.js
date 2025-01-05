import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "../utils/axiosconf";
import { API_URL } from "../consts/consts";
import { useAuth } from "../contexts/AuthContext";
import { useSnackbar } from "../contexts/AlertContext";
import { FaPlayCircle } from "react-icons/fa";

const WatchCourse = () => {
  const { id } = useParams();
  const { token } = useAuth();
  const { addSnackbar } = useSnackbar();

  const [loading, setLoading] = useState(true);
  const [course, setCourse] = useState(null);
  const [currentVideo, setCurrentVideo] = useState({});

  useEffect(() => {
    const fetchCourseDetails = async () => {
      try {
        setLoading(true);
        const response = await axios.get(`/Orders/${id}/coursedetails`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        setCourse(response.data);
        setCurrentVideo(response.data.videos[0]);
      } catch (error) {
        addSnackbar("Kurs bilgileri alınırken hata oluştu!", "error");
      } finally {
        setLoading(false);
      }
    };

    fetchCourseDetails();
  }, [id, token]);

  const handleVideoChange = (video) => {
    setCurrentVideo(video);
  };

  if (loading) {
    return (
      <div className="flex justify-center items-center h-screen bg-gray-100">
        <div className="flex items-center space-x-2">
          <svg
            className="animate-spin h-10 w-10 text-purple-600"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
          >
            <circle
              className="opacity-25"
              cx="12"
              cy="12"
              r="10"
              stroke="currentColor"
              strokeWidth="4"
            ></circle>
            <path
              className="opacity-75"
              fill="currentColor"
              d="M4 12a8 8 0 018-8v8z"
            ></path>
          </svg>
          <span className="text-gray-700 text-lg">Yükleniyor...</span>
        </div>
      </div>
    );
  }

  return (
    <div className="flex flex-col md:flex-row h-screen bg-gray-50">
      <div className="flex-1 flex flex-col">
        <div className="p-4 border-b bg-white shadow flex justify-between items-center">
          <h1 className="text-xl font-semibold text-gray-800">
            {currentVideo.title}
          </h1>

          <h1 className="text-xl font-semibold text-gray-500">
            {course.title}
          </h1>
        </div>
        <div className="flex-grow bg-black flex items-center justify-center p-4">
          <video
            controls
            className="w-full max-h-[80vh] object-contain rounded-lg shadow-lg"
            src={API_URL + currentVideo.url}
          ></video>
        </div>
      </div>

      <div className="md:w-1/4 w-full bg-white border-t md:border-l md:border-t-0 shadow-lg overflow-y-auto">
        <h2 className="text-lg font-bold p-4 border-b bg-gray-100 text-gray-800">
          Ders Listesi
        </h2>
        <ul>
          {course.videos.map((video, index) => (
            <li
              key={video.id}
              className={`flex items-center space-x-4 p-4 border-b cursor-pointer transition-all ${
                currentVideo.id === video.id
                  ? "bg-purple-600 text-white"
                  : "hover:bg-gray-50 text-gray-700"
              }`}
              onClick={() => handleVideoChange(video)}
            >
              <span
                className={`text-sm font-semibold ${
                  currentVideo.id === video.id ? "text-white" : "text-gray-500"
                }`}
              >
                {index + 1}.
              </span>
              <span className="flex-grow">{video.title}</span>
              {currentVideo.id === video.id && <FaPlayCircle size={18} />}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default WatchCourse;
