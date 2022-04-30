import React, { useState, useEffect } from 'react';
import { Modal, Button } from 'react-bootstrap';
import ActivesList from './ActivesList';
import Active from '../Models/Active';
import DealForm from './DealForm';

export default function Trading(props){
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return(
        <div>
        {props.loaded ? <ActivesList loaded={props.loaded} openDeal={handleShow}/> : null}
        <Button variant="primary" onClick={handleShow}>
         Launch demo modal
        </Button>
        <DealForm show={show} handleClose={handleClose}></DealForm>
    </div>
    )
}