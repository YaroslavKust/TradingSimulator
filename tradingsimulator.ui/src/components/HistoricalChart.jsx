import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Chart from "react-google-charts";

export default function HistoricalChart(props){
    const ticket = props.ticket;

    const lineOptions = {
        title: ticket,
        curveType: 'function',
        width: '100%',
        height: '600',
        chartArea: {width: '90%', height: '85%'},
        legend: { position: 'bottom' },
        backgroundColor: { fill: '#212529' },
        colors: ['red'],
        hAxis: {
            textStyle:{color: '#FFF'}
          },
          vAxis: {
            textStyle:{color: '#FFF'}
          } 
      };
  
      const candlesOptions = {
          title: ticket,
          bar: { groupWidth: "100%" },
          candlestick: {
          fallingColor: { strokeWidth: 0, fill: "red" }, 
          risingColor: { strokeWidth: 0, fill: "green" }, 
          },
          width: '100%',
          height: '600',
          chartArea: {width: '90%', height: '85%'},
          legend: "none",
          hAxis: {
            textStyle:{color: '#FFF'}
          },
          vAxis: {
            textStyle:{color: '#FFF'}
          },
          backgroundColor: { fill: '#212529' },
      }

    const [chartData, setChartData] = useState(null);
    const [loaded, setLoaded] = useState(false);
    const [chartType, setChartType] = useState("LineChart");
    const [options, setOptions] = useState(lineOptions);

    useEffect(async ()=>{
      let ticketName = ticket;

      if(ticketName.includes('/')){
          ticketName = ticketName.replace('/',"%2F");
      }

      const response = await fetch(`https://localhost:7028/api/Chart/${ticketName}`);
      const dataRes = await response.json();
      const resArray = dataRes.result[ticket];

      let res = resArray.map(data=>{
          return [new Date(data[0] * 1000), parseFloat(data[4])];
      });

      res.unshift(["time","price"]);
      setChartData(res);
      setLoaded(true);
    },[]);

    useEffect( async ()=>{
        let ticketName = ticket;
        let res;

        if(ticketName.includes('/')){
            ticketName = ticketName.replace('/',"%2F");
        }

        const response = await fetch(`https://localhost:7028/api/Chart/${ticketName}`);
        const dataRes = await response.json();
        const resArray = dataRes.result[ticket];

        if(chartType == "LineChart"){
            res = resArray.map(data=>{
                return [new Date(data[0] * 1000), parseFloat(data[4])];
            });
            res.unshift(["time","price"]);
            setOptions(lineOptions);
        }
        else{
            res = resArray.map(data=>{
                return [new Date(data[0] * 1000), 
                parseFloat(data[3]), 
                parseFloat(data[1]), 
                parseFloat(data[4]), 
                parseFloat(data[2])];
            });
            res.unshift(["time","","","",""]);
            if(res.length > 201){
                res = res.slice(0,201);
            }
            setOptions(candlesOptions);

        }
        setChartData(res);
        setLoaded(true);
    },[chartType])

      return(
        <div>
            <p>Интервал</p>
            <select onChange={e => setChartType(e.target.value)}>
                <option value={"LineChart"}>Линейный</option>
                <option value={"CandlestickChart"}>Свечи</option>
            </select>
            {loaded ? 
                <Chart
                    chartType={chartType}
                    data={chartData}
                    options={options}
                /> 
                : <div>Loading...</div>}
        </div>      
      )
}