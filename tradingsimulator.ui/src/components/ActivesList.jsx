import React, { useState, useEffect } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import Active from '../Models/Active';

export default function ActivesList(props){
    const [ connection, setConnection ] = useState(null);
    const [ actives, setActives] = useState(props.actives);
    const [ loaded, setLoaded ] = useState(false);

    useEffect(async ()=>{
        const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:7028/rates')
            .withAutomaticReconnect()
            .build();
        setConnection(newConnection);
        setLoaded(true);
    }, []);

    useEffect(async () => {
        if (connection) {
            try{
                await connection.start();
                console.log('Connected!');
                connection.on("SendRates", message =>{
                    if(actives){
                        var json = JSON.parse(message).payload;
                        var new_data = [...actives];
                        var active = new Active("", json.symbolName, json.ofr, json.bid);
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
    
    const contents = ()=>{
        let res = actives.map((active)=>{
        return ( 
                <tr key = { active.id } >
                <td> {active.name}</td>
                <td> {active.ticket} </td>
                <td> { active.buy_price.toFixed(4) } </td>
                <td> { active.sell_price.toFixed(4) } </td> 
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
            <th> Sell </th>
            </tr> 
            </thead>
            <tbody> 
            {loaded ? contents() : null}
            </tbody>
            </table>
        </div>
    );
}