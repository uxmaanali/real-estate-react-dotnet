import React from 'react'
import { AuthorizedContext } from "../services/authorized-context.service";
import { Navigate } from "react-router-dom";

export default function PrivateRoute({ children }: { children: JSX.Element }) {
	const isAuthorized = AuthorizedContext.isAuthenticated();
	return isAuthorized ? children : <Navigate to="/auth/login" />;
}
