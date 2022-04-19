import { Modal, Button } from "react-bootstrap";

export default function DealForm(props){

    const handleClose = props.handleClose;
    const show = props.show;

    return(
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Открыть сделку {props.active}</Modal.Title>
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
        </Modal>)
}