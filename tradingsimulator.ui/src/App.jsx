import React, { useState, useEffect } from 'react';
import Active from './Models/Active';
import ActivesList from './components/ActivesList';
import { BrowserRouter, Link, Navigate, Route, Routes } from 'react-router-dom';
import Portfolio from './components/Portfolio';
import Login from './components/Login';
import Registration from './components/Registration'
import ProtectedRoute from './components/ProtectedRoute';
import Trading from './components/Trading';
import 'bootstrap/dist/css/bootstrap.min.css';

export default function App(){
    
    const setTokenn = (token) =>{
        localStorage.setItem("access_token",token);
    }

    const getToken = () =>{
        return localStorage.getItem("access_token");
    }

    return (
        <div>
            <BrowserRouter>
                <p>
                    <h2><Link to="/trading">Trading</Link></h2>
                    <h2><Link to="/portfolio">Portfolio</Link></h2>
                </p>
                <Routes>
                    <Route element={<ProtectedRoute token={getToken()}/>}>
                        <Route path='/trading' element={<Trading/> }/>
                        <Route path='/portfolio' element={<Portfolio/>}/>
                    </Route>
                    <Route path='/registration' element={<Registration/>}/>
                    <Route path='/login' element={<Login/>}/>
                    <Route path='/' element={getToken()? <Navigate to="/trading" replace/> : 
                        <Navigate to={{pathname: "/login", state:{setToken: setTokenn}}} replace/>}/>
                </Routes>
            </BrowserRouter>
        </div>
    );
}