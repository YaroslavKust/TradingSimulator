import React from "react";

export default function DealsListItem(props){
    const deal = props.deal;
    const updateDeals = props.updateDeals;

    async function CloseDeal(id, closePrice){
        await fetch('https://localhost:7028/api/deals/close', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            "Authorization": "Bearer " + localStorage.getItem("access_token")
          },
          body: JSON.stringify({id, closePrice})
        });
    }

    const handleCloseDeal = async (e)=>{
        e.preventDefault();
        await CloseDeal(deal.id, deal.closePrice);
        await updateDeals();
    }

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
                <td>{deal.status}</td>
                <td><form onSubmit={handleCloseDeal}><button type="submit">Закрыть</button></form></td>
            </tr>
            );
}