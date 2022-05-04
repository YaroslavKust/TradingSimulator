import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { TradingContext } from '../TradingContext';

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
                <td style={{color: active.buy_color}}> { active.buy_price.toFixed(4) } </td>
                <td>
                    <button 
                        onClick={()=>props.openDeal(active.ticket, active.buy_price, "buy", active.id)}
                        className='button button-green'
                    >Купить
                    </button>
                </td>
                <td style={{color: active.sell_color}}> { active.sell_price.toFixed(4) } </td>
                <td>
                    <button 
                        onClick={()=>props.openDeal(active.ticket, active.sell_price, "sell", active.id)}
                        className='button button-red'
                        >Продать
                    </button>
                </td>
                </tr>);
        });
        return res;
    }
    
    return (
        <div>    
            <table className = 'table table-striped table-dark' aria-labelledby = "tabelLabel">
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