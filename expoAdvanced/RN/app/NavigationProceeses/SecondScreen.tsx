import { MaterialCommunityIcons } from "@expo/vector-icons";
import { Pressable, SafeAreaView, ScrollView, StyleSheet, Text, View } from "react-native";

const FEATURES = [
    { icon: "star-outline", title: "Premium Core", desc: "Built with the latest technologies." },
    { icon: "lightning-bolt-outline", title: "Fast Performance", desc: "Optimized for all devices." },
    { icon: "shield-check-outline", title: "Secure & Reliable", desc: "Data protection by default." },
];

export default function SecondScreen({ navigation }: any) {
    return (
        <SafeAreaView style={styles.container}>
            <ScrollView contentContainerStyle={styles.content}>
                <Pressable onPress={() => navigation.goBack()} style={styles.backButton}>
                    <MaterialCommunityIcons name="chevron-left" size={28} color="#333" />
                </Pressable>

                <Text style={styles.title}>Key Features</Text>
                <Text style={styles.subtitle}>Our application offers top-tier features for developers.</Text>

                {FEATURES.map((item, index) => (
                    <View key={index} style={styles.card}>
                        <View style={styles.iconBox}>
                            <MaterialCommunityIcons name={item.icon as any} size={28} color="#6C63FF" />
                        </View>
                        <View style={styles.cardText}>
                            <Text style={styles.cardTitle}>{item.title}</Text>
                            <Text style={styles.cardDesc}>{item.desc}</Text>
                        </View>
                    </View>
                ))}

                <Pressable
                    style={({ pressed }) => [
                        styles.nextButton,
                        pressed && styles.buttonPressed
                    ]}
                    onPress={() => navigation.navigate("Third")}
                >
                    <Text style={styles.nextButtonText}>Next Step</Text>
                </Pressable>
            </ScrollView>
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: "#F8F9FA",
    },
    content: {
        padding: 24,
    },
    backButton: {
        width: 44,
        height: 44,
        borderRadius: 22,
        backgroundColor: "#fff",
        alignItems: "center",
        justifyContent: "center",
        marginBottom: 20,
        elevation: 2,
        shadowColor: "#000",
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.1,
        shadowRadius: 4,
    },
    title: {
        fontSize: 28,
        fontWeight: "bold",
        color: "#1A1A1A",
        marginBottom: 8,
    },
    subtitle: {
        fontSize: 16,
        color: "#666",
        marginBottom: 32,
    },
    card: {
        flexDirection: "row",
        backgroundColor: "#fff",
        padding: 20,
        borderRadius: 20,
        marginBottom: 16,
        alignItems: "center",
        elevation: 2,
        shadowColor: "#000",
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.05,
        shadowRadius: 5,
    },
    iconBox: {
        width: 54,
        height: 54,
        borderRadius: 15,
        backgroundColor: "#F0F0FF",
        alignItems: "center",
        justifyContent: "center",
        marginRight: 16,
    },
    cardText: {
        flex: 1,
    },
    cardTitle: {
        fontSize: 18,
        fontWeight: "600",
        color: "#333",
        marginBottom: 4,
    },
    cardDesc: {
        fontSize: 14,
        color: "#888",
    },
    nextButton: {
        backgroundColor: "#1A1A1A",
        height: 56,
        borderRadius: 18,
        alignItems: "center",
        justifyContent: "center",
        marginTop: 20,
    },
    nextButtonText: {
        color: "#fff",
        fontSize: 16,
        fontWeight: "700",
    },
    buttonPressed: {
        opacity: 0.8,
        transform: [{ scale: 0.98 }],
    },
});
