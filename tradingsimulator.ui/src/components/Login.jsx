import React, {useState} from "react";
import { useNavigate } from "react-router-dom";

export default function Login({ setLoggedIn }) {
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();
    const navigate = useNavigate();

    async function loginUser(credentials) {
        let response = await fetch('https://localhost:7028/api/account/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(credentials)
        });
        return await response.json();
    }

    const handleSubmit = async (e) =>{
        e.preventDefault();
        let response = await loginUser({email, password});
        localStorage.setItem("access_token",response.token);
        setLoggedIn();
        navigate("/trading");
        }

    const handleRegisterRedirect = (e) =>{
      e.preventDefault();
      navigate("/registration");
    }

    return(
      <div className="login-container">
      <form onSubmit={handleSubmit} className="login">
          <h2>Вход в систему</h2>
          <label>Email</label><br/>
          <input type="text" onChange={e => setEmail(e.target.value)} required className="deal-from__input"/><br/>
          <label>Пароль</label><br/>
          <input type="password" onChange={e => setPassword(e.target.value)} required className="deal-from__input"/>
        <div className="account-button-conatiner">
          <button type="submit" className="button button-blue button-login">Вход</button>
          <button className="button button-green button-register" onClick={handleRegisterRedirect}>Регистрация</button>
        </div>
      </form>
      </div>
    )
}