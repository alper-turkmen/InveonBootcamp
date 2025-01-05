import React, {
  useCallback,
  useEffect,
  useMemo,
  useReducer,
  useState,
} from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import { AuthProvider } from "./contexts/AuthContext";
import { CartProvider } from "./contexts/CartContext";

import HomePage from "./pages/HomePage";
import CourseDetailPage from "./pages/CourseDetailPage";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import ProfilePage from "./pages/ProfilePage";
import Navbar from "./components/Navbar";
import Footer from "./components/Footer";
import NotFound from "./pages/NotFound";
import WatchCourse from "./pages/WatchCourse";
import { SnackbarProvider } from "./contexts/AlertContext";
import TeacherDashboard from "./pages/TeacherPages/TeacherDashboard";
import EditCoursePage from "./pages/TeacherPages/EditCoursePage";
import UserDashboard from "./pages/UserPages/UserDashboard";
import UserCart from "./pages/UserPages/UserCart";
import AboutUs from "./pages/AboutUs";

function App() {
  return (
    <Router>
      <SnackbarProvider>
        <AuthProvider>
          <CartProvider>
            <Navbar />
            <Routes>
              <Route path="/" element={<HomePage />} />
              <Route path="/course/:id" element={<CourseDetailPage />} />
              <Route path="/login" element={<LoginPage />} />
              <Route path="/register" element={<RegisterPage />} />
              <Route path="/profile" element={<ProfilePage />} />
              <Route path="*" element={<NotFound />} />
              <Route path="/watch/:id" element={<WatchCourse />} />
              <Route path="/teacher-dashboard" element={<TeacherDashboard />} />
              <Route path="/edit-course/:id" element={<EditCoursePage />} />
              <Route path="/user-dashboard" element={<UserDashboard />} />
              <Route path="/cart" element={<UserCart />} />
              <Route path="/about" element={<AboutUs />} />
            </Routes>
            <Footer />
          </CartProvider>
        </AuthProvider>
      </SnackbarProvider>
    </Router>
  );
}

export default App;
