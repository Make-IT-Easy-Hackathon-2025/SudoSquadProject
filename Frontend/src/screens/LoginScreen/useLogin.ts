import { useState } from "react";
import { useAuth } from "../../contexts/AuthContext";

export const useLogin = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState({email: "", password: ""});

    const userLogin = () => {
        if(email === "") {
            console.log("Email is required");
            setError({email: "Email is required", password: error.password});
            console.log("Email is required");
            console.log(error.email)
        } else {
            console.log("Email is valid");
            setError({email: "", password: error.password});
            console.log("Email is valid");
            console.log(error.email)
        }
        
        if(password === "") {
            console.log("Password is required");
            setError({email: error.email, password: "Password is required"});
        } else {
            console.log("Password is valid");
            setError({email: error.email, password: ""});
        }
        if(email !!== "" && password !== "") {
            const auth = useAuth();
        if(auth && auth.login){
            const response = auth.login({ email, password });
            console.log(response);
        }
        }

        

    }
    console.log(error)

    return { email, setEmail, password, setPassword, userLogin, error, setError };
};