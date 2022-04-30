import React, { useState, useEffect } from 'react';
import { BrowserRouter, Link, Navigate, Route, Routes } from 'react-router-dom';
import Portfolio from './Portfolio';
import ProtectedRoute from './ProtectedRoute';
import Trading from './Trading';
import 'bootstrap/dist/css/bootstrap.min.css';

export default function Dashboard(props){
    
    return (
        <div>
        <p>
            <h2><Link to="/trading">Trading</Link></h2>
            <h2><Link to="/portfolio">Portfolio</Link></h2>
        </p>
            {props.children}
        </div>
    );
}