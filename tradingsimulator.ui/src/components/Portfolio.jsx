import React, { useState, useEffect } from 'react';

export default function Portfolio(props){
    const [loaded, setLoaded] = useState(false);
    const [deals, setDeals] = useState(null);

    useEffect( async () => {
        const deals_response = await fetch('https://localhost:7028/api/deals', {
            method: 'GET',
            headers:{
                "Authorization": "Bearer " + localStorage.getItem("access_token")
            }
        });

        const deals_json = await deals_response.json();
        setDeals(deals_json.reverse());
        setLoaded(true);
    },[]);

if(loaded){
    return(
    <div>
        <h3>Portoflio</h3>
        <div>
            <label>Баланс: {props.profile.balance}</label><br/>
            <label>Кредит: {props.profile.debt}</label><br/>
            <label>Залог: {props.profile.deposit}</label><br/>
        </div>
        <h4>Сделки</h4>
        <div>
            <table>
                <tr>
                    <th>Цена открытия</th>
                    <th>Количество</th>
                    <th>Маржа</th>
                    <th>Цена закрытия</th>
                </tr>
            {
            deals.map((deal)=>{
                return(
                    <tr>
                        <td>{deal.openPrice}</td>
                        <td>{deal.count}</td>
                        <td>{deal.marginMultiplier}</td>
                        <td>{deal.stopLoss}</td>
                        <td>{deal.takeProfit}</td>
                    </tr>
                )
            })}
            </table>
        </div>
    </div>
    )
        }

return null;
}