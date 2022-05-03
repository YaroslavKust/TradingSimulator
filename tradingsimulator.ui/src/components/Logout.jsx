import { Navigate } from "react-router-dom";

export default function Logout(props){
    const setLogout = props.setLogout;

    const logout = () =>{
        localStorage.removeItem("accessToken");
        setLogout();
    }

    logout();

    return (
        <Navigate to={{pathname: "/login"}} replace/>
    )
}