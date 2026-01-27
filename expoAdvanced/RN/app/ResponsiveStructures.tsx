import { Dimensions, Text, View } from "react-native";


export default function ResponsiveStructures() {

    console.log(Dimensions.get("window"));
    const deviceWidth = Dimensions.get("window").width;
    const deviceHeight = Dimensions.get("window").height;
    return (

        <View style={{ width: deviceWidth, height: deviceHeight }}>
           <View style={{ width: deviceWidth/2, height: deviceHeight/2 ,backgroundColor:"red"}}>
            <Text>Red</Text>
           </View>
           <View style={{ width: deviceWidth/2, height: deviceHeight/2 ,backgroundColor:"blue"}}>
            <Text>Blue</Text>
           </View>
           <View style={{ width: deviceWidth, height: deviceHeight ,backgroundColor:"green"}}>
            <Text>Green</Text>
           </View>
        </View>
    );
}