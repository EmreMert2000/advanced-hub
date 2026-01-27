import { NavigationContainer, NavigationIndependentTree } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import FirstScreen from "./FirstScreen";
import SecondScreen from "./SecondScreen";
import ThirdScreen from "./ThirdScreen";

const Stack = createNativeStackNavigator();

export default function IntroNavigation() {
    return (
        <NavigationIndependentTree>
            <NavigationContainer>
                <Stack.Navigator
                    initialRouteName="First"
                    screenOptions={{
                        headerShown: false,
                        animation: "slide_from_right"
                    }}
                >
                    <Stack.Screen name="First" component={FirstScreen} />
                    <Stack.Screen name="Second" component={SecondScreen} />
                    <Stack.Screen name="Third" component={ThirdScreen} />
                </Stack.Navigator>
            </NavigationContainer>
        </NavigationIndependentTree>
    );
}
