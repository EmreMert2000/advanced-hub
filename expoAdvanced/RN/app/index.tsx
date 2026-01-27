import { View } from "react-native";
import BasicComponent from "./BasicComponent";
import ModalScreen from "./modal";
import Events from "./Events";
import FormActivity from "./FormActivity";
import ResponsiveStructures from "./ResponsiveStructures";
import ProfileScreen from "./ProfileScreen";

export default function Index() {
    return (
        <View style={{ flex: 1 }}>
           
            <ProfileScreen/>
            
        </View>
    );
}