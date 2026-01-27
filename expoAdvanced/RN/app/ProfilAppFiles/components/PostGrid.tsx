import { Dimensions, FlatList, StyleSheet, View } from "react-native";

const { width } = Dimensions.get("window");
const ITEM_SIZE = width / 3 - 2;

const POSTS = [
    { id: "1", color: "#FF6B6B" },
    { id: "2", color: "#4ECDC4" },
    { id: "3", color: "#45B7D1" },
    { id: "4", color: "#96CEB4" },
    { id: "5", color: "#FFEEAD" },
    { id: "6", color: "#D4A5A5" },
    { id: "7", color: "#9B59B6" },
    { id: "8", color: "#3498DB" },
    { id: "9", color: "#E67E22" },
];

export default function PostGrid() {
    const renderItem = ({ item }: { item: typeof POSTS[0] }) => (
        <View style={[styles.postItem, { backgroundColor: item.color }]} />
    );

    return (
        <FlatList
            data={POSTS}
            renderItem={renderItem}
            keyExtractor={(item) => item.id}
            numColumns={3}
            scrollEnabled={false}
            contentContainerStyle={styles.container}
        />
    );
}

const styles = StyleSheet.create({
    container: {
        padding: 1,
    },
    postItem: {
        width: ITEM_SIZE,
        height: ITEM_SIZE,
        margin: 1,
    },
});
