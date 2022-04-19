import React, { useState, useEffect } from 'react';

export default function Portfolio(){
    const [loaded, setLoaded] = useState(false);
    const [data, setData] =  useState(null);
    const [deals, setDeals] = useState(null);

    useEffect( async () => {
        const response = await fetch('https://localhost:7028/api/account/balance', {
            method: 'GET',
            headers:{
                "Authorization": "Bearer " + localStorage.getItem("access_token")
            }
        });
        let res = await response.json();
        console.log(res);
        setData(res);

        const deals_response = await fetch('https://localhost:7028/api/deals', {
            method: 'GET',
            headers:{
                "Authorization": "Bearer " + localStorage.getItem("access_token")
            }
        });

        const deals_json = await deals_response.json();
        console.log(deals_json);
        setDeals(deals_json);
        setLoaded(true);
    },[]);

if(loaded)
    return(
    <div>
        <h3>Portoflio</h3>
        <div>
            <label>Баланс: {data.balance}</label><br/>
            <label>Кредит: {data.debt}</label><br/>
            <label>Залог: {data.deposit}</label><br/>
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
            {deals.map((deal)=>{
                return(
                    <tr>
                        <td>{deal.openPrice}</td>
                        <td>{deal.count}</td>
                        <td>{deal.marginMultiplier}</td>
                        <td>{deal.closePrice}</td>
                    </tr>
                )
            })}
            </table>
        </div>
    </div>
    )

return null;
}