import React, { useEffect, useState } from "react";
import {
  View,
  Text,
  FlatList,
  StyleSheet,
  TextInput,
  ActivityIndicator,
  SafeAreaView,
  StatusBar,
  TouchableOpacity,
} from "react-native";
import { searchMovies } from "../api/omdb";
import { Movie } from "../types";
import colors from "../themes/colors";
import MovieCard from "../components/MovieCard";

export default function HomeScreen() {
  const [query, setQuery] = useState("Marvel");
  const [movies, setMovies] = useState<Movie[]>([]);
  const [loading, setLoading] = useState(false);

  const fetchMovies = async (searchQuery: string) => {
    if (!searchQuery) return;
    setLoading(true);
    const result = await searchMovies(searchQuery);
    if (result.Response === "True") {
      setMovies(result.Search);
    } else {
      setMovies([]);
    }
    setLoading(false);
  };

  useEffect(() => {
    fetchMovies(query);
  }, []);

  const renderMovieItem = ({ item }: { item: Movie }) => (
    <MovieCard item={item} onPress={(movie) => console.log("Pressed", movie.Title)} />
  );

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="light-content" backgroundColor={colors.background} />
      <View style={styles.header}>
        <Text style={styles.headerTitle}>FilmListApp</Text>
      </View>

      <View style={styles.searchContainer}>
        <TextInput
          style={styles.searchInput}
          placeholder="Search for movies..."
          placeholderTextColor={colors.gray}
          value={query}
          onChangeText={setQuery}
          onSubmitEditing={() => fetchMovies(query)}
        />
        <TouchableOpacity style={styles.searchButton} onPress={() => fetchMovies(query)}>
          <Text style={styles.searchButtonText}>Search</Text>
        </TouchableOpacity>
      </View>

      {loading ? (
        <View style={styles.center}>
          <ActivityIndicator size="large" color={colors.accent} />
        </View>
      ) : (
        <FlatList
          data={movies}
          keyExtractor={(item) => item.imdbID}
          renderItem={renderMovieItem}
          contentContainerStyle={styles.listContent}
          numColumns={2}
          columnWrapperStyle={styles.columnWrapper}
          ListEmptyComponent={
            <View style={styles.center}>
              <Text style={styles.emptyText}>No movies found</Text>
            </View>
          }
        />
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: colors.background,
  },
  header: {
    padding: 16,
    borderBottomWidth: 1,
    borderBottomColor: "#333",
    alignItems: "center",
  },
  headerTitle: {
    color: colors.text,
    fontSize: 24,
    fontWeight: "bold",
  },
  searchContainer: {
    flexDirection: "row",
    padding: 16,
    alignItems: "center",
  },
  searchInput: {
    flex: 1,
    backgroundColor: "#2a2a2a",
    color: colors.text,
    borderRadius: 8,
    paddingHorizontal: 16,
    paddingVertical: 10,
    marginRight: 10,
    fontSize: 16,
  },
  searchButton: {
    backgroundColor: colors.accent,
    paddingVertical: 10,
    paddingHorizontal: 16,
    borderRadius: 8,
  },
  searchButtonText: {
    color: colors.white,
    fontWeight: "bold",
    fontSize: 16,
  },
  listContent: {
    padding: 8,
  },
  columnWrapper: {
    justifyContent: "space-between",
  },
  center: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    marginTop: 50,
  },
  emptyText: {
    color: colors.gray,
    fontSize: 18,
  },
});