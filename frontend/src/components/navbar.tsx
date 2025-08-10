import { Link, useNavigate } from "react-router-dom"
import { AuthorizedContext } from "../services/authorized-context.service"
import { AuthService } from "../services/auth.service";

export default function Navbar() {
	const navigate = useNavigate();
	const isAuthenticated = AuthorizedContext.isAuthenticated();

	const logout = () => {
		AuthService.logout();
		navigate('/');
	}

	return (
		<nav className="navbar navbar-expand-md navbar-dark bg-dark">
			<div className="container-fluid">
				<Link className="navbar-brand" to="/">Real Estate</Link>

				<div className="collapse navbar-collapse justify-content-end">
					<ul className="nav navbar-nav ml-auto">
						{
							isAuthenticated ? (
								<li className="nav-item">
									<Link className="nav-link" to="/favorites">Favorites</Link>
								</li>
							) : ""
						}
						{
							isAuthenticated ?
								(<li className="nav-item">
									<a className="nav-link cursor-pointer" onClick={logout}>Logout</a>
								</li>) : (
									<li className="nav-item">
										<Link className="nav-link" to="/auth/login">Login</Link>
									</li>
								)
						}
					</ul>
				</div>
			</div>
		</nav>
	)
}
