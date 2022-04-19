import React, { useState, useEffect } from 'react';
import { Modal, Button } from 'react-bootstrap';
import ActivesList from './ActivesList';
import Active from '../Models/Active';
import DealForm from './DealForm';

export default function Trading(){
    const [ actives, setActives] = useState(null);
    const [ loaded, setLoaded ] = useState(false);
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

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

    return(
        <div>
            {loaded ? <ActivesList actives={actives}/> : null}
            <Button variant="primary" onClick={handleShow}>
             Launch demo modal
            </Button>
            <Modal 
            show={show} 
            onHide={handleClose}
            >
            <Modal.Header closeButton>
                <Modal.Title>Открыть сделку</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <label>Количество</label>
                <input type="number"></input>
                <br/>
                <label>Маржа</label>
                <select>
                    <option value={1}>x1</option>
                    <option value={2}>x2</option>
                    <option value={5}>x5</option>
                    <option value={10}>x10</option>
                </select>
                <br/>
                <label>Открыть сделку по цене</label>
                <input type="number"></input>
                <br/>
                <label>Стоп-лосс</label>
                <input type="number"></input>
                <br/>
                <label>Тейк-профит</label>
                <input type="number"></input>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="primary" onClick={handleClose}>
                    Save Changes
                </Button>
            </Modal.Footer>
        </Modal>
        </div>
    )
}