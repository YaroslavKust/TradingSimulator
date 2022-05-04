import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import RealTimeChart from "./RealTimeChart";
import HistoricalChart from "./HistoricalChart";

export default function ChartPage(){
    const params = useParams();
    const ticket = params.ticket;
    const [period, setPeriod] = useState("1m");

    const chart = () =>{
      return period == "stream" ?
        <RealTimeChart ticket={ticket}/>:
        <HistoricalChart ticket={ticket} period={period}/>
    }

      return(
        <div>
          <h1>{ticket}</h1>
          <label>Интервал</label><br></br>
          <select defaultValue={"1m"} onChange={e => setPeriod(e.target.value)} >
            <option value={"stream"}>В реальном времени</option>
            <option value={"1m"}>Минута</option>
            <option value={"1h"}>Час</option>
            <option value={"1d"}>День</option>
            <option value={"1w"}>Неделя</option>
          </select>
          {chart()}
        </div> 
      )
}