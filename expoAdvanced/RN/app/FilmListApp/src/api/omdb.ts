const API_KEY = "9b1460f6";
const BASE_URL = `https://www.omdbapi.com/?apikey=${API_KEY}`;

import { SearchResponse } from "../types";

export const searchMovies = async (query: string): Promise<SearchResponse> => {
  try {
    const response = await fetch(`${BASE_URL}&s=${encodeURIComponent(query)}`);
    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error fetching movies:", error);
    return { Search: [], totalResults: "0", Response: "False", Error: "Network Error" };
  }
};