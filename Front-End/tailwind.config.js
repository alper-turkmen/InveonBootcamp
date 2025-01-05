/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        primary: '#9333EA', 
        secondary: '#6B7280', 
        background: '#F9FAFB', 
        white: '#FFFFFF', 
        gray: {
          50: '#F9FAFB',
          100: '#F3F4F6',
          600: '#4B5563',
          800: '#1F2937',
        },
        purple: {
          600: '#9333EA', 
        },
      },
    },
  },
  plugins: [],
}

