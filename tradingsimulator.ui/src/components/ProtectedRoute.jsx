import React from "react";
import { Navigate, Outlet } from "react-router-dom";

export default function ProtectedRoute({loggedIn, redirectPath = '/login'}){
  if (!loggedIn) {
    return <Navigate to={redirectPath} replace />;
  }
  return <Outlet/>;
};