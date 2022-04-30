import React, { useState, useEffect } from 'react';
import Active from './Models/Active';
import { BrowserRouter, Link, Navigate, Route, Routes } from 'react-router-dom';
import Portfolio from './components/Portfolio';
import Login from './components/Login';
import Registration from './components/Registration'
import ProtectedRoute from './components/ProtectedRoute';
import Trading from './components/Trading';
import 'bootstrap/dist/css/bootstrap.min.css';
import Dashboard from './components/Dashboard';
import { HubConnectionBuilder } from '@microsoft/signalr';
import ChartPage from './components/Chart';
import {TradingContext} from './TradingContext';

export default function App(){
    const[userData, setUserData] = useState();
    const[loggedIn, setLoggedIn] = useState();
    const [ actives, setActives] = useState(null);
    const [ loaded, setLoaded ] = useState(false);
    const[connection, setConnection] = useState(null);

    const setTokenn = (token) =>{
        localStorage.setItem("access_token",token);
    }

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
           await updateUserData();

            const resp = await fetch('https://localhost:7028/api/actives');
            const data = await resp.json();
            const results = [];
            for(let item of data){
                const result = new Active(item.name, item.ticket, 0, 0);
                result.id = item.id;
                results.push(result);
            }
            setActives(results);

            const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:7028/rates')
            .withAutomaticReconnect()
            .build();
            setConnection(newConnection);

            setLoaded(true);
        }
        
    },[loggedIn]);

    useEffect(async () => {
        if (connection) {
            try{
                await connection.start();
                console.log('Connected!');
                connection.on("SendRates", message =>{
                    if(actives){
                        const json = JSON.parse(message).payload;
                        const new_data = [...actives];
                        let index = new_data.findIndex(a => a.ticket == json.symbolName);
                        let item = new_data[index];
                        item.buy_price = json.ofr;
                        item.sell_price = json.bid;
                        setActives(new_data);
                    }        
                });
            }
            catch(e){
                console.log(e);
            }
        }
    }, [connection]);

    const setLogIn = () => {
        setLoggedIn(true);
    }

    return (
        <div>
            <TradingContext.Provider value={[actives, setActives]}>
                <BrowserRouter>
                    <Routes>
                        <Route element={<Dashboard><ProtectedRoute token={getToken()}/></Dashboard>}>
                            <Route path='/trading' element={<Trading loaded={loaded}/> }/>
                            <Route path='/portfolio' element={<Portfolio profile={userData}/>}/>
                            <Route path='/chart/:ticket' element={<ChartPage />}/>
                        </Route>
                        <Route path='/registration' element={<Registration/>}/>
                        <Route path='/login' element={<Login/>}/>
                        <Route path='/' element={getToken()? <Navigate to="/trading" replace/> : 
                            <Navigate to={{pathname: "/login"}} replace/>}/>
                    </Routes>
                </BrowserRouter>
            </TradingContext.Provider>
        </div>
    );
}