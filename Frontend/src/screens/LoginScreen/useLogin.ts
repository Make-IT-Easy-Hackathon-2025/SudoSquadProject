import { useState } from "react";
import { useAuth } from "../../contexts/AuthContext";

interface ErrorType {
    [key: string]: string;
}

export const useLogin = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState<ErrorType>({});

    const auth = useAuth();

    const userLogin = async () => {
        if(email !== "" && password !== "") {
            if(auth && auth.login){
                await auth.login({ email, password });
            }
        }
    }

    return { email, setEmail, password, setPassword, userLogin, error, setError };
};
