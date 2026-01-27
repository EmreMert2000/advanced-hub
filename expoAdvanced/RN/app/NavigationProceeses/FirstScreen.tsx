import { MaterialCommunityIcons } from "@expo/vector-icons";
import { Pressable, SafeAreaView, StyleSheet, Text, View } from "react-native";

export default function FirstScreen({ navigation }: any) {
    return (
        <SafeAreaView style={styles.container}>
            <View style={styles.content}>
                <View style={styles.iconContainer}>
                    <MaterialCommunityIcons name="rocket-launch" size={80} color="#6C63FF" />
                </View>
                <Text style={styles.title}>Welcome to Expo{'\n'}Transitions</Text>
                <Text style={styles.subtitle}>
                    Discover the power of smooth navigation and modern UI design in React Native.
                </Text>

                <Pressable
                    style={({ pressed }) => [
                        styles.button,
                        pressed && styles.buttonPressed
                    ]}
                    onPress={() => navigation.navigate("Second")}
                >
                    <Text style={styles.buttonText}>Get Started</Text>
                    <MaterialCommunityIcons name="arrow-right" size={20} color="#fff" />
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
        paddingHorizontal: 30,
    },
    iconContainer: {
        width: 150,
        height: 150,
        borderRadius: 75,
        backgroundColor: "#F0F0FF",
        alignItems: "center",
        justifyContent: "center",
        marginBottom: 40,
    },
    title: {
        fontSize: 32,
        fontWeight: "bold",
        color: "#1A1A1A",
        textAlign: "center",
        lineHeight: 40,
    },
    subtitle: {
        fontSize: 16,
        color: "#666",
        textAlign: "center",
        marginTop: 15,
        marginBottom: 50,
        lineHeight: 24,
    },
    button: {
        backgroundColor: "#6C63FF",
        flexDirection: "row",
        alignItems: "center",
        paddingHorizontal: 35,
        paddingVertical: 18,
        borderRadius: 20,
        gap: 10,
        elevation: 5,
        shadowColor: "#6C63FF",
        shadowOffset: { width: 0, height: 4 },
        shadowOpacity: 0.3,
        shadowRadius: 8,
    },
    buttonPressed: {
        transform: [{ scale: 0.98 }],
        opacity: 0.9,
    },
    buttonText: {
        color: "#fff",
        fontSize: 18,
        fontWeight: "700",
    },
});
