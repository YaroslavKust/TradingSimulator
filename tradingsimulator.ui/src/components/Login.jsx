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

    return(
      <form onSubmit={handleSubmit}>
        <label>
          <p>Email</p>
          <input type="text" onChange={e => setEmail(e.target.value)} required/>
        </label>
        <label>
          <p>Password</p>
          <input type="password" onChange={e => setPassword(e.target.value)} required/>
        </label>
        <div>
          <button type="submit">Submit</button>
        </div>
      </form>
    )
}