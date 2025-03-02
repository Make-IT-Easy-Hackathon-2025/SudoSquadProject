import { FlatList, View } from 'react-native';
import { BodyText, Column, CustomButton, Header2, Row } from "../atoms";
import { RoadMap } from "../../utils/types";
import BouncyCheckbox from "react-native-bouncy-checkbox";

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
              <Header2 text={item.order.toString() + "."} />
              <BodyText text={item.value} />
              <BouncyCheckbox onPress={() => {
                item.isDone = !item.isDone;
              }}/>
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
      style={{ height: "80%" }}
    />
  );
};
