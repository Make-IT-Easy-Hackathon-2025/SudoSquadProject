import { FlatList, View } from "react-native";
import { BodyText, Column, Header2, Row } from "../atoms";
import { RoadMap } from "../../utils/types";

export const RoadMapItemsWidget: React.FC<{
  roadMap: RoadMap
}> = ({ roadMap }) => {
  return (
    <FlatList
      data={roadMap?.items}
      renderItem={({ item }) => {
        return (
          <Column>
            <Row style={{ alignItems: "center" }}>
              <Header2 text={item.id.toString() + "."} />
              <BodyText text={item.value} />
            </Row>
          </Column>
        );
      }}
      ListEmptyComponent={() => (
          <View></View>
      )}
      keyExtractor={(item, index) =>
        item.id ? item.id.toString() : index.toString()
      }
      style={{ height: 550 }}
    />
  );
};
