import React, { useState, useEffect } from "react";

export default function OperationsList(){
    const [loaded, setLoaded] = useState(false);
    const [operations, setOPerations] = useState();

    useEffect(() => {
        const fetchData = async () =>{
            const operations_response = await fetch('https://localhost:7028/api/operations', {
                method: 'GET',
                headers:{
                    "Authorization": "Bearer " + localStorage.getItem("access_token")
                } 
            });
            const operations_json = await operations_response.json();
            setOPerations(operations_json.reverse());
            setLoaded(true);
        }
        
        fetchData().catch(console.error);
    },[]);

    return(loaded ? <div>
        <table className = 'table table-striped' aria-labelledby = "tabelLabel">
            <thead>
            <tr id={0}>
                <th>Дата</th>
                <th>Тип</th>
                <th>Сумма</th>
            </tr>
            </thead>
            <tbody>
        {
        operations.map((operation)=>{
                const date = Date.parse(operation.date);
                const datef = new Date(date).toLocaleString("ru");
                return(
                    <tr id={operation.id}>
                        <td>{datef}</td>
                        <td>{operation.type}</td>
                        <td style={{color: operation.sum > 0 ? 'green' : 'red'}}>{operation.sum}</td>
                    </tr>
                )})}
        </tbody>
        </table>
    </div>: <div>Loading...</div>);
}