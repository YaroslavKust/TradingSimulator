import React, { useState, useEffect } from 'react';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import Portfolio from './components/Portfolio';
import Login from './components/Login';
import Registration from './components/Registration'
import ProtectedRoute from './components/ProtectedRoute';
import Trading from './components/Trading';
import 'bootstrap/dist/css/bootstrap.min.css';
import './styles/style.css';
import Dashboard from './components/Dashboard';
import ChartPage from './components/Chart';
import {UserContext} from './UserContext';
import Logout from './components/Logout';

export default function App(){
    const[userData, setUserData] = useState();
    const[loggedIn, setLoggedIn] = useState(false);
    const [ loaded, setLoaded ] = useState(false);

    const getToken = () =>{
        return localStorage.getItem("access_token");
    }

    const updateUserData = async () =>{
        const response = await fetch('https://localhost:7028/api/account/balance', {
                method: 'GET',
                headers:{
                    "Authorization": "Bearer " + localStorage.getItem("access_token")
                }
            });
            let res = await response.json();
            console.log(res);
            setUserData(res);
    }

    useEffect( async () => {
        if(getToken()){
            setLoggedIn(true);
            await updateUserData();
        }
        setLoaded(true);
    },[]);

    const setLoggedInFunc = () =>{
        setLoggedIn(true);
    }

    const setLogout = () =>{
        setLoggedIn(false);
    }

    return (loaded ?
        <div className='app'>
            <UserContext.Provider value={updateUserData}>
                <BrowserRouter>
                    <Routes>
                        <Route element={<Dashboard><ProtectedRoute loggedIn={loggedIn}/></Dashboard>}>
                            <Route path='/trading' element={<Trading loaded={loaded}/> }/>
                            <Route path='/portfolio' element={<Portfolio profile={userData} loaded={loaded}/>}/>
                            <Route path='/chart/:ticket' element={<ChartPage />}/>
                        </Route>
                        <Route path='/registration' element={<Registration/>}/>
                        <Route path='/logout' element={<Logout setLogout={setLogout}/>}/>
                        <Route path='/login' element={<Login setLoggedIn={setLoggedInFunc}/>}/>
                        <Route path='/' element={loggedIn? <Navigate to="/trading" replace/> : 
                            <Navigate to={{pathname: "/login"}} replace/>}/>
                    </Routes>
                </BrowserRouter>
            </UserContext.Provider>
        </div>
        : <div>Loading...</div>
    );
}