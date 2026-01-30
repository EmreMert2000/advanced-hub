import React from "react";
import { View, Text, Image, StyleSheet, TouchableOpacity } from "react-native";
import { Movie } from "../types";
import colors from "../themes/colors";

interface MovieCardProps {
  item: Movie;
  onPress?: (movie: Movie) => void;
}

const MovieCard: React.FC<MovieCardProps> = ({ item, onPress }) => {
  return (
    <TouchableOpacity 
      style={styles.card} 
      onPress={() => onPress && onPress(item)}
      activeOpacity={0.7}
    >
      <Image
        source={{
          uri: item.Poster !== "N/A" ? item.Poster : "https://via.placeholder.com/150",
        }}
        style={styles.poster}
        resizeMode="cover"
      />
      <View style={styles.info}>
        <Text style={styles.title} numberOfLines={2}>
          {item.Title}
        </Text>
        <Text style={styles.year}>{item.Year}</Text>
      </View>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  card: {
    backgroundColor: "#1e1e1e",
    borderRadius: 8,
    width: "48%",
    marginBottom: 16,
    overflow: "hidden",
    elevation: 3,
  },
  poster: {
    width: "100%",
    height: 200,
  },
  info: {
    padding: 10,
  },
  title: {
    color: colors.text,
    fontSize: 14,
    fontWeight: "bold",
    marginBottom: 4,
  },
  year: {
    color: colors.gray,
    fontSize: 12,
  },
});

export default MovieCard;
