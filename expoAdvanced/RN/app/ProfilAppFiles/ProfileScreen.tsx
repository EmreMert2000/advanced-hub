import { SafeAreaView, ScrollView, StatusBar, StyleSheet, View } from "react-native";
import ActionButtons from "./components/ActionButtons";
import ProfileHeader from "./components/Header";
import PostGrid from "./components/PostGrid";
import ProfileStats from "./components/Stats";

export default function ProfileScreen() {
    return (
        <SafeAreaView style={styles.safeArea}>
            <StatusBar barStyle="dark-content" />
            <ScrollView style={styles.container} showsVerticalScrollIndicator={false}>
                <ProfileHeader />
                <ProfileStats />
                <ActionButtons />
                <View style={styles.divider} />
                <PostGrid />
            </ScrollView>
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    safeArea: {
        flex: 1,
        backgroundColor: "#F8F9FA",
    },
    container: {
        flex: 1,
    },
    divider: {
        height: 1,
        backgroundColor: "#EEE",
        marginVertical: 10,
    },
});
