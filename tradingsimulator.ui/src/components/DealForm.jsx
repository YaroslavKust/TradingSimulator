import { useState, useContext, useEffect } from "react";
import { Modal } from "react-bootstrap";
import { TradingContext } from '../TradingContext';
import { UserContext } from '../UserContext';

export default function DealForm(props){
    const [currPrice, setCurrPrice] = useState(props.formData.price);

    const[count, setCount] = useState(0);
    const[marginMultiplier, setMargin] = useState(1);
    const[openPrice, setOpenPrice] = useState(0);
    const[stopLoss, setStopLoss] = useState(0);
    const[takeProfit, setTakeProfit] = useState(0);
    const[executePermanently, setExecutePermanently] = useState(true);

    const [actives, setActives] = useContext(TradingContext);
    const updateUserData = useContext(UserContext);

    const handleClose = props.handleClose;
    const show = props.show;

    useEffect(()=>{
            if(props.formData.ticket != ""){
                let price = 0;
                let active = actives.find(a => a.ticket == props.formData.ticket);
                if(props.formData.side == "buy"){
                    price = active.buy_price;
                }     
                else{
                    price = active.sell_price;
                }
                if(price != currPrice){
                    setCurrPrice(price);
                }
            }
    },[actives]);

    const sendDeal = async () =>{
        let dealValue = count;
        if(props.formData.side == "sell"){
            dealValue *= -1;
        }

        const credentials = {
            activeId: props.formData.activeId,
            count: dealValue,
            marginMultiplier,
            openPrice: openPrice == 0 ? currPrice : openPrice,
            stopLoss,
            takeProfit,
            executePermanently,
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
        await updateUserData();
        setCount(0);
        setMargin(1);
        setStopLoss(0);
        setTakeProfit(0);
        setOpenPrice(0);
        setExecutePermanently(true);
        handleClose();
    }

    const handleSubmit = (e) =>{
        e.preventDefault();
        sendDeal();
    }

    const handleCheckBox = () =>{
        setExecutePermanently(!executePermanently);
    }

    return(
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton className='deal-form'>
                <Modal.Title>{props.formData.side == "buy" ? "Покупка" : "Продажа"} {props.formData.ticket}  {currPrice.toFixed(4)}</Modal.Title>
            </Modal.Header>
            <Modal.Body className='deal-form'>
                <div className='form-wrapper'>
                <form onSubmit={handleSubmit} className='deal-form__inner'>
                <label>Количество</label><br/>
                <input type="number" step="0.0001" className="deal-from__input" onChange={e => setCount(e.target.value)}></input>
                <br/>
                <label>Маржа</label><br/>
                <select onChange={e => setMargin(e.target.value)} className="deal-from__input">
                    <option value={1}>x1</option>
                    <option value={2}>x2</option>
                    <option value={5}>x5</option>
                    <option value={10}>x10</option>
                </select>
                <br/>
                <label>Открыть сделку по цене</label><br/>
                <input type="number" step="0.0001" className="deal-from__input" onChange={e => setOpenPrice(e.target.value)}></input>
                <br/>
                <label>Стоп-лосс</label><br/>
                <input type="number" step="0.0001" className="deal-from__input" onChange={e => setStopLoss(e.target.value)}></input>
                <br/>
                <label>Тейк-профит</label><br/>
                <input type="number" step="0.0001" className="deal-from__input" onChange={e => setTakeProfit(e.target.value)}></input>
                <br/>
                <input type="checkbox" checked={executePermanently} onChange={handleCheckBox} className='checkbox'/>
                <label>Автоматическое совершение сделки </label>
                <br></br><br></br>
                <button type="submit" 
                    className={
                        props.formData.side == "buy" ? "button submit-deal-button button-green" : "button submit-deal-button button-red"}>
                    {props.formData.side == "buy" ? "Купить" : "Продать"}
                </button>
                </form>
                </div>
            </Modal.Body>
        </Modal>)
}