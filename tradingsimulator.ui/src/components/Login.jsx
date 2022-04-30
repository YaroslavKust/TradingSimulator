import React, {useState} from "react";
import { useNavigate } from "react-router-dom";

export default function Login({ setToken }) {
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

    const handleSubmit = (e) =>{
        e.preventDefault();
        loginUser({email, password}).then(response=>{
            localStorage.setItem("access_token",response.token);
          });
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