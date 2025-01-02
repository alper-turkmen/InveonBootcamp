
  const CourseCard = ({ image, title, description }) => {
    return (
      <div className="bg-white rounded-lg shadow-lg p-6">
        <img src={image} alt={title} className="rounded-lg mb-4" />
        <h3 className="text-lg font-bold">{title}</h3>
        <p className="text-gray-600 mt-2">{description}</p>
        <button className="mt-4 bg-purple-600 text-white py-2 rounded-lg w-full">
          Detayları Gör
        </button>
      </div>
    );
  };

    export default CourseCard;