import React from "react";
import { Route, Redirect, Navigate, Outlet } from "react-router-dom";

export default function ProtectedRoute({token, redirectPath = '/login'}){
  if (!token) {
    return <Navigate to={redirectPath} replace />;
  }
  return <Outlet/>;
};