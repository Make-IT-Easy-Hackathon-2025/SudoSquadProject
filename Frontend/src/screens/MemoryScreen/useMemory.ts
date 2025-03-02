import { useState } from "react";
import { UserMemory } from "../../utils/types/UserMemory";

export const useMemory = () => {
  const [memory, setMemory] = useState<UserMemory>();

  return { memory, setMemory };
};
