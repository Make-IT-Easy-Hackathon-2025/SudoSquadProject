import { useAuth } from "../../contexts/AuthContext";
import  {UserMemory}  from "../../utils"

export const useHome = () => {
    const auth = useAuth();
    const user = auth.authState?.user;

    const userMemories: UserMemory[] = [
        {
          id: "1",
          title: "Birthday Party",
          description: "Had a wonderful time with family and friends.",
          imageUrl: "",
          date: "2017-02-20T12:15:00.000Z"
        },
        {
            id: "2",
            title: "Vacation in Paris",
            description: "Explored the Eiffel Tower and local markets.",
            imageUrl: "",
          date: "2019-06-30T10:20:00.000Z"
        },
        {
            id: "3",
            title: "Wedding Day",
            description: "Said 'I do' to the love of my life.",
            imageUrl: "",
          date: "2018-05-16T15:30:00.000Z"
        },
        {
            id: "4",
            title: "Family Reunion",
            description: "Reconnected with relatives after a long time.",
            imageUrl: "",
          date: "2016-11-25T09:10:00.000Z"
        },
        {
            id: "5",
            title: "Graduation Day",
          description: "Graduated with honors and started a new chapter.",
          imageUrl: "",
          date: "2020-07-15T13:45:00.000Z"
        },
        {
            id: "6",
            title: "Christmas Celebration",
          description: "Celebrated the season with joy and laughter.",
          imageUrl: "",
          date: "2021-12-24T17:00:00.000Z"
        },
        
      ]
      const nodes: UserMemory[]  = [];
      const edges: {from: number, to: number}[] = [];

      userMemories.forEach((memory, index) => {
        nodes.push(memory);
      
        // Ha nem az utolsó esemény, akkor összekötjük a következővel
        if (index < userMemories.length - 1) {
          edges.push({
            from: parseInt(memory.id),
            to: parseInt(userMemories[index + 1].id)
          });
        }
      });
      

      return { userMemories, nodes, edges, user  };
};