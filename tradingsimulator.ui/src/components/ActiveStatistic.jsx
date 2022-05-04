import React, { useState, useEffect } from 'react';

export function ActiveStatistic(props){
    const [response, setResponse] = useState(false);
    const ticket = props.ticket;

    useEffect(()=>{
        const fetchData = async () =>{
            const resp = await fetch('https://localhost:7028/api/actives');
            const data = await resp.json();
        }
    },[]);
}