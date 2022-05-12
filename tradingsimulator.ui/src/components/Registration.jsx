import React, {useState} from "react";
import { useNavigate } from "react-router-dom";

export default function Registration() {
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();
    const [confirmedPassword, setConfirmedPassword] = useState();
    const [isRegistered, setRegistered] = useState(false);

    const navigate = useNavigate();

    if(isRegistered){
        return navigate("/login");
    }

    async function registerUser(credentials) {
        let response = await fetch('https://localhost:7028/api/account/registration', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(credentials)
        });
        return response.status;
    }

    const handleSubmit = async (e) =>{
      e.preventDefault();
        console.log("a");
        if(password == confirmedPassword){
            console.log("hi");
            let response = await registerUser({email, password});
            if(response == 201)
                setRegistered(true);
        }
    }

    return(
      <div className="login-container">
      <form onSubmit={handleSubmit} className="login">
        <h2>Регистрация</h2>
          <label>Email</label><br/>
          <input type="text" onChange={e => setEmail(e.target.value)} required className="deal-from__input"/><br/>
          <label>Пароль</label><br/>
          <input type="password" onChange={e => setPassword(e.target.value)} required className="deal-from__input"/><br/>
          <label>Повторите пароль</label><br/>
          <input type="password" onChange={e => setConfirmedPassword(e.target.value)} required className="deal-from__input"/><br/>
        <div className="account-button-conatiner">
          <button type="submit" className="button button-green">Зарегистрироваться</button>
        </div>
      </form>
      </div>
    )
}