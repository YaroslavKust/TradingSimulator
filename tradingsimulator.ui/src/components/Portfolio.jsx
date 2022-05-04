import React, { useState, useEffect } from 'react';
import Chart from 'react-google-charts';
import DealsList from './DealsList';

export default function Portfolio(props){
    const loaded = props.loaded;
    const [dealsLoaded, setDealsLoaded] = useState(false);
    const [deals, setDeals] = useState(null);
    const [statistic, setStatistic] = useState(null);

    const chartOptions = {
        legend: {textStyle: {color: "#FFF", fontSize: 16}},
        pieHole: 0.4,
        height: "100%", 
        chartArea: {width: '90%', height: '90%'},
        backgroundColor: { fill:'transparent' },
    }

    const updateDeals = async () =>{
        const deals_response = await fetch('https://localhost:7028/api/deals', {
            method: 'GET',
            headers:{
                "Authorization": "Bearer " + localStorage.getItem("access_token")
            } 
        });

        const deals_json = await deals_response.json();
        setDeals(deals_json.reverse());
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
            <div style={{display: "flex"}}>
                <div>
                    <h3>Финансы</h3>
                    <h5>Баланс: {props.profile.balance}</h5>
                    <h5>Кредит: {props.profile.debt}</h5>
                    <h5>Залог: {props.profile.deposit}</h5>
                </div>
                <div>
                    <h3 style={{textAlign: 'center'}}>Активы</h3>
                    <div style={{height: 350, width: 600}}>
                        <Chart 
                            chartType='PieChart' 
                            options={chartOptions} 
                            data={statistic}
                            />
                    </div>
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