import { StyleSheet, Text, View } from "react-native";

export default function ProfileStats() {
    return (
        <View style={styles.container}>
            <View style={styles.statBox}>
                <Text style={styles.statValue}>1,254</Text>
                <Text style={styles.statLabel}>Followers</Text>
            </View>
            <View style={[styles.statBox, styles.borderLeft]}>
                <Text style={styles.statValue}>482</Text>
                <Text style={styles.statLabel}>Following</Text>
            </View>
            <View style={[styles.statBox, styles.borderLeft]}>
                <Text style={styles.statValue}>128</Text>
                <Text style={styles.statLabel}>Posts</Text>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flexDirection: "row",
        backgroundColor: "#fff",
        marginHorizontal: 20,
        borderRadius: 16,
        paddingVertical: 15,
        elevation: 4,
        shadowColor: "#000",
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.1,
        shadowRadius: 4,
        marginVertical: 10,
    },
    statBox: {
        flex: 1,
        alignItems: "center",
    },
    borderLeft: {
        borderLeftWidth: 1,
        borderLeftColor: "#eee",
    },
    statValue: {
        fontSize: 18,
        fontWeight: "bold",
        color: "#333",
    },
    statLabel: {
        fontSize: 12,
        color: "#888",
        marginTop: 2,
    },
});
