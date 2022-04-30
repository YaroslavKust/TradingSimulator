import { useState } from "react";
import { Modal, Button } from "react-bootstrap";

export default function DealForm(props){
    const[count, setCount] = useState(0);
    const[marginMultiplier, setMargin] = useState(1);
    const[openPrice, setOpenPrice] = useState(0);
    const[stopLoss, setStopLoss] = useState(0);
    const[takeProfit, setTakeProfit] = useState(0);
    const[processPemanent, setProcessPremanent] = useState(true);

    const handleClose = props.handleClose;
    const show = props.show;

    const sendDeal = async () =>{
        const credentials = {
            activeId: 1,
            count,
            marginMultiplier,
            openPrice: 100,
            stopLoss,
            takeProfit,
            processPemanent,
            status: 0
        }

        await fetch('https://localhost:7028/api/deals/open', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            "Authorization": "Bearer " + localStorage.getItem("access_token")
          },
          body: JSON.stringify(credentials)
        });

        handleClose();
    }

    return(
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Открыть сделку {props.active}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <label>Количество</label><br/>
                <input type="number" onChange={e => setCount(e.target.value)}></input>
                <br/>
                <label>Маржа</label><br/>
                <select onChange={e => setMargin(e.target.value)}>
                    <option value={1}>x1</option>
                    <option value={2}>x2</option>
                    <option value={5}>x5</option>
                    <option value={10}>x10</option>
                </select>
                <br/>
                <label>Открыть сделку по цене</label><br/>
                <input type="number" onChange={e => setOpenPrice(e.target.value)}></input>
                <br/>
                <label>Стоп-лосс</label><br/>
                <input type="number" onChange={e => setStopLoss(e.target.value)}></input>
                <br/>
                <label>Тейк-профит</label><br/>
                <input type="number" onChange={e => setTakeProfit(e.target.value)}></input>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="primary" onClick={sendDeal}>
                    Save Changes
                </Button>
            </Modal.Footer>
        </Modal>)
}