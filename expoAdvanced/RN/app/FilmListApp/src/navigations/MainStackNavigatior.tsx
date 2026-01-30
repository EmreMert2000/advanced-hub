import { createNativeStackNavigator } from "@react-navigation/native-stack";
import HomeScreen from "../screens/HomeScreen";
const stack = createNativeStackNavigator();
function MainStackNavigatior() {
    return (
        <stack.Navigator>
            <stack.Screen name="Home" component={HomeScreen} />
        </stack.Navigator>
    );
}
export default MainStackNavigatior;
