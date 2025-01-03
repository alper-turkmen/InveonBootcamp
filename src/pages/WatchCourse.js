import React, { useState } from 'react';

const WatchCourse = () => {
  const [currentVideo, setCurrentVideo] = useState({
    title: 'Ders 1: Giriş',
    src: '',
  });

  const lessons = [
    { title: 'Ders 1: Giriş', src: '' },
    { title: 'Ders 2: HTML Temelleri', src: '' },
    { title: 'Ders 3: CSS ile Stil Verme', src: '' },
    { title: 'Ders 4: JavaScript’e Giriş', src: '' },
    { title: 'Ders 5: Proje Tamamlama', src: '' },
  ];

  const handleVideoChange = (lesson) => {
    setCurrentVideo(lesson);
  };

  return (
    <div className="flex flex-col md:flex-row h-screen bg-gray-100">
      <div className="flex-1 flex flex-col">
        <div className="p-4 border-b bg-white shadow">
          <h1 className="text-xl font-semibold text-gray-800">{currentVideo.title}</h1>
        </div>
        <div className="flex-grow bg-black flex items-center justify-center p-4">
          <video
            controls
            className="w-full max-h-[80vh] object-contain rounded-lg shadow-lg"
            src={currentVideo.src}
          ></video>
        </div>
      </div>

      <div className="md:w-1/4 w-full bg-white border-t md:border-l md:border-t-0 shadow-lg overflow-y-auto">
        <h2 className="text-lg font-bold p-4 border-b">Ders Listesi</h2>
        <ul>
          {lessons.map((lesson, index) => (
            <li
              key={index}
              className={`p-4 border-b cursor-pointer ${
                currentVideo.title === lesson.title
                  ? 'bg-purple-600 text-white'
                  : 'hover:bg-gray-50'
              }`}
              onClick={() => handleVideoChange(lesson)}
            >
              {lesson.title}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default WatchCourse;