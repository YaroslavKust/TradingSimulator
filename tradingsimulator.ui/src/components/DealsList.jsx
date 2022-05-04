import React, { useContext, useState, useEffect } from "react";
import { UserContext } from "../UserContext";
import Active from '../Models/Active';
import { HubConnectionBuilder } from '@microsoft/signalr';

export default function DealsList(props){
    const deals = props.deals;
    const updateDeals = props.updateDeals;
    const [ loaded, setLoaded ] = useState(false);
    const [ actives, setActives] = useState(null);
    const updateUserData = useContext(UserContext);
    const[connection, setConnection] = useState(null);

    useEffect(() => {
        let newConnection;
        const fetchData = async () =>{
            const resp = await fetch('https://localhost:7028/api/actives');
            const data = await resp.json();
            const results = [];
            for(let item of data){
                const result = new Active(item.name, item.ticket, item.lastAsk, item.lastBid);
                result.id = item.id;
                results.push(result);
            }
            setActives(results);

            newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:7028/rates')
            .withAutomaticReconnect()
            .build();
            setConnection(newConnection);
            setLoaded(true);
        }
        
        fetchData().catch(console.error);

        return ()=> newConnection.stop();
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

    async function CloseDeal(id, closePrice){
        await fetch('https://localhost:7028/api/deals/close', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            "Authorization": "Bearer " + localStorage.getItem("access_token")
          },
          body: JSON.stringify({id, closePrice})
        });
        alert("deal closed");
        await updateDeals();
        await updateUserData();
    }

    return(loaded ? <div>
        <table className = 'table table-striped table-dark' aria-labelledby = "tabelLabel">
            <thead>
            <tr id={0}>
                <th>Тикер</th>
                <th>Тип</th>
                <th>Цена открытия</th>
                <th>Количество</th>
                <th>Маржа</th>
                <th>Стоп-лосс</th>
                <th>Тейк-профит</th>
                <th>Цена закрытия</th>
                <th>Статус</th>
                <th></th>
            </tr>
            </thead>
            <tbody>
        {
        deals.map((deal)=>{
            if(actives){
                const closePrice = deal.closePrice == 0 ? actives.find(a=>a.ticket == deal.active.ticket).buy_price : deal.closePrice;
                return(
                    <tr id={deal.id}>
                        <td>{deal.active.ticket}</td>
                        <td>{deal.count > 0 ? "Лонг" : "Шорт"}</td>
                        <td>{deal.openPrice}</td>
                        <td>{deal.count > 0 ? deal.count : deal.count * -1}</td>
                        <td>x{deal.marginMultiplier}</td>
                        <td>{deal.stopLoss}</td>
                        <td>{deal.takeProfit}</td>
                        <td>{closePrice.toFixed(4)}</td>
                        <td>{deal.status}</td>
                        <td><button 
                            onClick={()=>CloseDeal(deal.id, closePrice)} disabled={deal.status == "Close"}
                            className='button button-blue'
                            >Закрыть</button></td>
                    </tr>
                )}})}
        </tbody>
        </table>
    </div>: <div>Loading...</div>);
}