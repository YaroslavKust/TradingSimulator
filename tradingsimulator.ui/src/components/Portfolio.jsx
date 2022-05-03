import React, { useState, useEffect } from 'react';
import Chart from 'react-google-charts';
import DealsList from './DealsList';

export default function Portfolio(props){
    const loaded = props.loaded;
    const [dealsLoaded, setDealsLoaded] = useState(false);
    const [deals, setDeals] = useState(null);
    const [statistic, setStatistic] = useState(null);

    const updateDeals = async () =>{
        const deals_response = await fetch('https://localhost:7028/api/deals', {
            method: 'GET',
            headers:{
                "Authorization": "Bearer " + localStorage.getItem("access_token")
            } 
        });

        const deals_json = await deals_response.json();
        setDeals(deals_json.reverse());

        console.log("deals updated");
        setDealsLoaded(true);
    }

    useEffect(async ()=>{
        const statistic_response = await fetch('https://localhost:7028/api/deals/statistic', {
            method: 'GET',
            headers:{
                "Authorization": "Bearer " + localStorage.getItem("access_token")
            } 
        });

        const stat_json = await statistic_response.json();
        let arr = stat_json.map(stat=>{
            return [stat.type, stat.sum];
        });
        arr.unshift(["active type","sum"]);
        setStatistic(arr);
        console.log(stat_json);
        await updateDeals();
    },[]);

    if(loaded && dealsLoaded){
        return(
        <div>
            <h3>Portoflio</h3>
            <div style={{display: "flex"}}>
                <div>
                    <label>Баланс: {props.profile.balance}</label><br/>
                    <label>Кредит: {props.profile.debt}</label><br/>
                    <label>Залог: {props.profile.deposit}</label><br/>
                </div>
                <div style={{height: 300}}>
                    <Chart 
                        chartType='PieChart' 
                        options={
                            {pieHole: 0.6, 
                                height: "100%", 
                                chartArea: {width: '90%', height: '100%'},
                                backgroundColor: { fill:'transparent' },
                                annotations: {
                                    textStyle:{color: '#FFF'}
                                  }
                            }} 
                        data={statistic}
                        />
                </div>
            </div>
            <h4>Сделки</h4>
            <DealsList deals={deals} updateDeals={updateDeals}/>
        </div>
        )
    }
    else{
        return (<div>Loading...</div>)
    }
}