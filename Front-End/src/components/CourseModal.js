import React from "react";
import { API_URL } from "../consts/consts";

const CourseModal = ({ course, onClose }) => {
  if (!course) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
      <div className="bg-white p-6 rounded-lg max-w-lg w-full">
        <div className="flex justify-between items-center mb-4">
          <h2 className="text-2xl font-bold">{course.title}</h2>
          <button
            onClick={onClose}
            className="text-gray-500 hover:text-gray-700"
          >
            X
          </button>
        </div>
        <img
          src={API_URL + course.coverImage}
          alt={course.title}
          className="rounded-lg mb-4"
        />
        <p className="text-gray-600 mb-2">{course.description}</p>
        <p className="font-semibold mb-4">Eğitmen: {course.teacher}</p>
        <h3 className="text-lg font-bold mb-2">Videolar:</h3>
        <ul className="list-disc list-inside">
          {course.videos.map((video) => (
            <li key={video.id}>
              {video.indexInCourse}. {video.title}
            </li>
          ))}
        </ul>
        <div className="mt-6">
          <button
            onClick={onClose}
            className="bg-purple-500 text-white px-4 py-2 rounded-lg hover:bg-purple-700"
          >
            Kapat
          </button>
        </div>
      </div>
    </div>
  );
};

export default CourseModal;
