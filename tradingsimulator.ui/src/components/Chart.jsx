import { useEffect, useState, useContext } from "react";
import { useParams } from "react-router-dom";
import useGoogleCharts from '../useGoogleCharts';
import { TradingContext } from '../TradingContext';

export default function ChartPage(props){
   const [actives, setActives] = useContext(TradingContext);
    const google = useGoogleCharts();
    const params = useParams();
    const ticket = params.ticket;

    const [chart, setChart] = useState(null);
    const [chartData, setChartData] = useState(null);
    const [loaded, setLoaded] = useState(false);
    
    useEffect(async ()=>{
      let ticketName = ticket;
      if(ticketName.includes('/')){
          ticketName = ticketName.replace('/',"%2F");
      }
      const response = await fetch(`https://localhost:7028/api/Chart/${ticketName}`);
      const dataRes = await response.json();
      const resArray = dataRes.result[ticket];
      const res = [];
      // const res = resArray.map(data=>{
      //     return [new Date(data[0] * 1000), parseFloat(data[4])];
      // });

      res.unshift(["time","price"]);
      setChartData(res);
      setLoaded(true);
    },[]);

    useEffect(()=>{
      if(actives && chartData){
        const new_data = actives.find((active)=>active.ticket == ticket);
        const new_arr = [...chartData];
        const chart_item = [new Date(), new_data.buy_price];

        if(new_arr[new_arr.length - 1][1] !== chart_item[1]){
          new_arr.push(chart_item);
          setChartData(new_arr);
          let data = google.visualization.arrayToDataTable(chartData);
          let options = {
            title: ticket,
            curveType: 'function',
            width: '90%',
            height: '600',
            chartArea: {width: '90%', height: '85%'},
            legend: { position: 'bottom' }
          };
  
          let newChart = new google.visualization.LineChart(document.getElementById('curve_chart'));
          newChart.draw(data, options);
          setChart(newChart);
        }
      }
    },[actives]);

    useEffect(()=>{
        if(google && !chart && loaded){
          let data = google.visualization.arrayToDataTable(chartData);
            console.log(chartData);
            let options = {
              title: 'Company Performance',
              curveType: 'function',
              width: '90%',
              height: '600',
              chartArea: {width: '90%', height: '85%'},
              legend: { position: 'bottom' }
            };
    
            let newChart = new google.visualization.LineChart(document.getElementById('curve_chart'));
            newChart.draw(data, options);
            setChart(newChart);
        }
    },[chartData, chart]);

      return(
        <div>
          <div id="curve_chart"></div>
        </div>
        
      )
}