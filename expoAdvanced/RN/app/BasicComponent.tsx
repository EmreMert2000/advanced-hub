import { ScrollView,Button, Image, StyleSheet, Text, View, ActivityIndicator } from "react-native";

import { SafeAreaView } from "react-native-safe-area-context";
export default function BasicComponent() {
    return (
           <SafeAreaView style={styles.container}>
            <ScrollView horizontal showsHorizontalScrollIndicator={false}>
                 <View style={styles.titleContainer}>
               <Text style={styles.title}>Home</Text>
             </View>
             <View style={styles.titleContainer}>
               <Text style={styles.title}>Anasayfa</Text>
             </View>
             <Button title="Home" onPress={() => {}} />
             <Image 
               source={require('@/assets/images/icon.png')} 
               style={styles.icon}
             />
             <Image
               source={{ uri: 'https://ichef.bbci.co.uk/news/1536/cpsprodpb/14235/production/_100058428_mediaitem100058424.jpg.webp' }} 
               style={styles.icon}
             />
             <ActivityIndicator size="large" color="red" />
            </ScrollView>
           </SafeAreaView>
         );

}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    gap: 16,
    backgroundColor:"rgba(128, 224, 32, 0.13)"
  },
  titleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  title: {
    fontSize: 20,
    fontWeight: 'bold',
  },
  icon: {
    width: 100,
    height: 100,
  },
});