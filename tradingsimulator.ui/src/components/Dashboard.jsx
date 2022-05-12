import React, { useState, useEffect } from 'react';
import { BrowserRouter, Link, Navigate, Route, Routes } from 'react-router-dom';
import Portfolio from './Portfolio';
import ProtectedRoute from './ProtectedRoute';
import Trading from './Trading';
import 'bootstrap/dist/css/bootstrap.min.css';

export default function Dashboard(props){
    
    return (
        <div>
        <div className='dashboard-header'>
            <h3 className='dashboard-header__item'><Link to="/trading">Торговля</Link></h3>
            <h3 className='dashboard-header__item'><Link to="/portfolio">Портфель</Link></h3>
            <h3 className='dashboard-header__item-exit'><Link to="/logout">Выход</Link></h3>
        </div>
            {props.children}
        </div>
    );
}