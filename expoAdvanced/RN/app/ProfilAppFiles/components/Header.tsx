import { Image, StyleSheet, Text, View } from "react-native";

export default function ProfileHeader() {
    return (
        <View style={styles.container}>
            <View style={styles.avatarContainer}>
                <Image
                    source={require("../../../assets/images/profile_avatar.png")}
                    style={styles.avatar}
                />
            </View>
            <Text style={styles.name}>Antigravity Dev</Text>
            <Text style={styles.username}>@antigravity_dev</Text>
            <Text style={styles.bio}>
                Building the future of agentic coding. 🚀 {"\n"}React Native entusiast & UI Designer.
            </Text>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        alignItems: "center",
        paddingVertical: 20,
    },
    avatarContainer: {
        padding: 4,
        borderRadius: 60,
        borderWidth: 2,
        borderColor: "#6C63FF",
        marginBottom: 12,
    },
    avatar: {
        width: 100,
        height: 100,
        borderRadius: 50,
    },
    name: {
        fontSize: 22,
        fontWeight: "bold",
        color: "#333",
    },
    username: {
        fontSize: 14,
        color: "#666",
        marginBottom: 8,
    },
    bio: {
        fontSize: 15,
        color: "#444",
        textAlign: "center",
        paddingHorizontal: 40,
        lineHeight: 20,
    },
});
