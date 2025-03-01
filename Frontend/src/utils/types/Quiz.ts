import { QuizQuestion } from "./QuizQuestion";

export type Quiz = {
  id: number;
  title: string;
  description: string | undefined;
  questions: QuizQuestion[];
};
