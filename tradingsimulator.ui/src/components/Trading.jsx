import React, { useState, useEffect } from 'react';
import ActivesList from './ActivesList';
import DealForm from './DealForm';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { TradingContext } from '../TradingContext';
import Active from '../Models/Active';

export default function Trading(props){
    const [show, setShow] = useState(false);
    const[connection, setConnection] = useState(null);
    const [ loaded, setLoaded ] = useState(false);
    const [ actives, setActives] = useState(null);

    const [formData, setFormData] = useState({ticket: "", price: 0, side: "buy", activeId: 0});

    const handleClose = () => setShow(false);

    const openDeal = (ticket, price, side, activeId) => {
        setFormData({
            ticket, 
            price, 
            side,
            activeId
        });
        setShow(true);
    }

    useEffect( async () => {
        const resp = await fetch('https://localhost:7028/api/actives');
        const data = await resp.json();
        const results = [];
        for(let item of data){
            const result = new Active(item.name, item.ticket, item.lastAsk, item.lastBid);
            result.id = item.id;
            results.push(result);
        }
        setActives(results);

        const newConnection = new HubConnectionBuilder()
        .withUrl('https://localhost:7028/rates')
        .withAutomaticReconnect()
        .build();
        setConnection(newConnection);
        console.log("a");

        setLoaded(true);       
    },[]);

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

    return(
        <div>
        <TradingContext.Provider value={[actives, setActives]}>
            {loaded ? <ActivesList loaded={loaded} openDeal={openDeal}/> : <div>Loading...</div>}
            <DealForm show={show} handleClose={handleClose} formData = {formData} ></DealForm>
        </TradingContext.Provider>
    </div>
    )
}