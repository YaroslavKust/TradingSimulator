import React, { useState, useEffect } from "react";

export default function CloseDealsList(){
    const [loaded, setLoaded] = useState(false);
    const [deals, setDeals] = useState();

    useEffect(() => {
        const fetchData = async () =>{
            const deals_response = await fetch('https://localhost:7028/api/deals/historical', {
                method: 'GET',
                headers:{
                    "Authorization": "Bearer " + localStorage.getItem("access_token")
                } 
            });
            const deals_json = await deals_response.json();
            setDeals(deals_json.reverse());
            setLoaded(true);
        }
        
        fetchData().catch(console.error);
    },[]);

    return(loaded ? <div>
        <table className = 'table table-striped' aria-labelledby = "tabelLabel">
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
                <th>Доход</th>
                <th></th>
            </tr>
            </thead>
            <tbody>
        {
        deals.map((deal)=>{
                const diff = ((deal.closePrice - deal.openPrice)*deal.count).toFixed(4);
                return(
                    <tr id={deal.id}>
                        <td>{deal.active.ticket}</td>
                        <td>{deal.count > 0 ? "Лонг" : "Шорт"}</td>
                        <td>{deal.openPrice}</td>
                        <td>{deal.count > 0 ? deal.count : deal.count * -1}</td>
                        <td>x{deal.marginMultiplier}</td>
                        <td>{deal.stopLoss}</td>
                        <td>{deal.takeProfit}</td>
                        <td>{deal.closePrice}</td>
                        <td>{diff}</td>
                        <td>{(diff/(deal.openPrice*Math.abs(deal.count))*100).toFixed(4)}%</td>
                    </tr>
                )})}
        </tbody>
        </table>
    </div>: <div>Loading...</div>);
}