import { MaterialCommunityIcons } from "@expo/vector-icons";
import { Pressable, SafeAreaView, StyleSheet, Text, View } from "react-native";

export default function ThirdScreen({ navigation }: any) {
    return (
        <SafeAreaView style={styles.container}>
            <View style={styles.content}>
                <View style={styles.successIcon}>
                    <MaterialCommunityIcons name="check-decagram" size={100} color="#4CAF50" />
                </View>

                <Text style={styles.title}>All Set!</Text>
                <Text style={styles.subtitle}>
                    You've successfully completed the navigation tutorial. You can now build amazing apps!
                </Text>

                <Pressable
                    style={({ pressed }) => [
                        styles.finishButton,
                        pressed && styles.buttonPressed
                    ]}
                    onPress={() => navigation.navigate("First")}
                >
                    <Text style={styles.finishButtonText}>Restart Journey</Text>
                </Pressable>

                <Pressable
                    style={styles.secondaryButton}
                    onPress={() => navigation.popToTop()}
                >
                    <Text style={styles.secondaryButtonText}>Back to Home</Text>
                </Pressable>
            </View>
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: "#fff",
    },
    content: {
        flex: 1,
        alignItems: "center",
        justifyContent: "center",
        paddingHorizontal: 40,
    },
    successIcon: {
        width: 180,
        height: 180,
        borderRadius: 90,
        backgroundColor: "#E8F5E9",
        alignItems: "center",
        justifyContent: "center",
        marginBottom: 40,
    },
    title: {
        fontSize: 32,
        fontWeight: "bold",
        color: "#1A1A1A",
        marginBottom: 16,
    },
    subtitle: {
        fontSize: 16,
        color: "#666",
        textAlign: "center",
        lineHeight: 24,
        marginBottom: 60,
    },
    finishButton: {
        backgroundColor: "#4CAF50",
        width: "100%",
        height: 60,
        borderRadius: 20,
        alignItems: "center",
        justifyContent: "center",
        marginBottom: 16,
        elevation: 4,
        shadowColor: "#4CAF50",
        shadowOffset: { width: 0, height: 4 },
        shadowOpacity: 0.3,
        shadowRadius: 8,
    },
    finishButtonText: {
        color: "#fff",
        fontSize: 18,
        fontWeight: "700",
    },
    secondaryButton: {
        padding: 10,
    },
    secondaryButtonText: {
        color: "#666",
        fontSize: 16,
        fontWeight: "600",
    },
    buttonPressed: {
        opacity: 0.9,
        transform: [{ scale: 0.98 }],
    },
});
