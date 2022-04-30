import React, { useState, useEffect, useContext } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import Active from '../Models/Active';
import { Link } from 'react-router-dom';
import { TradingContext } from '../TradingContext';
import { Button } from 'bootstrap';

export default function ActivesList(props){
    const [actives, setActives] = useContext(TradingContext);
    const contents = ()=>{
        let res = actives.map((active)=>{
            let ticketName = active.ticket;
            if(ticketName.includes('/')){
                ticketName = ticketName.replace('/',"%2F");
            }
        return ( 
                <tr key = { active.id } >
                <td><Link to={`/chart/${ticketName}`}>{active.name}</Link></td>
                <td> {active.ticket} </td>
                <td> { active.buy_price.toFixed(4) } </td>
                <td><button>Купить</button></td>
                <td> { active.sell_price.toFixed(4) } </td> 
                <td><button>Продать</button></td>
                </tr>);
        });
        return res;
    }
    
    return (
        <div>    
            <table className = 'table table-striped' aria-labelledby = "tabelLabel">
            <thead>
            <tr>
            <th> Name </th>  
            <th> Ticket </th>
            <th> Buy </th>
            <th></th>
            <th> Sell </th>
            <th></th>
            </tr> 
            </thead>
            <tbody> 
            {props.loaded ? contents() : null}
            </tbody>
            </table>
        </div>
    );
}