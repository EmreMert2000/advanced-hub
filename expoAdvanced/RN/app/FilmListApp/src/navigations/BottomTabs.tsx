import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import HomeScreen from "../screens/HomeScreen";
import AntDesign from '@expo/vector-icons/AntDesign';

const BottomTabs = createBottomTabNavigator();
function BottomTabsNavigator() {
    return (
        <BottomTabs.Navigator
        screenOptions={{
            tabBarActiveTintColor: 'tomato',
            tabBarInactiveTintColor: 'gray',
            headerStyle:{backgroundColor:'black'},
            headerTitleStyle:{color:'white'},
            headerTitleAlign:'center',
            headerTitle:'Film List App'
        }}>
            <BottomTabs.Screen options={{
                tabBarIcon: ({ color, size }) => (
                    <AntDesign name="home" size={24} color={color} />
                ),
            }} name="Home" component={HomeScreen} />
             <BottomTabs.Screen options={{
                tabBarIcon: ({ color, size }) => (
                    <AntDesign name="layout" size={24} color={color} />
                ),
            }} name="Categories" component={HomeScreen} />
             <BottomTabs.Screen options={{
                tabBarIcon: ({ color, size }) => (
                    <AntDesign name="save" size={size} color={color} />
                ),
            }} name="SavedScreen" component={HomeScreen} />
        </BottomTabs.Navigator>
    );
}
export default BottomTabsNavigator;
