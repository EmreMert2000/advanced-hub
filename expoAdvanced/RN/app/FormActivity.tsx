//Text Input ve button activity ekranları

import { Text, View, StyleSheet, TextInput, Pressable } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { useState } from "react";

export default function FormActivity() {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [value, setValue] = useState("");
    return (
        <SafeAreaView style={styles.container}>
            <Text>Form Activity</Text>
            <TextInput style={styles.input} placeholder="Name" keyboardType="default" onChangeText={setName}/>
            <TextInput style={styles.input} placeholder="Email" keyboardType="email-address" onChangeText={setEmail}/>
            <TextInput style={styles.input} placeholder="Password" secureTextEntry={true} onChangeText={setPassword}/>
            <Pressable style={styles.button} onPress={() => console.log({ name: name, email: email, password: password })}>
                <Text style={styles.buttonText}>Submit</Text>
            </Pressable>
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor:"#e7dedeff"
    },
    input: {
        width: "80%",
        height: 40,
        borderColor: "#ccc",
        borderWidth: 1,
        marginBottom: 10,
        paddingHorizontal: 10,
    },
    button: {
        width: "80%",
        height: 40,
        backgroundColor: "#be1717ff",
        justifyContent: "center",
        alignItems: "center",
        borderRadius: 5,
    },
    buttonText: {
        color: "#fff",
        fontSize: 16,
        fontWeight: "bold",
    },
});