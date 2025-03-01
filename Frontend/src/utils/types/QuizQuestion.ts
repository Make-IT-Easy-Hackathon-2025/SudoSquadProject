import { QuizOption } from "./QuizOption";

export type QuizQuestion = {
  id: number;
  value: string;
  options: QuizOption[];
};
