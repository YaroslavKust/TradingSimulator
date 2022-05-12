import React, { useState, useEffect } from 'react';
import Chart from 'react-google-charts';
import DealsList from './DealsList';
import CloseDealsList from './CloseDealsList';

export default function Portfolio(props){
    const loaded = props.loaded;
    const [dealsLoaded, setDealsLoaded] = useState(false);
    const [deals, setDeals] = useState(null);
    const [statistic, setStatistic] = useState(null);
    const [income, setIncome] = useState(null);
    const [dealType, setDealType] = useState("open");

    const chartOptions = {
        legend: {textStyle: { fontSize: 16}},
        pieHole: 0.5,
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

        const income_response = await fetch('https://localhost:7028/api/account/income/week', {
            method: 'GET',
            headers:{
                "Authorization": "Bearer " + localStorage.getItem("access_token")
            } 
        });

        const income_json = await income_response.json();
        const stat_json = await statistic_response.json();
        let arr = stat_json.map(stat=>{
            return [stat.type, stat.sum];
        });
        arr.unshift(["active type","sum"]);
        setStatistic(arr);
        setIncome(income_json);
        await updateDeals();
    },[]);

    const dealsData = () =>{
        if(dealType == "open"){
            return <DealsList deals={deals} updateDeals={updateDeals}/>;
        }
        else if(dealType == "close"){
            return <CloseDealsList deals={deals}/>
        }
    }

    if(loaded && dealsLoaded){
        return(
        <div>
            <div style={{display: "flex", justifyContent: "space-around"}}>
                <div>
                    <h2 className='fin-head'>Финансы</h2>
                    <h3>Баланс: {props.profile.balance}</h3>
                    <h3>Кредит: {props.profile.debt}</h3>
                    <h3>Залог: {props.profile.deposit}</h3>
                </div>
                <div className='statistic-container'>
                    <h2 className='fin-head' style={{textAlign: 'center'}}>Активы</h2>
                    <div style={{height: 350, width: 600}}>
                        <Chart 
                            chartType='PieChart' 
                            options={chartOptions} 
                            data={statistic}
                            />
                    </div>
                </div>
                <div>
                    <h2 className='fin-head'>Доходность</h2>
                    <h3>Сделок: {income.dealsCount}</h3>
                    <h3>Успешных: {income.dealsSuccessed}</h3>
                    <h3>Неудачных: {income.dealsFailed}</h3>
                    <h3>Доход: {income.income} ({income.incomePercents.toFixed(4)}%)</h3>
                </div>
            </div>
            <h2 style={{margin: 10}}>Сделки</h2>
            <select defaultValue="open" onChange={e => setDealType(e.target.value)}>
                <option value="open">Открытые</option>
                <option value="close">Закрытые</option>
            </select>
            {dealsData()}
        </div>
        )
    }
    else{
        return (<div>Loading...</div>)
    }
}