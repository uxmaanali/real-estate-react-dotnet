import { Routes, Route, Navigate } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/Login";
import NotFound from "./pages/NotFound";
import Register from "./pages/Register";

import "../styles/style.css";
import Navbar from "./components/navbar";
import { AuthorizedContext } from "./services/authorized-context.service";
import Favorites from "./pages/Favorites";
import PropertyDetail from "./pages/PropertyDetail";

function App() {
  const PrivateRoute = ({ children }: { children: JSX.Element }) => {
    const isAuthorized = AuthorizedContext.isAuthenticated();
    return isAuthorized ? children : <Navigate to="/auth/login" />;
  };

  return (
    <div className="container">
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/properties/:id" element={<PropertyDetail />} />
        <Route path="/auth/login" element={<Login />} />
        <Route path="/auth/register" element={<Register />} />
        <Route path="/favorites" element={<PrivateRoute><Favorites /></PrivateRoute>} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </div>

  );
}

export default App;
