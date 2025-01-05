import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { useSnackbar } from "../../contexts/AlertContext";
import { useAuth } from "../../contexts/AuthContext";
import axios from "../../utils/axiosconf";
import { API_URL } from "../../consts/consts";
import InputField from "../../components/InputField";
import { DndContext, closestCenter } from "@dnd-kit/core";
import {
  arrayMove,
  SortableContext,
  verticalListSortingStrategy,
} from "@dnd-kit/sortable";
import SortableItem from "../../components/SortableItem";
import Header from "../../components/Header";
import MiniButton from "../../components/MiniButton";
import TabButton from "../../components/TabButton";
import VideoWindow from "../../components/VideoWindow";
import VideoTitle from "../../components/VideoTitle";
import VideoUploadModal from "../../components/VideoUploadModal";
import { FaPlay } from "react-icons/fa";

const EditCoursePage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { token } = useAuth();
  const { addSnackbar } = useSnackbar();

  const [activeTab, setActiveTab] = useState(1);
  const [course, setCourse] = useState({
    title: "",
    description: "",
    price: "",
    coverImage: "",
    videos: [],
  });

  const [loading, setLoading] = useState(false);
  const [coverImageFile, setCoverImageFile] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentVideo, setCurrentVideo] = useState({ url: "", title: "" });
  const [openTitleModal, setOpenTitleModal] = useState(false);
  const [isUploadModalOpen, setIsUploadModalOpen] = useState(false);
  const [showInfo, setShowInfo] = useState(true);

  useEffect(() => {
    const fetchCourse = async () => {
      try {
        const response = await axios.get(`/Course/${id}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        setCourse(response.data);
      } catch (error) {
        addSnackbar("Kurs bilgileri alınırken hata oluştu", "error");
      }
    };
    fetchCourse();
  }, [id, token, addSnackbar]);

  const handleDragEnd = async (event) => {
    const { active, over } = event;

    if (active.id !== over.id) {
      const oldIndex = course.videos.findIndex(
        (video) => video.id === active.id
      );
      const newIndex = course.videos.findIndex((video) => video.id === over.id);
      const sortedVideos = arrayMove(course.videos, oldIndex, newIndex);

      setCourse({ ...course, videos: sortedVideos });

      try {
        for (let index = 0; index < sortedVideos.length; index++) {
          const video = sortedVideos[index];

          await axios.put(
            `/Video/${id}/videos/${video.id}`,
            {
              indexInCourse: index,
              id: video.id,
              title: video.title,
              url: video.url,
            },
            {
              headers: {
                Authorization: `Bearer ${token}`,
              },
            }
          );
        }

        addSnackbar("Video sıralaması güncellendi", "success");
      } catch (error) {
        addSnackbar("Sıralama güncellenirken bir hata oluştu", "error");
        setCourse({
          ...course,
          videos: arrayMove(sortedVideos, newIndex, oldIndex),
        });
      }
    }
  };

  const handleUploadVideo = async (title, base64, fileName) => {
    try {
      const response = await axios.post(
        `/Video/${id}/videos`,
        {
          title: title,
          file: base64,
          fileName: fileName,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );

      if (response.status === 201) {
        const newVideo = response.data;
        setCourse((prev) => ({
          ...prev,
          videos: [...prev.videos, newVideo],
        }));

        addSnackbar("Video başarıyla eklendi.", "success");
        setIsUploadModalOpen(false);
      } else {
        addSnackbar("Video eklenirken hata oluştu.", "error");
      }
    } catch (error) {
      addSnackbar("Video eklenirken hata oluştu.", "error");
    }
  };

  const handleDeleteVideo = async (id) => {
    const video = course.videos.find((video) => video.id === id);

    let response = await axios.delete(
      `/Video/${course.id}/videos/${video.id}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response.status === 204) {
      addSnackbar("Video başarıyla silindi.", "success");
    } else {
      addSnackbar("Video silinirken bir hata oluştu.", "error");
    }

    const updatedVideos = course.videos.filter((video) => video.id !== id);
    setCourse({ ...course, videos: updatedVideos });
  };

  const handleWatchVideo = (url, title) => {
    setCurrentVideo({ url: API_URL + url, title });
    setIsModalOpen(true);

    console.log(currentVideo);
    console.log(isModalOpen);
  };

  const handleUpdateVideoTitle = async (id, newTitle) => {
    try {
      const video = course.videos.find((v) => v.id === id);
      const updatedVideo = { ...video, title: newTitle };

      await axios.put(
        `/Video/${course.id}/videos/${video.id}`,
        {
          indexInCourse: video.indexInCourse,
          id: video.id,
          title: newTitle,
          url: video.url,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      const updatedVideos = course.videos.map((v) =>
        v.id === id ? updatedVideo : v
      );
      setCourse({ ...course, videos: updatedVideos });

      addSnackbar("Video başlığı başarıyla güncellendi.", "success");
      setOpenTitleModal(false);
    } catch (error) {
      addSnackbar("Video başlığı güncellenirken bir hata oluştu.", "error");
    }
  };
  const openEditTitleModal = (id) => {
    const video = course.videos.find((video) => video.id === id);
    setCurrentVideo(video);
    setOpenTitleModal(true);
    console.log(video);
  };

  const handleFileUpload = async (file) => {
    try {
      if (!file) {
        addSnackbar("Lütfen bir dosya seçin.", "warning");
        return;
      }

      const toBase64 = (file) => {
        return new Promise((resolve, reject) => {
          const reader = new FileReader();
          reader.readAsDataURL(file);
          reader.onload = () => resolve(reader.result.split(",")[1]);
          reader.onerror = (error) => reject(error);
        });
      };

      const base64String = await toBase64(file);
      const fileName = file.name;

      const response = await axios.put(
        `/Course/${id}/photo`,
        {
          coverImage: base64String,
          coverImageName: fileName,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );

      if (response.status >= 200 && response.status < 300) {
        addSnackbar("Kapak resmi güncellendi.", "success");
      } else {
        addSnackbar("Kapak resmi güncellenemedi.", "error");
      }
    } catch (error) {
      console.error(error);
      addSnackbar("Kapak resmi güncellenirken bir hata oluştu.", "error");
    }
  };

  const handleUpdateCourseInfo = async () => {
    try {
      setLoading(true);

      if (!course.title || !course.description || !course.price) {
        addSnackbar("Lütfen tüm alanları doldurun.", "error");
        setLoading(false);
        return;
      }

      const data = {
        title: course.title,
        description: course.description,
        price: course.price,
      };

      const response = await axios.put(`/Course/${id}`, data, {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });

      if (response.status >= 200 && response.status < 300) {
        addSnackbar("Kurs bilgileri güncellendi.", "success");
      } else {
        addSnackbar("Kurs bilgileri güncellenemedi.", "error");
      }
    } catch (error) {
      console.error(error);
      addSnackbar("Kurs bilgileri güncellenirken bir hata oluştu.", "error");
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <div className="bg-gray-50 text-gray-800 min-h-screen">
        <Header
          title="Kursu Düzenle"
          subtitle="Kursunuzu ve videolarınızı düzenleyin."
        />

        <div className="container mx-auto py-8 px-4 bg-white rounded-lg shadow-md">
          <div className="flex border-b mb-6">
            <TabButton
              label="Kurs Bilgileri"
              isActive={activeTab === 1}
              onClick={() => setActiveTab(1)}
            />
            <TabButton
              icon={<FaPlay />}
              label="Videolar"
              isActive={activeTab === 2}
              onClick={() => setActiveTab(2)}
            />
          </div>

          {activeTab === 1 && (
            <div>
              <label className="block font-medium mb-2">Kapak Resmi</label>
              <div className="flex items-center space-x-4 mb-6">
                <img
                  src={
                    coverImageFile
                      ? URL.createObjectURL(coverImageFile)
                      : API_URL + course.coverImage
                  }
                  alt="Kapak Resmi"
                  className="h-30 object-cover rounded-lg border-2 border-purple-500"
                />
                <input
                  type="file"
                  onChange={(e) => {
                    if (e.target.files.length > 0) {
                      setCoverImageFile(e.target.files[0]);
                      handleFileUpload(e.target.files[0]);
                    }
                  }}
                  className="border p-2 rounded-md"
                />
              </div>

              <label className="block font-medium mb-2">Kurs Adı</label>
              <InputField
                id="title"
                type="text"
                placeholder="Başlık"
                value={course.title}
                onChange={(e) =>
                  setCourse({ ...course, title: e.target.value })
                }
                rounded="md"
              />
              <br />

              <label className="block font-medium mb-2">Açıklama</label>
              <textarea
                placeholder="Açıklama"
                value={course.description}
                onChange={(e) =>
                  setCourse({ ...course, description: e.target.value })
                }
                rows="4"
                className="w-full border p-2 rounded-md mt-4 mb-4"
              ></textarea>
              <br />
              <label className="block font-medium mb-2">Fiyat</label>
              <InputField
                id="price"
                type="number"
                placeholder="Fiyat"
                value={course.price}
                onChange={(e) =>
                  setCourse({ ...course, price: e.target.value })
                }
                rounded="md"
              />
              <br />
              <MiniButton text="Kaydet" onClick={handleUpdateCourseInfo} />
            </div>
          )}

          {activeTab === 2 && (
            <div>
              {showInfo && (
                <div
                  className={`flex items-center p-4 my-4 text-sm rounded-lg  bg-green-200 text-green-800'
            }`}
                >
                  <span className="mr-3">
                    Videoların öğrencilerin göreceği sırasını değiştirmek için
                    sürükleyip bırakabilirsiniz
                  </span>
                  <button
                    className="ml-auto p-1 rounded-full bg-transparent hover:bg-gray-200"
                    onClick={() => setShowInfo(false)}
                  >
                    <svg
                      className="w-4 h-4"
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 14 14"
                    >
                      <path
                        stroke="currentColor"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth="2"
                        d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"
                      />
                    </svg>
                  </button>
                </div>
              )}

              <DndContext
                collisionDetection={closestCenter}
                onDragEnd={handleDragEnd}
              >
                <SortableContext
                  items={course.videos.map((video) => video.id)}
                  strategy={verticalListSortingStrategy}
                >
                  {course.videos.map((video) => (
                    <SortableItem
                      key={video.id}
                      id={video.id}
                      title={video.title}
                      onUpdate={() => openEditTitleModal(video.id)}
                      onDelete={() => handleDeleteVideo(video.id)}
                      onWatch={() => handleWatchVideo(video.url, video.title)}
                    />
                  ))}
                </SortableContext>
              </DndContext>
              <MiniButton
                text="Video Ekle"
                onClick={() => setIsUploadModalOpen(true)}
              />
              <VideoWindow
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                videoUrl={currentVideo.url}
                title={currentVideo.title}
              />

              <VideoUploadModal
                isOpen={isUploadModalOpen}
                onClose={() => setIsUploadModalOpen(false)}
                onUpload={handleUploadVideo}
              />

              <VideoTitle
                currentVideo={currentVideo}
                setCurrentVideo={setCurrentVideo}
                setOpenTitleModal={setOpenTitleModal}
                openTitleModal={openTitleModal}
                updateVideoTitle={handleUpdateVideoTitle}
              />
            </div>
          )}
        </div>
      </div>
    </>
  );
};

export default EditCoursePage;
