import { Navigate } from "react-router-dom";
import AuthService from "../api/services/Auth";

export function PrivateRoute({ children }) {
    const user = AuthService.getUser();

    return !user ? <Navigate to="/student/login"/> : children;
}