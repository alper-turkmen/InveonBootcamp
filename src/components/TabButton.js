const TabButton = ({ label, isActive, onClick, icon }) => {
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
