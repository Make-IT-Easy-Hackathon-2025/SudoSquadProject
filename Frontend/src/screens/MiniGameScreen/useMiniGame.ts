import { useState } from "react";
import { post } from "../../services/APIService";
import { Quiz } from "../../utils/types";

const sampleQuiz: Quiz = {
    id: 1,
    title: "World Geography Quiz",
    description: "Test your knowledge of world geography with these challenging questions!",
    questions: [
      {
        id: 1,
        value: "Which is the largest ocean on Earth?",
        options: [
          { id: 101, optionValue: "Atlantic Ocean", isCorrect: false },
          { id: 102, optionValue: "Indian Ocean", isCorrect: false },
          { id: 103, optionValue: "Pacific Ocean", isCorrect: true },
          { id: 104, optionValue: "Arctic Ocean", isCorrect: false }
        ]
      },
      {
        id: 2,
        value: "Which country has the longest coastline in the world?",
        options: [
          { id: 201, optionValue: "Russia", isCorrect: false },
          { id: 202, optionValue: "Canada", isCorrect: true },
          { id: 203, optionValue: "Australia", isCorrect: false },
          { id: 204, optionValue: "United States", isCorrect: false }
        ]
      },
      {
        id: 3,
        value: "What is the capital city of Brazil?",
        options: [
          { id: 301, optionValue: "Rio de Janeiro", isCorrect: false },
          { id: 302, optionValue: "São Paulo", isCorrect: false },
          { id: 303, optionValue: "Brasília", isCorrect: true },
          { id: 304, optionValue: "Salvador", isCorrect: false }
        ]
      },
      {
        id: 4,
        value: "Which African country is known as the 'Land of a Thousand Hills'?",
        options: [
          { id: 401, optionValue: "Kenya", isCorrect: false },
          { id: 402, optionValue: "Ethiopia", isCorrect: false },
          { id: 403, optionValue: "Rwanda", isCorrect: true },
          { id: 404, optionValue: "Tanzania", isCorrect: false }
        ]
      },
      {
        id: 5,
        value: "The city of Istanbul straddles which two continents?",
        options: [
          { id: 501, optionValue: "Europe and Africa", isCorrect: false },
          { id: 502, optionValue: "Europe and Asia", isCorrect: true },
          { id: 503, optionValue: "Asia and Africa", isCorrect: false },
          { id: 504, optionValue: "Europe and Australia", isCorrect: false }
        ]
      },
      {
        id: 6,
        value: "Which is the smallest country in the world by land area?",
        options: [
          { id: 601, optionValue: "Monaco", isCorrect: false },
          { id: 602, optionValue: "Liechtenstein", isCorrect: false },
          { id: 603, optionValue: "San Marino", isCorrect: false },
          { id: 604, optionValue: "Vatican City", isCorrect: true }
        ]
      },
      {
        id: 7,
        value: "The Great Barrier Reef is located off the coast of which country?",
        options: [
          { id: 701, optionValue: "Mexico", isCorrect: false },
          { id: 702, optionValue: "Philippines", isCorrect: false },
          { id: 703, optionValue: "Australia", isCorrect: true },
          { id: 704, optionValue: "Indonesia", isCorrect: false }
        ]
      },
      {
        id: 8,
        value: "Which mountain range separates Europe from Asia?",
        options: [
          { id: 801, optionValue: "Alps", isCorrect: false },
          { id: 802, optionValue: "Himalayas", isCorrect: false },
          { id: 803, optionValue: "Andes", isCorrect: false },
          { id: 804, optionValue: "Ural Mountains", isCorrect: true }
        ]
      }
    ]
};


export const useMiniGame = () => {
    console.log("UseMiniGame called");
    const [ topic, setTopic] = useState<string>("");
    const [ loading, setLoading] = useState<boolean>(false);
    const [ error, setError] = useState<string>("");
    const [quiz, setQuiz] = useState<Quiz[]>([]);

    // console.log("UseMiniGame called");

    const generate = async () => {
        // console.log("Generate function called");
        try{
            setLoading((prev) => !prev);
             const response: Quiz[]  = await post("/quizes/prompt", {keyword: topic, useAi: true});
            //  console.log(response);
             if(response){
                //  console.log(response);
                    // setQuiz(response);
             }
            setQuiz([sampleQuiz]);
            // console.log("Ez itt a topicaaaaaa: ", topic);
            setLoading((prev) => !prev);
        }catch(e: any){
            setError(e.message);
        }
        
       
    }

    const saveQuizInMemory = (quiz: Quiz) => {
        console.log(quiz);
    }


    return {
        topic, setTopic, generate, loading, error, quiz, saveQuizInMemory}
}