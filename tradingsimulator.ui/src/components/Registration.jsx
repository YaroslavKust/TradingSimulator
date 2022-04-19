import React, {useState} from "react";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react/cjs/react.production.min";

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

    const handleSubmit = async () =>{
        console.log("a");
        if(password == confirmedPassword){
            console.log("hi");
            let response = await registerUser({email, password});
            if(response == 201)
                setRegistered(true);
        }
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
        <label>
          <p>Confirm password</p>
          <input type="password" onChange={e => setConfirmedPassword(e.target.value)} required/>
        </label>
        <div>
          <button type="submit">Submit</button>
        </div>
      </form>
    )
}