import { QuizOption } from "./QuizOption";

export type QuizQuestion = {
  id: number;
  questionValue: string;
  options: QuizOption[];
};
