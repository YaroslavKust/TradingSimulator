import React, { useState, useEffect } from 'react';
import Active from './Models/Active';
import ActivesList from './components/ActivesList';

export default function App(){
    const [ actives, setActives] = useState(null);
    const [ loaded, setLoaded ] = useState(false);

    useEffect(async ()=>{
        const response = await fetch('https://localhost:7028/api/actives');
        const data = await response.json();
        const results = [];
        for(let item of data){
            const result = new Active(item.name, item.ticket, 0, 0);
            result.id = item.id;
            results.push(result);
        }
        setActives(results);
        setLoaded(true);
    }, []);
    
    return (
        <div>
           {loaded ? <ActivesList actives={actives}></ActivesList> : null }
        </div>
    );
}