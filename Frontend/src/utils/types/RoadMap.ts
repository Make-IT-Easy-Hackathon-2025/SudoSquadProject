import { RoadMapItem } from "./RoadMapItem";

export type RoadMap = {
  id: number;
  title: string;
  description: string | undefined;
  items: RoadMapItem[];
};
