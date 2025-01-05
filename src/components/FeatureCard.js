const FeatureCard = ({ icon, title, description }) => {
  return (
    <div className="p-8 bg-white rounded-lg shadow-lg">
      <div className="text-4xl flex justify-center items-center text-white rounded-full w-14 h-14 mx-auto">
        <img src={icon} alt={title} width={64} />
      </div>
      <h3 className="text-xl font-bold mt-4">{title}</h3>
      <p className="mt-4 text-gray-600">{description}</p>
    </div>
  );
};

export default FeatureCard;
