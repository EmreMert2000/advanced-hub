import { Pressable, StyleSheet, Text, View } from "react-native";

export default function ActionButtons() {
    return (
        <View style={styles.container}>
            <Pressable style={[styles.button, styles.primaryButton]}>
                <Text style={styles.primaryButtonText}>Edit Profile</Text>
            </Pressable>
            <Pressable style={[styles.button, styles.secondaryButton]}>
                <Text style={styles.secondaryButtonText}>Share Profile</Text>
            </Pressable>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flexDirection: "row",
        paddingHorizontal: 20,
        gap: 12,
        marginVertical: 10,
    },
    button: {
        flex: 1,
        height: 44,
        borderRadius: 12,
        alignItems: "center",
        justifyContent: "center",
    },
    primaryButton: {
        backgroundColor: "#6C63FF",
    },
    secondaryButton: {
        backgroundColor: "#F0F0FF",
        borderWidth: 1,
        borderColor: "#E0E0FF",
    },
    primaryButtonText: {
        color: "#fff",
        fontWeight: "600",
        fontSize: 15,
    },
    secondaryButtonText: {
        color: "#6C63FF",
        fontWeight: "600",
        fontSize: 15,
    },
});
