const Header = ({ title, subtitle }) => {
    return (
      <section className="bg-purple-400 text-white py-10 bg-gradient-to-b from-purple-400 to-purple-700">
        <div className="container mx-auto text-center">
          <h1 className="text-4xl font-bold">{title}</h1>
          <p className="text-lg mt-2">{subtitle}</p>
        </div>
      </section>
    );
  };
  
  export default Header;