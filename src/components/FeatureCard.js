const FeatureCard = ({ icon, title, description }) => {
    return (
      <div className="p-8 bg-white rounded-lg shadow-lg">
        <div className="text-4xl">{icon}</div>
        <h3 className="text-xl font-bold mt-4">{title}</h3>
        <p className="mt-4 text-gray-600">{description}</p>
      </div>
    );
  };
  
    export default FeatureCard;