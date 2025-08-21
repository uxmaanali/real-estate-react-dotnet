import React from 'react'
import { Route, Routes } from "react-router-dom"
import Favorites from "../pages/Favorites"
import Home from "../pages/Home"
import Login from "../pages/Login"
import NotFound from "../pages/NotFound"
import PropertyDetail from "../pages/PropertyDetail"
import Register from "../pages/Register"
import PrivateRoute from "./private-route"

export default function MainRoutes() {
	return (
		<Routes>
			<Route path="/" element={<Home />} />

			<Route path="/auth/login" element={<Login />} />
			<Route path="/auth/register" element={<Register />} />

			<Route path="/properties/:id" element={<PropertyDetail />} />
			<Route path="/favorites" element={<PrivateRoute><Favorites /></PrivateRoute>} />

			<Route path="*" element={<NotFound />} />
		</Routes>
	)
}
