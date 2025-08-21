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
import MainRoutes from "./routes/main-routes";

function App() {


  return (
    <div className="container">
      <Navbar />
      <MainRoutes />
    </div>

  );
}

export default App;
