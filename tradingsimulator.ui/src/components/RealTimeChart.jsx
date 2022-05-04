import { useEffect, useState } from "react";
import Chart from "react-google-charts";
import { HubConnectionBuilder } from '@microsoft/signalr';

export default function RealTimeChart(props){
    const [active, setActive] = useState();
    const ticket = props.ticket;

    const [chartData, setChartData] = useState(null);
    const [loaded, setLoaded] = useState(false);
    const[connection, setConnection] = useState(null);
    
    const options = {
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

    useEffect(() => {
      const newConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7028/rates')
      .withAutomaticReconnect()
      .build();
      setConnection(newConnection);
      const res = [];
      res.unshift(["time","price"]);
      setChartData(res);
      setLoaded(true); 

      return ()=> newConnection.stop();
  },[]);

  useEffect(async () => {
      if (connection) {
          try{
              await connection.start();
              console.log('Connected!');
              connection.on("SendRates", message =>{
                    const json = JSON.parse(message).payload;
                    const new_data = {};
                    new_data.ticket = json.symbolName;
                    new_data.buy_price = json.ofr;
                    new_data.sell_price = json.bid;
                    setActive(new_data);        
              });
          }
          catch(e){
              console.log(e);
          }
      }
  }, [connection]);

    useEffect(()=>{
      if(active && chartData){
        const new_data = active;

        if(chartData[chartData.length - 1][1] !== new_data.buy_price && new_data.ticket == ticket){
          const new_arr = [...chartData];
          const chart_item = [new Date(), new_data.buy_price];
          new_arr.push(chart_item);

          if(new_arr.length > 100){
            new_arr.splice(1,1);
          }
          console.log(new_arr.length);
          setChartData(new_arr);
        }
      }
    },[active]);

      return(loaded ?
        <div>
          <Chart
            chartType="LineChart"
            data={chartData}
            options={options}
          />
        </div> : <div>Loading...</div>
        
      )
}