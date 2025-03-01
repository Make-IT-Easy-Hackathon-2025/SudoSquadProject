import { useState } from "react";


export const useMiniGame = () => {
    const [topic, setTopic] = useState<string>("");

    

    return {
        topic, setTopic}
}