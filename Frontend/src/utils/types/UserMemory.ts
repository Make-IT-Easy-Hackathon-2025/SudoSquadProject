export type UserMemory = {
  id: number;
  title: string;
  description: string | undefined;
  imageUrl?: string | undefined;
  quizId: number | undefined;
  roadmapId: number | undefined;
  memorygameId: number | undefined;
  date: Date | undefined;
};
