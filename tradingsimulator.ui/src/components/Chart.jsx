import { useState } from "react";
import { useParams } from "react-router-dom";
import RealTimeChart from "./RealTimeChart";
import HistoricalChart from "./HistoricalChart";

export default function ChartPage(props){
    const params = useParams();
    const ticket = params.ticket;
    const [period, setPeriod] = useState("1m");

      return(
        <div>
          <select onChange={e => setPeriod(e.target.value)} >
            <option value={"stream"} >В реальном времени</option>
            <option value={"1m"} selected>Минута</option>
          </select>
          {period == "stream" ?
            <RealTimeChart ticket={ticket}/> :
            <HistoricalChart ticket={ticket}/>}
        </div> 
      )
}