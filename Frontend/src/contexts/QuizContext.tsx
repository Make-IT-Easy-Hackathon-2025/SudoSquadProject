import React, {
  createContext,
  useState,
  useContext,
  PropsWithChildren,
} from "react";
import { Quiz, storeData, UserMemory } from "../utils";
import { post } from "../services/APIService";
import { getUserMemoryContext } from "./UserMemoryContext";

type QuizState = {
  topic: string;
  quiz: Quiz;
  loading: boolean;
  error: string;
};

type response = { quiz?: Quiz; userMemory?: UserMemory };

export type QuizProps = {
  quizState?: QuizState;
  generateQuiz?(): Promise<{ userMemory: UserMemory } | undefined>;
  saveQuizInMemory?(quiz: Quiz): void;
};

const QuizContext = createContext<QuizProps>({});

export const useQuiz = () => {
  return useContext(QuizContext);
};

export const QuizProvider = ({ children }: PropsWithChildren<{}>) => {
  const [quizState, setQuizState] = useState<QuizState>({
    topic: "",
    quiz: {} as Quiz,
    loading: false,
    error: "",
  });

  const generateQuiz = async (): Promise<response | undefined> => {
    try {
      setQuizState({ ...quizState, loading: true });
      const response = await post<response>("/quizes/prompt", {
        topic: quizState.topic,
        useAi: true,
      });
      return response;
    } catch (error: any) {
      setQuizState({ ...quizState, loading: false, error: error.message });
    }
  };

  const saveQuizInMemory = async (quiz: Quiz) => {
    // try{
    //     setQuizState({ ...quizState, loading: true, quiz: quiz });
    //     const response: UserMemory = await post<response>(`/memories/quiz/${quiz.id}`);
    //     const getMemoryContext =   getUserMemoryContext();
    //     await getMemoryContext.storeQuizToUserMemory(quiz);
    // }catch(){
  };
  return;
};

//   return (
//     // <QuizContext.Provider value={{ quizState, generateQuiz, saveQuizInMemory }}>
//     //   {children}
//     // </QuizContext.Provider>
//   );
