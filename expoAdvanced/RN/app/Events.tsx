
import { StyleSheet,Pressable, Text, View } from "react-native";

export default function Events() {
    return (
        <View style={{ flex: 1 }}>
            <Pressable onPress={() => console.log("Tıklandı")}>
                <Text>Tıkla</Text>
            </Pressable>
        </View>
        //TouchableOpacity ile farkı static çalışması
        //Resimde oynama olmaz

    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
    },
});