// Force refresh - Update to IntroNavigation
import { View } from "react-native";
import IntroNavigation from "./NavigationProceeses/IntroNavigation";
import MainStackNavigatior from "./FilmListApp/src/navigations/MainStackNavigatior";

import BottomTabsNavigator from "./FilmListApp/src/navigations/BottomTabs";

export default function Index() {
    return (
        <BottomTabsNavigator />
    );
}

